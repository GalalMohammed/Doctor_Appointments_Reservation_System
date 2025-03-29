using Microsoft.AspNetCore.Mvc;

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
            //if (token != null)
            //{
            //    int? id = GetIdFromToken(token);
            //    if (id != null)
            //    {
            //        SaveToDb(id);
            //        return View();
            //    }
            //}
            return View("CustomError");
        }
    }
}
