using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Brimo.IDP.Admin.EntityFramework.Shared.Entities.Identity;
using Brimo.IDP.STS.Identity.Services;
using Brimo.IDP.STS.Identity.ViewModels.Auth.Register;
using Microsoft.AspNetCore.Identity;
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
        private readonly ISMSNotification _smsSender;
        private readonly IConfiguration _configuration;

        public AuthController(UserManager<UserIdentity> userManager, ISMSNotification smsSender, IConfiguration configuration)
        {
            _smsSender = smsSender;
            _userManager = userManager;
            _configuration = configuration;
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
                if (!result.Succeeded)
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
                ToPhoneNumber = "+201005060016"
            });
            return Ok(new { verificationCode = code });
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
        public async Task<IActionResult> Register(RegisterVM model)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.PhoneNumber == model.PhoneNumber);
            if (user == null) return BadRequest("User with this phone number not found");

            if (!user.PhoneNumberConfirmed) return BadRequest("Please confirm your phone number first");

           var result = await _userManager.AddPasswordAsync(user, model.Password);

            var bussnessUserId = await CreateCustomer(model, user);
            user.BusinessUserId = bussnessUserId;
            await _userManager.UpdateAsync(user);
            return Ok();
        }


        private async Task<string> CreateCustomer(RegisterVM model, UserIdentity user)
        {
            // TODO Get Access token from identity server

            //// discover endpoints from metadata
            //var client = new HttpClient();
            //var disco = await client.GetDiscoveryDocumentAsync("http://brimo-dev-identity-sts.azurewebsites.net");
            //if (disco.IsError)
            //{
            //    Console.WriteLine(disco.Error);

            //}
            //// request token
            //var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            //{
            //    Address = disco.TokenEndpoint,

            //    ClientId = "test",
            //    ClientSecret = "test",
            //    Scope = "brimo_api"

            //});

            //if (tokenResponse.IsError)
            //{
            //}

            HttpClient apiClient = new HttpClient();

            // TODO Set bearer toekn to http headers
            //apiClient.SetBearerToken(tokenResponse.AccessToken);

            var body = new
            {
                AccountId = user.Id.ToString(),
                model.PhoneNumber,
                model.ShopName,
                model.ShopAddress,
                model.LocationOnMap
            };

            var json = JsonConvert.SerializeObject(body);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = _configuration["BrimoAPIURL"] + "/api/CustomerManagment/Customers";

            var response = await apiClient.PostAsync(url, data);

            var responseString = await response.Content.ReadAsStringAsync();
            return responseString;
        }
    }
}