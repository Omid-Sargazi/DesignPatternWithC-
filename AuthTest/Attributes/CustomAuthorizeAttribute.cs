using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
public class CustomAuthorizeAttribute : TypeFilterAttribute
{
    public CustomAuthorizeAttribute(params string[] permissions)
        : base(typeof(CustomAuthorizeFilter))
    {
        Arguments = new object[] { permissions };
        Order = int.MinValue; // اجرا در اولویت بالا
    }

    public string RequiredRole { get; set; }
    public string RequiredClaim { get; set; }
    public string RequiredClaimValue { get; set; }
}

public class CustomAuthorizeFilter : IAsyncAuthorizationFilter
{
    private readonly string[] _permissions;
    private readonly string _requiredRole;
    private readonly string _requiredClaim;
    private readonly string _requiredClaimValue;

    public CustomAuthorizeFilter(
        string[] permissions,
        string requiredRole = null,
        string requiredClaim = null,
        string requiredClaimValue = null)
    {
        _permissions = permissions ?? Array.Empty<string>();
        _requiredRole = requiredRole;
        _requiredClaim = requiredClaim;
        _requiredClaimValue = requiredClaimValue;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var user = context.HttpContext.User;

        // 1. بررسی احراز هویت اولیه
        if (!user.Identity.IsAuthenticated)
        {
            context.Result = new UnauthorizedObjectResult(new
            {
                Message = "نیاز به ورود به سیستم",
                Code = "UNAUTHENTICATED"
            });
            return;
        }

        // 2. بررسی نقش
        if (!string.IsNullOrEmpty(_requiredRole))
        {
            if (!user.IsInRole(_requiredRole))
            {
                context.Result = new ForbidResult();
                return;
            }
        }

        // 3. بررسی Claim
        if (!string.IsNullOrEmpty(_requiredClaim))
        {
            var claim = user.FindFirst(_requiredClaim);
            if (claim == null ||
                (!string.IsNullOrEmpty(_requiredClaimValue) && claim.Value != _requiredClaimValue))
            {
                context.Result = new ObjectResult(new
                {
                    Message = "دسترسی غیرمجاز",
                    Details = $"Claim {_requiredClaim} مورد نیاز است"
                })
                {
                    StatusCode = StatusCodes.Status403Forbidden
                };
                return;
            }
        }

        // 4. بررسی مجوزها (Permissions)
        if (_permissions.Length > 0)
        {
            var userPermissions = user.Claims
                .Where(c => c.Type == "Permission" || c.Type == "permissions")
                .Select(c => c.Value)
                .ToList();

            if (!_permissions.All(p => userPermissions.Contains(p)))
            {
                var missingPermissions = _permissions.Except(userPermissions).ToList();
                context.Result = new ObjectResult(new
                {
                    Message = "دسترسی غیرمجاز",
                    MissingPermissions = missingPermissions,
                    RequiredPermissions = _permissions
                })
                {
                    StatusCode = StatusCodes.Status403Forbidden
                };
                return;
            }
        }

        // 5. بررسی سفارشی اضافی (مثلاً IP یا زمان)
        if (!await AdditionalValidationAsync(context, user))
        {
            context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
            return;
        }
    }

    protected virtual Task<bool> AdditionalValidationAsync(AuthorizationFilterContext context, ClaimsPrincipal user)
    {
        // برای override در کلاس‌های مشتق شده
        return Task.FromResult(true);
    }
}