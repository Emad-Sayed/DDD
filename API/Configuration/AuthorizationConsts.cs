using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Configuration
{
    public class AuthorizationConsts
    {
        public const string AdministrationPolicy = "RequireAdministratorRole";
        public const string CustomerPolicy = "RequireCustomerRole";
        public const string DistributorPolicy = "RequireDistributorRole";
    }
}
