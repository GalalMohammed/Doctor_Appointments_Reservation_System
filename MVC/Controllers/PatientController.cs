using BLLServices.Managers.AppointmentManager;
using BLLServices.Managers.OrderManager;
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
        private readonly IOrderManager orderManager;

        public PatientController(IPatientManger patientManager,
                                 PatientMapper patientMapper,
                                 IAppointmentManager appointmentManager,
                                 IOrderManager orderManager
                                 )
        {
            this.patientManager = patientManager;
            this.patientMapper = patientMapper;
            this.appointmentManager = appointmentManager;
            this.orderManager = orderManager;
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
        [HttpGet("/patient/add-appointment")]
        public async Task<IActionResult> AddAppointment(int patientId, int doctorReservationId)
        {
            if (orderManager.IsOrderPaid(patientId, doctorReservationId))
            {
                await appointmentManager.AddAppointment(patientId, doctorReservationId);
                ViewBag.OrderSucceeded = true;
            }
            var patient = await patientManager.GetPatientInfo(patientId);
            if (!ViewBag.OrderSucceeded)
            {
                orderManager.DeleteOrder(patientId, doctorReservationId);
                ViewBag.OrderSucceeded = false;
            }
            return View("Profile", patientMapper.MapToPatientViewModel(patient));
        }
        public async Task<IActionResult> CancelAppointment(int appointmentId)
        {
            orderManager.DeleteOrder(int.Parse(User.FindFirst("currentId").Value),(int) await appointmentManager.GetReservationId(appointmentId));
            await appointmentManager.DeleteAppointmentAsync(appointmentId);
            return RedirectToAction("Profile");
        }
    }
}
