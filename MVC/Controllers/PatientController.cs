using BLLServices.Managers.AppointmentManager;
using BLLServices.Managers.PatientManger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC.Mappers;
using MVC.ViewModels;


namespace MVC.Controllers
{
    [Authorize(Roles = "patient")]
    public class PatientController : Controller
    {
        private readonly IPatientManger patientManager;
        private readonly PatientMapper patientMapper;
        private readonly IAppointmentManager appointmentManager;

        public PatientController(IPatientManger patientManager, PatientMapper patientMapper, IAppointmentManager appointmentManager)
        {
            this.patientManager = patientManager;
            this.patientMapper = patientMapper;
            this.appointmentManager = appointmentManager;
        }
        public async Task<IActionResult> Profile()
        {
            var patient = patientMapper.MapToPatientViewModel(await patientManager.GetPatientInfo(int.Parse(User.FindFirst("currentId").Value)));
            return View(patient);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PatientViewModel patient)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Success = true;
                patient.Id = int.Parse(User.FindFirst("currentId").Value);
                await patientManager.UpdatePatient(await patientMapper.MapToPatient(patient));
                var modifiedPatient = patientMapper.MapToPatientViewModel(await patientManager.GetPatientInfo(int.Parse(User.FindFirst("currentId").Value)));
                return View("Profile", modifiedPatient);
            }
            ViewBag.Success = false;
            return View("Profile", patient);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CancelAppointment(int _, int appointmentId)
        {
            await appointmentManager.DeleteAppointmentAsync(appointmentId);
            return Ok();
        }
    }
}
