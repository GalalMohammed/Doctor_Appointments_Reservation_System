using Microsoft.AspNetCore.Mvc;
using MVC.ViewModels;

namespace MVC.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ResetPassword()
        {
            return View(new ResetPasswordVM()); // Load the empty form
        }

        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model); // Return the view with validation errors
            }

            // TODO: Implement password reset logic (e.g., update user password in database)

            TempData["SuccessMessage"] = "Password reset successfully!";
            return RedirectToAction("Search","Doctor"); // Redirect to login page after successful reset
        }
    }
}
