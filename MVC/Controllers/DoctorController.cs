using Microsoft.AspNetCore.Mvc;
using MVC.Enums;
using MVC.ViewModels;

namespace MVC.Controllers
{
    public class DoctorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Search(
            int pageNum = 0,int pageSize = 10,string docSearch = "", 
            Governorate governorate = Governorate.All,Gender gender = Gender.All,int minYear = 0,
            double minPrice = 0 , double maxPrice = 10000)
        {
            List<docSearchVM> doctors = DoctorMockData.GetDoctors()
            .Where(d =>
                (string.IsNullOrEmpty(docSearch) || d.Name.ToLower().Contains(docSearch.ToLower()) || d.Specialties.Any(s => s.ToLower().Contains(docSearch.ToLower()))) &&
                (gender == Gender.All || d.Gender == gender) &&
                (governorate == Governorate.All || d.Governorate == governorate) &&
                d.Experiance >= minYear &&
                d.Fees >= minPrice && d.Fees <= maxPrice
            )
            .Skip(pageNum * pageSize).Take(pageSize).ToList();


            double docNums = DoctorMockData.GetDoctors()
                .Where(d =>
                (string.IsNullOrEmpty(docSearch) || d.Name.Contains(docSearch) || d.Specialties.Any(s => s.Contains(docSearch))) &&
                (gender == Gender.All || d.Gender == gender) &&
                (governorate == Governorate.All || d.Governorate == governorate) &&
                d.Experiance >= minYear &&
                d.Fees >= minPrice && d.Fees <= maxPrice
                ).Count();

            ViewBag.PageNums = Math.Ceiling(docNums / pageSize);
            ViewBag.currentPage = pageNum + 1;
            ViewBag.docSearch = docSearch;
            ViewBag.governorate = governorate;
            ViewBag.gender = gender;
            ViewBag.minYear = minYear;
            ViewBag.minPrice = minPrice;
            ViewBag.maxPrice = maxPrice;


            var today = (int)DateTime.Now.DayOfWeek;
            foreach (var doc in doctors)
            {
                doc.Appointments = doc.Appointments.Skip(today) // Start from today
                                   .Concat(doc.Appointments.Take(today)) // Append previous days at the end
                                   .ToList();
            }
            return View(doctors);
        }

        public static class DoctorMockData
        {
            public static List<docSearchVM> GetDoctors()
            {
                return new List<docSearchVM>
        {
            new docSearchVM
            {
                ID = 1,
                Name = "Peter Doe",
                Title = "Dentist",
                Image = "maleDoc.jpg",
                Gender = Gender.Male,
                Qualifications = "BDS, MDS",
                Fees = 58,
                Specialties = new List<string> { "Orthodontist", "Endodontist", "Cosmetic Dentist" },
                Rating = 4.5f,
                Experiance = 7,
                Governorate = Governorate.Menofia,
                Location = "Shebin El Kom",
                Phone = "23123322",
                Appointments = GenerateAppointments()
            },
            new docSearchVM
            {
                ID = 2,
                Name = "Sarah Adams",
                Title = "Cardiologist",
                Image = "femaleDoc.jpg",
                Gender = Gender.Female,
                Qualifications = "MBBS, MD",
                Fees = 120,
                Specialties = new List<string> { "Heart Specialist", "Internal Medicine" },
                Rating = 4.8f,
                Experiance = 10,
                Governorate = Governorate.Cairo,
                Location = "New Cairo",
                Phone = "55889977",
                Appointments = GenerateAppointments()
            },
            // 25 more doctors with varied data...
        }.Concat(GenerateMoreDoctors(100)).ToList();
            }

            private static List<docSearchVM> GenerateMoreDoctors(int count)
            {
                var random = new Random();
                var governorates = Enum.GetValues(typeof(Governorate)).Cast<Governorate>().ToList();
                var specialties = new List<string> { "Dermatologist", "Pediatrician", "Neurologist", "Surgeon", "Ophthalmologist" };

                return Enumerable.Range(3, count).Select(id => new docSearchVM
                {
                    ID = id,
                    Name = $"Doctor {id}",
                    Title = specialties[random.Next(specialties.Count)],
                    Image = id % 2 == 0 ? "maleDoc.jpg" : "femaleDoc.jpg",
                    Gender = id % 2 == 0 ? Gender.Male : Gender.Female,
                    Qualifications = "MBBS, MD",
                    Fees = random.Next(50, 10000),
                    Specialties = new List<string> { specialties[random.Next(specialties.Count)] },
                    Rating = (float)Math.Round(random.NextDouble() * 2 + 3, 1), // Random rating between 3.0 and 5.0 with 1 decimal place
                    Experiance = random.Next(1, 30),
                    Governorate = governorates[random.Next(governorates.Count)],
                    Location = "Random City",
                    Phone = random.Next(10000000, 99999999).ToString(),
                    Appointments = GenerateAppointments()
                }).ToList();
            }

            private static List<Appointment> GenerateAppointments()
            {
                var random = new Random();
                return Enumerable.Range(0, 7).Select(day => new Appointment
                {
                    Day = day,
                    Time = random.Next(0, 2) == 0 ? null : $"{random.Next(8, 12)}:00 AM|{random.Next(1, 6)}:00 PM"
                }).ToList();
            }
        }

    }
}
