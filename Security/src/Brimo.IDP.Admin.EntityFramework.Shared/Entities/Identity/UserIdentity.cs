using Microsoft.AspNetCore.Identity;
using System;

namespace Brimo.IDP.Admin.EntityFramework.Shared.Entities.Identity
{
	public class UserIdentity : IdentityUser
	{
		public string BusinessUserId { get; set; }
		public string FullName { get; set; }
		public UserType UserType { get; set; }
		public DateTime LastLoginDate { get; set; }
	}
}





