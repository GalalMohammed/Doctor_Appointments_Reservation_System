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


    }
}
