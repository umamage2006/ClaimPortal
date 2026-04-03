using Microsoft.AspNetCore.Mvc;
using ClaimPortal.ViewModels;

namespace ClaimPortal.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (model.Username == "admin" && model.Password == "admin123")
            {
                HttpContext.Session.SetString("User", model.Username);
                return RedirectToAction("Index", "Claim");
            }

            ViewBag.Error = "Invalid credentials";
            return View(model);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
