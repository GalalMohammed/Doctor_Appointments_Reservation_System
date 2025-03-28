using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    public class PatientController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
