using AspNetCoreGeneratedDocument;
using BLLServices.Managers.DoctorManger;
using BLLServices.Managers.SpecialtyManager;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC.Enums;
using MVC.Mappers;
using MVC.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Text.Json;
using System.Threading.Tasks;
using vezeetaApplicationAPI.Models;

namespace MVC.Controllers
{
    public class DoctorController : Controller
    {
        private IDoctorManager _doctorManager;
        private IDoctorMapper _doctorMapper;
        private ISpecialtyManager _specialityManager;

        public DoctorController(IDoctorManager doctorManager ,IDoctorMapper doctorMapper ,ISpecialtyManager specialtyManager)
        {
            _doctorManager = doctorManager;
            _doctorMapper = doctorMapper;
            _specialityManager = specialtyManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Profile(int ID, int pageNum = 0, int pageSize = 10, string? tab = "details")
        {
            var doctor = _doctorManager.GetDoctorByID(ID).Result;
            if (doctor == null) return NotFound();
            var doctorVM = _doctorMapper.MapToDoctorProfileVM(doctor).Result;

            ViewBag.tab = tab;
            ViewBag.pageNums = Math.Ceiling((double)doctorVM.Ratings.Count / pageSize);
            ViewBag.currentPage = pageNum + 1;

            var calReserves = doctor.DoctorReservations
                .Where(doc => doc.StartTime.Date > DateTime.Now.Date)
                .Select(_doctorMapper.MapToCalenderReservationVM)
                .ToList();
            for (int i = 1; i <= 15; i++)
            {
                var date = DateTime.Now.AddDays(i).Date;
                if(!calReserves.Any(cal => cal.to.Date == date))
                {
                    calReserves.Add(new CalenderReservationVM()
                    {
                        to = date,
                        from = date,
                        title = "Add Work +",
                        isAllDay = true,
                    });
                }
            }

            ViewBag.cal = JsonSerializer.Serialize(calReserves);
            //int today = DateTime.Now.Day;
            //for (int i = today; i < today + 14; i++)
            //{
            //    if (!doctorVM.Appointments.Any(app => app.Day == i))
            //    {
            //        doctorVM.Appointments.Add(new DoctorReservationViewModel() { Day = i, Time = null,IsAvailable = true });
            //    }
            //}
            doctorVM.Appointments.Sort((a, b) => a.Day - b.Day);
            doctorVM.Ratings = doctorVM.Ratings.Skip(pageNum * pageSize).Take(pageSize).ToList();
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
        public async Task<IActionResult> Search(FilterSearchVM search)
        {
            search.Name = search.Name?.Trim().ToLower() ?? "";

            Expression<Func<Doctor, bool>> condition = doc =>
                    (search.Name == "" || (doc.FirstName + " " + doc.LastName).ToLower().Trim().Contains(search.Name)) &&
                    (search.Speciality == 0 || doc.SpecialtyID == search.Speciality) &&
                    (search.Gender == Gender.All || doc.Gender == search.Gender) &&
                    (search.Governorate == Governorate.All || doc.Governorate == search.Governorate) &&
                    doc.Fees >= search.MinPrice && doc.Fees <= search.MaxPrice && doc.WaitingTime <= search.WaitingTime;
                    

            var doctorsTask = await _doctorManager.GetDoctorConditionByPage(search.PageNum, search.PageSize,condition);

            var doctors = new List<docSearchVM>();
            foreach(var doc in doctorsTask)
            {
                var singleDoc = await _doctorMapper.MapToDocSearchVMAsync(doc);
                doctors.Add(singleDoc);
            }


            double docNums = await _doctorManager.GetDoctorConditionCount(condition);

            var specialites = (await _specialityManager.GetAllSpecialties()).Select(_doctorMapper.MapToSpecialityVM).ToList();
            specialites.Insert(0, new SpecialityVM() { ID = 0,Name ="All"});

            ViewBag.PageNums = Math.Ceiling(docNums / search.PageSize);
            ViewBag.currentPage = search.PageNum + 1;
            ViewBag.Name = search.Name; // remember to change the view viewbags
            ViewBag.Speciality = search.Speciality;
            ViewBag.governorate = search.Governorate;
            ViewBag.gender = search.Gender;
            ViewBag.waitingTime = search.WaitingTime; //
            ViewBag.minPrice = search.MinPrice;
            ViewBag.maxPrice = search.MaxPrice;
            ViewBag.Specialites = new SelectList(specialites,"ID","Name",search.Speciality); //

            return View(doctors);
        }

        [HttpPost("Add-Reservation")]
        [ValidateAntiForgeryToken]
        public IActionResult AddReservation(NewResVM res)
        {
            if (res.StartTime >= res.EndTime)
            {
                ModelState.AddModelError("Start Time", "Start time must be before End time.");
            }
            else if (res.EndTime - res.StartTime < TimeSpan.FromMinutes(30))
            {
                ModelState.AddModelError("End Time", "The difference between Start time and End time must be at least 30 minutes.");
            }

            if (ModelState.IsValid)
            {
                var startTime = res.Date.Date.Add(res.StartTime);
                var endTime = res.Date.Date.Add(res.EndTime);
                // DB Logic
                if(res.ResID == 0)
                {
                    TempData["Added"] = $"Reservation on {res.Date.ToString("dddd, dd MMMM yyyy")} from {startTime.ToString("hh:mm tt")} to {endTime.ToString("hh:mm tt")} is added";
                }
                else
                {
                    TempData["Updated"] = $"Reservation on {res.Date.ToString("dddd, dd MMMM yyyy")} has been updated to be from {startTime.ToString("hh:mm tt")} to {endTime.ToString("hh:mm tt")}";
                }
                
            }
            else
            {
                TempData["Error"] = string.Join("\n",
                    ModelState
                        .Where(m => m.Value.Errors.Any())
                        .SelectMany(m => m.Value.Errors.Select(e => $"{m.Key}: {e.ErrorMessage}\n"))
                );
            }
                return RedirectToAction("profile", "Doctor", new { id = res.ID, tab = "calender" });
        }

        //public static class DoctorMockData
        //{
        //    public static List<docSearchVM> GetDoctors()
        //    {
        //        return new List<docSearchVM>
        //{
        //    new docSearchVM
        //    {
        //        ID = 1,
        //        Name = "Peter Doe",
        //        Title = "Dentist",
        //        Image = "maleDoc.jpg",
        //        Gender = Gender.Male,
        //        Qualifications = "BDS, MDS",
        //        Fees = 58,
        //        Specialties = new List<string> { "Orthodontist", "Endodontist", "Cosmetic Dentist" },
        //        Rating = 4.5f,
        //        Experience = 7,
        //        Governorate = Governorate.Menofia,
        //        Location = "Shebin El Kom",
        //        Phone = "23123322",
        //        Appointments = GenerateAppointments()
        //    },
        //    new docSearchVM
        //    {
        //        ID = 2,
        //        Name = "Sarah Adams",
        //        Title = "Cardiologist",
        //        Image = "femaleDoc.jpg",
        //        Gender = Gender.Female,
        //        Qualifications = "MBBS, MD",
        //        Fees = 120,
        //        Specialties = new List<string> { "Heart Specialist", "Internal Medicine" },
        //        Rating = 4.8f,
        //        Experience = 10,
        //        Governorate = Governorate.Cairo,
        //        Location = "New Cairo",
        //        Phone = "55889977",
        //        Appointments = GenerateAppointments()
        //    },
        //    // 25 more doctors with varied data...
        //}.Concat(GenerateMoreDoctors(98)).ToList();
        //    }

        //    private static List<docSearchVM> GenerateMoreDoctors(int count)
        //    {
        //        var random = new Random();
        //        var governorates = Enum.GetValues(typeof(Governorate)).Cast<Governorate>().ToList();
        //        var specialties = new List<string> { "Dermatologist", "Pediatrician", "Neurologist", "Surgeon", "Ophthalmologist" };

        //        return Enumerable.Range(3, count).Select(id => new docSearchVM
        //        {
        //            ID = id,
        //            Name = $"Doctor {id}",
        //            Title = specialties[random.Next(specialties.Count)],
        //            Image = id % 2 == 0 ? "maleDoc.jpg" : "femaleDoc.jpg",
        //            Gender = id % 2 == 0 ? Gender.Male : Gender.Female,
        //            Qualifications = "MBBS, MD",
        //            Fees = random.Next(50, 10000),
        //            Specialties = new List<string> { specialties[random.Next(specialties.Count)] },
        //            Rating = (float)Math.Round(random.NextDouble() * 2 + 3, 1), // Random rating between 3.0 and 5.0 with 1 decimal place
        //            Experience = random.Next(1, 30),
        //            Governorate = governorates[random.Next(governorates.Count)],
        //            Location = "Random City",
        //            Phone = random.Next(10000000, 99999999).ToString(),
        //            Appointments = GenerateAppointments()
        //        }).ToList();
        //    }

            
        //}
        //private static List<DoctorReservationViewModel> GenerateAppointments()
        //{
        //    var random = new Random();
        //    return Enumerable.Range(0, 7).Select(day => new DoctorReservationViewModel
        //    {
        //        Day = day,
        //        Time = random.Next(0, 2) == 0 ? null : $"{random.Next(8, 12)}:00 AM|{random.Next(1, 6)}:00 PM"
        //    }).ToList();
        //}

        //static List<Rating> GenerateRatings(int count, int doctorId)
        //{
        //    Random rnd = new Random();
        //    string[] names = { "John", "Sarah", "Michael", "Emily", "Daniel", "Emma", "David", "Sophia" };
        //    string[] reviews = {
        //    "Excellent doctor, very professional!",
        //    "Had a great experience, highly recommend!",
        //    "Doctor was kind and patient.",
        //    "Not the best experience, but okay overall.",
        //    "Very knowledgeable and helpful.",
        //    "I wouldn't go again.",
        //    "Top-notch service, very friendly.",
        //    "Wait time was too long, but doctor was good."
        //};

        //    List<Rating> ratings = new List<Rating>();

        //    for (int i = 1; i <= count; i++)
        //    {
        //        ratings.Add(new Rating
        //        {
        //            ID = i,
        //            PatientName = names[rnd.Next(names.Length)] + " " + (char)('A' + rnd.Next(26)) + $" {i}.",
        //            Review = reviews[rnd.Next(reviews.Length)],
        //            Rate = 1 + (rnd.Next(0, 9) * 0.5f), // Generates values: 1, 1.5, 2, ..., 4.5, 5
        //            DocID = doctorId,
        //            Date = RandomDate(rnd) // Generate a random formatted date
        //        });

        //    }

        //    return ratings;
        //}

        //private static string RandomDate(Random rnd)
        //{
        //    DateTime startDate = new DateTime(2023, 1, 1); // Start from Jan 1, 2023
        //    DateTime endDate = DateTime.Today; // Until today
        //    int range = (endDate - startDate).Days;
        //    DateTime randomDate = startDate.AddDays(rnd.Next(range));

        //    return randomDate.ToString("ddd dd MMM yyyy"); // Format: Tue 25 Aug 2023
        //}


    }
}
