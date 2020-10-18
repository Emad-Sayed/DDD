using Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace API.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            BusinessUserId = httpContextAccessor.HttpContext?.User?.FindFirstValue("BusinessUserId");
            Name = httpContextAccessor.HttpContext?.User?.FindFirstValue("name");
        }

        public string UserId { get; }
        public string BusinessUserId { get; }
        public string Name { get; }
    }
}
