using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthProblem1.Controllers
{
    [Authorize(AuthenticationSchemes = "JWT,Cookies")]
    public class HybridController : Controller
    {
        // هم با JWT و هم با Cookie قابل دسترسی
    }
    }
