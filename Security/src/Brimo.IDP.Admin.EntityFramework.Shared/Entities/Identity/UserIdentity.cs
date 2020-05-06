using Microsoft.AspNetCore.Identity;

namespace Brimo.IDP.Admin.EntityFramework.Shared.Entities.Identity
{
	public class UserIdentity : IdentityUser
	{
		public UserType UserType { get; set; }
	}
}





