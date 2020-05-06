using System.ComponentModel.DataAnnotations;

namespace Brimo.IDP.STS.Identity.ViewModels.Account
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}






