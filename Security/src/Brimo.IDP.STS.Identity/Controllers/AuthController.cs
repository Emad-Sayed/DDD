using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Brimo.IDP.Admin.EntityFramework.Shared.Entities.Identity;
using Brimo.IDP.STS.Identity.Services;
using Brimo.IDP.STS.Identity.ViewModels.Auth.Register;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Brimo.IDP.STS.Identity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly UserManager<UserIdentity> _userManager;
        private readonly RoleManager<UserIdentityRole> _roleManager;
        private readonly ISMSNotification _smsSender;
        private readonly IConfiguration _configuration;
        private readonly IEmailSender _emailSender;

        public AuthController(
            UserManager<UserIdentity> userManager,
            RoleManager<UserIdentityRole> roleManager,
            ISMSNotification smsSender,
            IConfiguration configuration,
             IEmailSender emailSender
            )
        {
            _smsSender = smsSender;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _emailSender = emailSender;
        }


        [HttpPost("SendSMSCode")]
        public async Task<IActionResult> SendSMSCode([FromBody]SendSMSCodeVM sendSMSCodeVM)
        {
            // Check if phone number is registered before if not will create new user with this phone number
            var userFromDb = await _userManager.Users.FirstOrDefaultAsync(x => x.PhoneNumber == sendSMSCodeVM.PhoneNumber);
            if (userFromDb == null)
            {
                userFromDb = new UserIdentity
                {
                    PhoneNumber = sendSMSCodeVM.PhoneNumber,
                    UserName = sendSMSCodeVM.PhoneNumber,
                    Email = sendSMSCodeVM.PhoneNumber + "@brimo.co"
                };

                var result = await _userManager.CreateAsync(userFromDb);
                if (result.Succeeded)
                {
                    // check if the customer role exist in the database or not if not will create it
                    var customerRole = await _roleManager.FindByNameAsync("Customer");
                    if (customerRole == null) await _roleManager.CreateAsync(new UserIdentityRole { Name = "Customer" });

                    // add the curtomer to customer role
                    await _userManager.AddToRoleAsync(userFromDb, "Customer");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Failed to verify phone number");
                    return BadRequest(ModelState);
                }
            }

            // Generate phone number code
            var code = await _userManager.GenerateChangePhoneNumberTokenAsync(userFromDb, userFromDb.PhoneNumber);

            _smsSender.Send(new SMSMessageModel
            {
                Message = $@"Your code is {code}",

                // TODO Send SMS message to phone number
                ToPhoneNumber = sendSMSCodeVM.PhoneNumber
            });
            return Ok();
        }


        [HttpPost("VerifySMSCode")]
        public async Task<IActionResult> VerifySMSCode(VerifySMSCodeVM model)
        {
            // Get user from phone number
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.PhoneNumber == model.PhoneNumber);
            if (user != null)
            {
                var result = await _userManager.VerifyChangePhoneNumberTokenAsync(user, model.SmsCode, model.PhoneNumber);
                user.PhoneNumberConfirmed = result;
                await _userManager.UpdateAsync(user);

                return Ok(new { codeVerifed = result });
            }
            return BadRequest();
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody]RegisterVM model)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.PhoneNumber == model.PhoneNumber);
            if (user == null) return BadRequest("User with this phone number not found");

            if (!user.PhoneNumberConfirmed) return BadRequest("Please confirm your phone number first");

            var result = await _userManager.AddPasswordAsync(user, model.Password);

            if (!result.Succeeded) return BadRequest("this user already registerd before");

            var businessUserId = await CreateCustomer(model, user);
            user.BusinessUserId = businessUserId;
            await _userManager.UpdateAsync(user);
            return Ok();
        }

        [HttpGet("GetCustomerByPhoneNumber")]
        public async Task<IActionResult> UpdateProfile([FromQuery]string phoneNumber)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.PhoneNumber == "+" + phoneNumber.Trim());
            if (user == null) return BadRequest("user_with_this_phone_number_not_found");

            var userDetails = await GetCustomerById(user.BusinessUserId);
            return Ok(userDetails);
        }

        [HttpPost("UpdateProfile")]
        public async Task<IActionResult> UpdateProfile(UpdateProfileVM model)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.PhoneNumber == model.PhoneNumber);
            if (user == null) return BadRequest("User with this phone number not found");

            if (!user.PhoneNumberConfirmed) return BadRequest("please_confirm_your_phone_number_first");

            await UpdateCustomer(model, user);
            return Ok();
        }


        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteUser([FromQuery]string userId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null) return BadRequest("User with this id not found");

            var result = await _userManager.DeleteAsync(user);

            if (!result.Succeeded) return BadRequest(result.Errors);

            return Ok();
        }


        [HttpPost("RegisterDistributor")]
        public async Task<IActionResult> RegisterDistributor(RegisterDistributorUserVM model)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == model.Email);
            if (user != null) return BadRequest("user_with_this_email_found");

            user = new UserIdentity { Email = model.Email, FullName = model.FullName, UserName = model.Email };
            var result = await _userManager.CreateAsync(user);

            if (result.Succeeded)
            {
                // check if the Distributor role exist in the database or not if not will create it
                var customerRole = await _roleManager.FindByNameAsync("Distributor");
                if (customerRole == null) await _roleManager.CreateAsync(new UserIdentityRole { Name = "Distributor" });

                // add the curtomer to customer role
                await _userManager.AddToRoleAsync(user, "Distributor");

                // send invitation to distributer user
                await SendInvitationMail(user);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Failed to verify phone number");
                return BadRequest(ModelState);
            }


            return Ok(user.Id);
        }

        private async Task SendInvitationMail(UserIdentity user)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var fullConfirmationUrl = _configuration["BrimoWebURL"] + "/complete_registration" + "?email=" + user.Email + "&token=" + HttpUtility.UrlEncode(token);

            // TODO Send Token To User Email
            await _emailSender.SendEmailAsync(user.Email, "Brimo Invitation", $"Brimo Team Please click the link to complete your rgistration <a href=\"{fullConfirmationUrl}\">link</a>");

        }


        [HttpPost("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(ConfirmEmailVM model)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == model.Email);
            if (user == null) return BadRequest("user_with_this_email_not_found");

            var result = await _userManager.ConfirmEmailAsync(user, model.Token);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok(new { user.EmailConfirmed });
        }


        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM model)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == model.Email);
            if (user == null) return BadRequest("user_with_this_email_not_found");

            if (!user.EmailConfirmed || !user.PhoneNumberConfirmed) throw new Exception("please_confirm_your_account_first");

            var result = await _userManager.AddPasswordAsync(user, model.Password);

            if (!result.Succeeded) return BadRequest(result.Errors);

            return Ok();
        }

        private async Task<CustomerVM> GetCustomerById(string customerId)
        {
            var apiClient = new HttpClient();

            var url = _configuration["BrimoAPIURL"] + $"/api/CustomerManagment/Customers/{customerId}?customerId=" + customerId;

            var response = await apiClient.GetAsync(url);

            var responseString = await response.Content.ReadAsStringAsync();
            var customerObject = JsonConvert.DeserializeObject<CustomerVM>(responseString);

            return customerObject;
        }

        private async Task<string> CreateCustomer(RegisterVM model, UserIdentity user)
        {
            var apiClient = new HttpClient();

            var body = new
            {
                AccountId = user.Id,
                model.PhoneNumber,
                model.Fullname,
                model.ShopName,
                model.City,
                model.Area,
                model.ShopAddress,
                model.LocationOnMap
            };

            var json = JsonConvert.SerializeObject(body);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = _configuration["BrimoAPIURL"] + "/api/CustomerManagment/Customers";

            var response = await apiClient.PostAsync(url, data);
            if (response.StatusCode != HttpStatusCode.OK) throw new Exception(await response.Content.ReadAsStringAsync());

            var responseString = await response.Content.ReadAsStringAsync();
            return responseString;
        }

        private async Task<string> UpdateCustomer(UpdateProfileVM model, UserIdentity user)
        {
            var apiClient = new HttpClient();

            var body = new
            {
                AccountId = user.Id,
                model.Fullname,
                model.ShopName,
                model.ShopAddress,
                model.City,
                model.Area,
                model.LocationOnMap
            };

            var json = JsonConvert.SerializeObject(body);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = _configuration["BrimoAPIURL"] + "/api/CustomerManagment/Customers";

            var response = await apiClient.PutAsync(url, data);

            if (response.StatusCode != HttpStatusCode.OK) throw new Exception(await response.Content.ReadAsStringAsync());

            var responseString = await response.Content.ReadAsStringAsync();
            return responseString;
        }
    }
}