using System.Collections.Generic;
using Brimo.IDP.Admin.Configuration.Identity;

namespace Brimo.IDP.Admin.Configuration.IdentityServer
{
    public class Client : global::IdentityServer4.Models.Client
    {
        public List<Claim> ClientClaims { get; set; } = new List<Claim>();
    }
}






