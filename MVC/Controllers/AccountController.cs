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
    }
}
