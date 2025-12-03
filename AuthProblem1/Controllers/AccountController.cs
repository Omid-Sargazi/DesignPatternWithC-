using Microsoft.AspNetCore.Mvc;

namespace AuthProblem1.Controllers
{
    public class AccountController: Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
    }
}
