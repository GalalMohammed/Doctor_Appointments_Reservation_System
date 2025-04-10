using BLLServices.Managers.PatientManger;
using Microsoft.AspNetCore.Mvc;
using MVC.Mappers;
using MVC.ViewModels;


namespace MVC.Controllers
{
    public class PatientController : Controller
    {
        private readonly IPatientManger patientManager;
        private readonly PatientMapper patientMapper;

        public PatientController(IPatientManger patientManager, PatientMapper patientMapper)
        {
            this.patientManager = patientManager;
            this.patientMapper = patientMapper;
        }
        public async Task<IActionResult> Profile()
        {
            var claims = User.Claims.ToList();
            var patient = patientMapper.MapToPatientViewModel(await patientManager.GetPatientInfo(int.Parse(User.FindFirst("currentId").Value)));
            return View(patient);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(PatientViewModel patient)
        {
            //ModelState.AddModelError("", "Test Error");
            //ModelState.AddModelError("", "Test Error");
            //ModelState.AddModelError("", "Test Error");
            //ModelState.AddModelError("", "Test Error");
            ViewBag.Success = ModelState.IsValid;
            return View("Profile", patient);
        }
    }
}
