using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using System.Diagnostics;
using MVC.ViewModels;

namespace MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            HomeVM homeVM;
            #region CodeFromDB

            #endregion

            #region FakeModels
            homeVM = GenerateFakeHomeVM();

            #endregion


            return View(homeVM);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



        #region FakeDateGenrator
        public static HomeVM GenerateFakeHomeVM()
        {
            return new HomeVM
            {
                Specialties = new List<string> { "Cardiology", "Dermatology", "Neurology", "Pediatrics", "Orthopedics", "Oncology", "Gastroenterology", "Endocrinology", "Psychiatry", "Urology", "Rheumatology", "Nephrology", "Pulmonology", "Hematology" },
                Doctors = new List<HomeDoctor>
            {
                new HomeDoctor { ID = 1, Name = "Dr. John Doe", Specialty = "Cardiology", Image = "doctor1.jpg", Rating = 4.8f, Location = "New York" },
                new HomeDoctor { ID = 1, Name = "Dr. John Doe", Specialty = "Cardiology", Image = "doctor1.jpg", Rating = 4.8f, Location = "New York" },
                new HomeDoctor { ID = 1, Name = "Dr. John Doe", Specialty = "Cardiology", Image = "doctor1.jpg", Rating = 4.8f, Location = "New York" },
                new HomeDoctor { ID = 2, Name = "Dr. Jane Smith", Specialty = "Dermatology", Image = "doctor2.jpg", Rating = 4.6f, Location = "Los Angeles" },
                new HomeDoctor { ID = 3, Name = "Dr. Emily Johnson", Specialty = "Neurology", Image = "doctor3.jpg", Rating = 4.9f, Location = "Chicago" },
                new HomeDoctor { ID = 4, Name = "Dr. Mark Brown", Specialty = "Pediatrics", Image = "doctor4.jpg", Rating = 4.7f, Location = "Houston" },
                new HomeDoctor { ID = 5, Name = "Dr. Alex Turner", Specialty = "Orthopedics", Image = "doctor5.jpg", Rating = 4.5f, Location = "San Francisco" },
                new HomeDoctor { ID = 6, Name = "Dr. Lisa White", Specialty = "Oncology", Image = "doctor6.jpg", Rating = 4.8f, Location = "Seattle" },
                new HomeDoctor { ID = 7, Name = "Dr. Robert Green", Specialty = "Gastroenterology", Image = "doctor7.jpg", Rating = 4.7f, Location = "Boston" },
                new HomeDoctor { ID = 8, Name = "Dr. Sophia Black", Specialty = "Endocrinology", Image = "doctor8.jpg", Rating = 4.6f, Location = "Denver" },
                new HomeDoctor { ID = 9, Name = "Dr. Michael Gray", Specialty = "Psychiatry", Image = "doctor9.jpg", Rating = 4.9f, Location = "Miami" },
                new HomeDoctor { ID = 10, Name = "Dr. Olivia Adams", Specialty = "Urology", Image = "doctor10.jpg", Rating = 4.5f, Location = "Atlanta" },
                new HomeDoctor { ID = 11, Name = "Dr. Ethan Clark", Specialty = "Cardiology", Image = "doctor11.jpg", Rating = 4.7f, Location = "Austin" },
                new HomeDoctor { ID = 12, Name = "Dr. Ava Martinez", Specialty = "Dermatology", Image = "doctor12.jpg", Rating = 4.8f, Location = "San Diego" },
                new HomeDoctor { ID = 13, Name = "Dr. Noah Wilson", Specialty = "Neurology", Image = "doctor13.jpg", Rating = 3.6f, Location = "Philadelphia" },
                new HomeDoctor { ID = 14, Name = "Dr. Emma Davis", Specialty = "Pediatrics", Image = "doctor14.jpg", Rating = 2.9f, Location = "Phoenix" },
                new HomeDoctor { ID = 15, Name = "Dr. Liam Thompson", Specialty = "Orthopedics", Image = "doctor15.jpg", Rating = 4.5f, Location = "Dallas" }
            }
            };
        }
        #endregion


    }
}