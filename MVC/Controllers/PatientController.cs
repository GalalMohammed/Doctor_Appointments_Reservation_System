using Microsoft.AspNetCore.Mvc;
using MVC.ViewModels;

namespace MVC.Controllers
{
    public class PatientController : Controller
    {
        public IActionResult Profile(int? id)
        {
            if (id == null)
                throw new Exception();
            var test = new PatientViewModel()
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                BirthDate = new DateOnly(2001, 1, 1),
                City = City.City1,
                Email = "test@example.com",
                OldPassword = "1234",
                PhoneNumber = "01234567891",
                Appointments = new List<AppointmentViewModel>()
                {
                    new AppointmentViewModel()
                    {
                        DateTime = DateTime.Now.AddDays(3),
                        Doctor = "Test Doctor",
                        Specialty = "Cardiology",
                        DoctorImagePath = "d.png",
                        Location = City.City1
                    },
                    new AppointmentViewModel()
                    {
                        DateTime = DateTime.Now.AddDays(3),
                        Doctor = "Test Doctor",
                        Specialty = "Cardiology",
                        DoctorImagePath = "d.png",
                        Location = City.City1
                    },
                    new AppointmentViewModel()
                    {
                        DateTime = DateTime.Now.AddDays(3),
                        Doctor = "Test Doctor",
                        Specialty = "Cardiology",
                        DoctorImagePath = "d.png",
                        Location = City.City1
                    },
                    new AppointmentViewModel()
                    {
                        DateTime = DateTime.Now.AddDays(3),
                        Doctor = "Test Doctor",
                        Specialty = "Cardiology",
                        DoctorImagePath = "d.png",
                        Location = City.City1
                    },
                    new AppointmentViewModel()
                    {
                        DateTime = DateTime.Now.AddDays(3),
                        Doctor = "Test Doctor",
                        Specialty = "Cardiology",
                        DoctorImagePath = "d.png",
                        Location = City.City1
                    },
                    new AppointmentViewModel()
                    {
                        DateTime = DateTime.Now.AddDays(3),
                        Doctor = "Test Doctor",
                        Specialty = "Cardiology",
                        DoctorImagePath = "d.png",
                        Location = City.City1
                    },
                    new AppointmentViewModel()
                    {
                        DateTime = DateTime.Now.AddDays(3),
                        Doctor = "Test Doctor",
                        Specialty = "Cardiology",
                        DoctorImagePath = "d.png",
                        Location = City.City1
                    },
                    new AppointmentViewModel()
                    {
                        DateTime = DateTime.Now.AddDays(3),
                        Doctor = "Test Doctor",
                        Specialty = "Cardiology",
                        DoctorImagePath = "d.png",
                        Location = City.City1
                    },
                    new AppointmentViewModel()
                    {
                        DateTime = DateTime.Now.AddDays(3),
                        Doctor = "Test Doctor",
                        Specialty = "Cardiology",
                        DoctorImagePath = "d.png",
                        Location = City.City1
                    },
                    new AppointmentViewModel()
                    {
                        DateTime = DateTime.Now.AddDays(3),
                        Doctor = "Test Doctor",
                        Specialty = "Cardiology",
                        DoctorImagePath = "d.png",
                        Location = City.City1
                    },
                    new AppointmentViewModel()
                    {
                        DateTime = DateTime.Now.AddDays(3),
                        Doctor = "Test Doctor",
                        Specialty = "Cardiology",
                        DoctorImagePath = "d.png",
                        Location = City.City1
                    },
                    new AppointmentViewModel()
                    {
                        DateTime = DateTime.Now.AddDays(3),
                        Doctor = "Test Doctor",
                        Specialty = "Cardiology",
                        DoctorImagePath = "d.png",
                        Location = City.City1
                    },
                    new AppointmentViewModel()
                    {
                        DateTime = DateTime.Now.AddDays(3),
                        Doctor = "Test Doctor",
                        Specialty = "Cardiology",
                        DoctorImagePath = "d.png",
                        Location = City.City1
                    }
                }
            };
            return View(test);
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
