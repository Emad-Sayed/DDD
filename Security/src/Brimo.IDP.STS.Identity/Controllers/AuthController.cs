using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Brimo.IDP.Admin.EntityFramework.Shared.Entities.Identity;
using Brimo.IDP.STS.Identity.Common.Exceptions;
using Brimo.IDP.STS.Identity.Common.Exceptions.Auth;
using Brimo.IDP.STS.Identity.Common.Middlewares;
using Brimo.IDP.STS.Identity.Services;
using Brimo.IDP.STS.Identity.ViewModels.Auth.Register;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<IActionResult> SendSMSCode([FromBody] SendSMSCodeVM sendSMSCodeVM)
        {
            // Check if phone number is registered before if not will create new user with this phone number
            var userFromDb = await _userManager.Users.FirstOrDefaultAsync(x => x.PhoneNumber == sendSMSCodeVM.PhoneNumber);
            if (userFromDb != null)
            {
                var isRegisteredBefore = await _userManager.HasPasswordAsync(userFromDb);
                if (isRegisteredBefore) throw new UserAlreadyExistException(sendSMSCodeVM.PhoneNumber);
            }
            else
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
                Message = $@"{code} هو كود التفعيل الخاص بك في بريمو",
                ToPhoneNumber = sendSMSCodeVM.PhoneNumber
            });

            return Ok();
        }


        [HttpPost("VerifySMSCode")]
        public async Task<IActionResult> VerifySMSCode(VerifySMSCodeVM model)
        {
            // Get user from phone number
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.PhoneNumber == model.PhoneNumber);

            if (user == null) throw new UserNotFoundException(model.PhoneNumber);

            var isRegisteredBefore = await _userManager.HasPasswordAsync(user);
            if (isRegisteredBefore) throw new UserAlreadyExistException(model.PhoneNumber);

            var result = await _userManager.VerifyChangePhoneNumberTokenAsync(user, model.SmsCode, model.PhoneNumber);
            user.PhoneNumberConfirmed = result;
            await _userManager.UpdateAsync(user);

            return Ok(new { codeVerifed = result });
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterVM model)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.PhoneNumber == model.PhoneNumber);
            if (user == null) throw new PhoneNumberNotConfirmedException(model.PhoneNumber);

            var isRegisteredBefore = await _userManager.HasPasswordAsync(user);
            if (isRegisteredBefore) throw new UserAlreadyExistException(model.PhoneNumber);

            if (!user.PhoneNumberConfirmed) throw new PhoneNumberNotConfirmedException(model.PhoneNumber);

            var result = await _userManager.AddPasswordAsync(user, model.Password);

            if (!result.Succeeded) throw new BusinessException(HttpStatusCode.BadRequest, result.Errors.ToString(), string.Empty);

            var businessUserId = await CreateCustomer(model, user);

            user.BusinessUserId = businessUserId;

            await _userManager.UpdateAsync(user);
            return Ok();
        }

        [HttpGet("GetCustomerByPhoneNumber")]
        public async Task<IActionResult> UpdateProfile([FromQuery] string phoneNumber)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.PhoneNumber == "+" + phoneNumber.Trim());
            if (user == null) throw new UserNotFoundException(phoneNumber);

            var userDetails = await GetCustomerById(user.BusinessUserId);
            return Ok(userDetails);
        }

        [HttpPost("UpdateProfile")]
        public async Task<IActionResult> UpdateProfile(UpdateProfileVM model)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.PhoneNumber == model.PhoneNumber);
            if (user == null) throw new UserNotFoundException(model.PhoneNumber);

            if (!user.PhoneNumberConfirmed) throw new PhoneNumberNotConfirmedException(model.PhoneNumber);

            await UpdateCustomer(model, user);
            return Ok();
        }

        [HttpPost("ForgetPassword")]
        public async Task<IActionResult> ForgetPassword([FromBody] ForgetPasswordVM model)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.PhoneNumber == model.PhoneNumber);
            if (user == null) throw new UserNotFoundException(model.PhoneNumber);

            var isRegisteredBefore = await _userManager.HasPasswordAsync(user);
            if (!isRegisteredBefore) throw new UserNotRegisteredException(model.PhoneNumber);

            if (!user.PhoneNumberConfirmed) throw new PhoneNumberNotConfirmedException(model.PhoneNumber);

            var code = new Random().Next(100000, 999999).ToString();
            user.ResetPasswordCode = code;

            await _userManager.UpdateAsync(user);

            _smsSender.Send(new SMSMessageModel
            {
                Message = $@"{code} هو كود التحقق الخاص بك في بريمو",
                ToPhoneNumber = model.PhoneNumber
            });

            return Ok();
        }

        [HttpPost("ActiveAndDeactiveCustomer/{userId}")]
        public async Task<IActionResult> ActiveAndDeactiveCustomer([FromRoute] string userId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null) throw new UserNotFoundException(userId);

            user.IsActive = !user.IsActive;

            await _userManager.UpdateAsync(user);

            return Ok(new { isActive = user.IsActive });
        }

        [HttpPost("ChangeForgetPassword")]
        public async Task<IActionResult> ChangeForgetPassword([FromBody] ChangeForgetPasswordVM model)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.PhoneNumber == model.PhoneNumber);
            if (user == null) throw new UserNotFoundException(model.PhoneNumber);

            var isRegisteredBefore = await _userManager.HasPasswordAsync(user);
            if (!isRegisteredBefore) throw new UserNotRegisteredException(model.PhoneNumber);

            if (!user.PhoneNumberConfirmed) throw new PhoneNumberNotConfirmedException(model.PhoneNumber);

            if (user.ResetPasswordCode != model.Code) throw new InvalidForgetPasswordCodeException();

            var resetPasswordToken = await _userManager.GeneratePasswordResetTokenAsync(user);

            var result = await _userManager.ResetPasswordAsync(user, resetPasswordToken, model.Password);

            if (!result.Succeeded) throw new BusinessException(HttpStatusCode.BadRequest, result.Errors.ToString(), "invalid_password");

            user.ResetPasswordCode = null;
            await _userManager.UpdateAsync(user);

            return Ok();
        }


        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteUser([FromQuery] string userId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null) throw new UserNotFoundException(string.Empty);

            var result = await _userManager.DeleteAsync(user);

            if (!result.Succeeded) return BadRequest(result.Errors);

            return Ok();
        }


        [HttpPost("RegisterDistributor")]
        public async Task<IActionResult> RegisterDistributor(RegisterDistributorUserVM model)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == model.Email);
            if (user != null) return BadRequest("user_with_this_email_found");

            user = new UserIdentity { Email = model.Email, FullName = model.FullName, UserName = model.Email, IsActive = true };
            var result = await _userManager.CreateAsync(user);

            if (result.Succeeded)
            {
                // check if the Distributor role exist in the database or not if not will create it
                var customerRole = await _roleManager.FindByNameAsync("Distributor");
                if (customerRole == null) await _roleManager.CreateAsync(new UserIdentityRole { Name = "Distributor" });

                // add the curtomer to customer role
                await _userManager.AddToRoleAsync(user, "Distributor");

                try
                {

                    // send invitation to distributer user
                    await SendInvitationMail(user);
                }
                catch (Exception)
                {
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Failed to verify phone number");
                return BadRequest(ModelState);
            }


            return Ok(user.Id);
        }

        [HttpPost("ResendSendInvitationMail")]
        public async Task<IActionResult> ResendSendInvitationMail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return BadRequest("user_not_found");

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var fullConfirmationUrl = _configuration["BrimoWebURL"] + "/complete_registration" + "?email=" + user.Email + "&token=" + HttpUtility.UrlEncode(token);

            // TODO Send Token To User Email
            await _emailSender.SendEmailAsync(user.Email, "Brimo Invitation", $"Brimo Team Please click the link to complete your rgistration <a href=\"{fullConfirmationUrl}\">link</a>");

            return Ok();
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

            await ConfirmDistributorUserEmail(user.Id);
            return Ok(new { user.EmailConfirmed });
        }


        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM model)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == model.Email);
            if (user == null) return BadRequest("user_with_this_email_not_found");

            if (await _userManager.IsInRoleAsync(user, "Customer"))
                if (!user.PhoneNumberConfirmed) throw new Exception("please_confirm_your_account_first");

            if (await _userManager.IsInRoleAsync(user, "Distributor"))
                if (!user.EmailConfirmed) throw new Exception("please_confirm_your_account_first");

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
                model.AreaId,
                model.ShopAddress,
                model.LocationOnMap
            };

            var json = JsonConvert.SerializeObject(body);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = _configuration["BrimoAPIURL"] + "/api/CustomerManagment/Customers";

            var response = await apiClient.PostAsync(url, data);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                await DeleteUser(user.Id);
                var error = JsonConvert.DeserializeObject<ErrorMessage>(await response.Content.ReadAsStringAsync());
                var errorMessage = error.Errors.FirstOrDefault() == null ? error.Message : error.Errors.FirstOrDefault().ErrorMessage;
                throw new BusinessException(HttpStatusCode.BadRequest, errorMessage, "");
            }

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

        private async Task ConfirmDistributorUserEmail(string accountId)
        {
            var apiClient = new HttpClient();

            var body = new
            {
                AccountId = accountId
            };

            var json = JsonConvert.SerializeObject(body);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = _configuration["BrimoAPIURL"] + "/api/DistributorManagment/Distributors/ConfirmDistributorUserEmail";

            var response = await apiClient.PostAsync(url, data);

            if (response.StatusCode != HttpStatusCode.OK) throw new Exception(await response.Content.ReadAsStringAsync());
        }
    }
}