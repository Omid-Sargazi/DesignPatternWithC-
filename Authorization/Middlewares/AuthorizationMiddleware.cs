using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Authorization.Middlewares
{
 

    public class CustomAuthorizationRequirement : IAuthorizationRequirement
    {
        public string RequiredRole { get; }
        public int MinimumAge { get; }

        public CustomAuthorizationRequirement(string requiredRole, int minimumAge = 18)
        {
            RequiredRole = requiredRole;
            MinimumAge = minimumAge;
        }
    }

    public class CustomAuthorizationHandler : AuthorizationHandler<CustomAuthorizationRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            CustomAuthorizationRequirement requirement)
        {
            // بررسی Claimهای کاربر
            var ageClaim = context.User.FindFirst(c => c.Type == ClaimTypes.DateOfBirth);
            var roleClaim = context.User.FindFirst(c => c.Type == ClaimTypes.Role);

            if (ageClaim != null && roleClaim != null)
            {
                var birthDate = DateTime.Parse(ageClaim.Value);
                var age = DateTime.Now.Year - birthDate.Year;

                // بررسی سن
                if (age >= requirement.MinimumAge)
                {
                    // بررسی نقش
                    if (roleClaim.Value == requirement.RequiredRole)
                    {
                        context.Succeed(requirement);
                    }
                }
            }

            return Task.CompletedTask;
        }
    }
}
