using AspNetCoreGeneratedDocument;
using BLLServices.Managers.DoctorManger;
using Microsoft.AspNetCore.Mvc;
using MVC.Enums;
using MVC.Mappers;
using MVC.ViewModels;

namespace MVC.Controllers
{
    public class DoctorController : Controller
    {
        private IDoctorManager _doctorManager;
        private IDoctorMapper _doctorMapper;

        public DoctorController(IDoctorManager doctorManager ,IDoctorMapper doctorMapper)
        {
            _doctorManager = doctorManager;
            _doctorMapper = doctorMapper;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Profile(int ID, int pageNum = 0, int pageSize = 10, bool? reviews = false)
        {
            var doctor = _doctorManager.GetDoctorByID(ID).Result;
            if (doctor == null) return NotFound();
            var doctorVM = _doctorMapper.MapToDoctorProfileVM(doctor);
            return View(doctorVM);

            //if(ID == 0) return BadRequest();
            //var doctor = new doctorProfileVM()
            //{
            //    ID = 1,
            //    Name = "Peter Doe",
            //    Title = "Dentist",
            //    Image = "maleDoc.jpg",
            //    Gender = Gender.Male,
            //    Qualifications = "BDS, MDS",
            //    Fees = 58,
            //    Specialties = new List<string> { "Orthodontist", "Endodontist", "Cosmetic Dentist" },
            //    Rating = 4.5f,
            //    Experience = 7,
            //    Governorate = Governorate.Menofia,
            //    Location = "Shebin El Kom",
            //    Phone = "23123322",
            //    Ratings = GenerateRatings(100,1),
            //    Appointments = GenerateAppointments(),
            //    Latitude = 30.0444,
            //    Longitude = 31.2357

            //};

            //doctor.Appointments = doctor.Appointments.Skip((int)DateTime.Now.DayOfWeek) // Start from today
            //                       .Concat(doctor.Appointments.Take((int)DateTime.Now.DayOfWeek)) // Append previous days at the end
            //                       .ToList();

            //ViewBag.reviews = reviews;
            //ViewBag.pageNums = Math.Ceiling((double)doctor.Ratings.Count / pageSize);
            //ViewBag.currentPage = pageNum + 1;

            //doctor.Ratings = doctor.Ratings.Skip(pageNum * pageSize).Take(pageSize).ToList();
            //return View(doctor);
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
                d.Experience >= minYear &&
                d.Fees >= minPrice && d.Fees <= maxPrice
            )
            .Skip(pageNum * pageSize).Take(pageSize).ToList();


            double docNums = DoctorMockData.GetDoctors()
                .Where(d =>
                (string.IsNullOrEmpty(docSearch) || d.Name.Contains(docSearch) || d.Specialties.Any(s => s.Contains(docSearch))) &&
                (gender == Gender.All || d.Gender == gender) &&
                (governorate == Governorate.All || d.Governorate == governorate) &&
                d.Experience >= minYear &&
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
                Experience = 7,
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
                Experience = 10,
                Governorate = Governorate.Cairo,
                Location = "New Cairo",
                Phone = "55889977",
                Appointments = GenerateAppointments()
            },
            // 25 more doctors with varied data...
        }.Concat(GenerateMoreDoctors(98)).ToList();
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
                    Experience = random.Next(1, 30),
                    Governorate = governorates[random.Next(governorates.Count)],
                    Location = "Random City",
                    Phone = random.Next(10000000, 99999999).ToString(),
                    Appointments = GenerateAppointments()
                }).ToList();
            }

            
        }
        private static List<DoctorReservationViewModel> GenerateAppointments()
        {
            var random = new Random();
            return Enumerable.Range(0, 7).Select(day => new DoctorReservationViewModel
            {
                Day = day,
                Time = random.Next(0, 2) == 0 ? null : $"{random.Next(8, 12)}:00 AM|{random.Next(1, 6)}:00 PM"
            }).ToList();
        }

        static List<Rating> GenerateRatings(int count, int doctorId)
        {
            Random rnd = new Random();
            string[] names = { "John", "Sarah", "Michael", "Emily", "Daniel", "Emma", "David", "Sophia" };
            string[] reviews = {
            "Excellent doctor, very professional!",
            "Had a great experience, highly recommend!",
            "Doctor was kind and patient.",
            "Not the best experience, but okay overall.",
            "Very knowledgeable and helpful.",
            "I wouldn't go again.",
            "Top-notch service, very friendly.",
            "Wait time was too long, but doctor was good."
        };

            List<Rating> ratings = new List<Rating>();

            for (int i = 1; i <= count; i++)
            {
                ratings.Add(new Rating
                {
                    ID = i,
                    PatientName = names[rnd.Next(names.Length)] + " " + (char)('A' + rnd.Next(26)) + $" {i}.",
                    Review = reviews[rnd.Next(reviews.Length)],
                    Rate = 1 + (rnd.Next(0, 9) * 0.5f), // Generates values: 1, 1.5, 2, ..., 4.5, 5
                    DocID = doctorId,
                    Date = RandomDate(rnd) // Generate a random formatted date
                });

            }

            return ratings;
        }

        private static string RandomDate(Random rnd)
        {
            DateTime startDate = new DateTime(2023, 1, 1); // Start from Jan 1, 2023
            DateTime endDate = DateTime.Today; // Until today
            int range = (endDate - startDate).Days;
            DateTime randomDate = startDate.AddDays(rnd.Next(range));

            return randomDate.ToString("ddd dd MMM yyyy"); // Format: Tue 25 Aug 2023
        }


    }
}
