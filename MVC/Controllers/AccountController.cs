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
        public IActionResult ConfirmEmail(string? token)
        {
            // =============== Function Skeleton ===============
            //if (token != null)
            //{
            //    int? id = GetIdFromToken(token);
            //    if (id != null)
            //    {
            //        SaveToDb(id);
            return View();
            //    }
            //}
            //return View("CustomError");
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginUser)
        {
            // =============== Function Skeleton ===============
            //if (ModelState.IsValid)
            //{
            //    var appUser = await userManager.FindByNameAsync(loginUser.Username);
            //    if (appUser != null)
            //    {
            //        bool correctPassword = await userManager.CheckPasswordAsync(appUser, loginUser.Password);
            //        if (correctPassword)
            //        {
            //            await signInManager.SignInAsync(appUser, loginUser.RememberMe);
            //            return RedirectToAction("Index", "Customer");
            //        }
            //    }
            //}
            ModelState.AddModelError("", "Invalid username or password");
            return View(loginUser);
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel registerUser)
        {
            ModelState.AddModelError("", "Test Error 1");
            ModelState.AddModelError("", "Test Error 2");
            ModelState.AddModelError("", "Test Error 3");
            ModelState.AddModelError("", "Test Error 4");
            return View(registerUser);
        }

        public IActionResult ForgetPassword()
        {

            return View();
        }

        [HttpPost]
        public IActionResult ForgetPassword(ForgetPasswordVM forgetPasswordVM)
        {
            #region need some sort of checking (If Email Already Exist in DB)

            #endregion
            if (ModelState.IsValid)
            {
                return RedirectToAction("ResetPassword");
            }
            return View(forgetPasswordVM);
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
