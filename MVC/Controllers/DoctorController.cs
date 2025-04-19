using BLLServices.Common.UploadService;
using BLLServices.Managers.DoctorManger;
using BLLServices.Managers.DoctorReservationManager;
using BLLServices.Managers.SpecialtyManager;
using DAL.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC.Enums;
using MVC.Mappers;
using MVC.ViewModels;
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
        private readonly ISpecialtyManager _specialityManager;
        private readonly IUploadService uploadService;
        private readonly IDoctorReservationManager doctorReservationManager;
        public DoctorController(IDoctorManager doctorManager,
                                IDoctorMapper doctorMapper,
                                ISpecialtyManager _specialityManager,
                                IUploadService uploadService,
                                IDoctorReservationManager doctorReservationManager)
        {
            _doctorManager = doctorManager;
            _doctorMapper = doctorMapper;
            this._specialityManager = _specialityManager;
            this.uploadService = uploadService;
            this.doctorReservationManager = doctorReservationManager;
        }

        [HttpGet]
        public IActionResult Profile(int? ID, int pageNum = 0, int pageSize = 10, string? tab = "details")
        {
            if (User.IsInRole("doctor"))
            {
                ViewBag.ID = ID == null || ID == int.Parse(User.FindFirst("currentId").Value);
                if (ID == null) ID = int.Parse(User.FindFirst("currentId").Value);
            }
            var doctor = _doctorManager.GetDoctorByID(ID.Value).Result;
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
                if (!calReserves.Any(cal => cal.to.Date == date))
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


            var doctorsTask = await _doctorManager.GetDoctorConditionByPage(search.PageNum, search.PageSize, condition);

            var doctors = new List<docSearchVM>();
            foreach (var doc in doctorsTask)
            {
                var singleDoc = await _doctorMapper.MapToDocSearchVMAsync(doc);
                doctors.Add(singleDoc);
            }


            double docNums = await _doctorManager.GetDoctorConditionCount(condition);

            var specialites = (await _specialityManager.GetAllSpecialties()).Select(_doctorMapper.MapToSpecialityVM).ToList();
            specialites.Insert(0, new SpecialityVM() { ID = 0, Name = "All" });
            

            ViewBag.PageNums = Math.Ceiling(docNums / search.PageSize);
            ViewBag.currentPage = search.PageNum + 1;
            ViewBag.Name = search.Name ?? string.Empty; // remember to change the view viewbags
            ViewBag.Speciality = search.Speciality;
            ViewBag.governorate = search.Governorate;
            ViewBag.gender = search.Gender;
            ViewBag.waitingTime = search.WaitingTime; //
            ViewBag.minPrice = search.MinPrice;
            ViewBag.maxPrice = search.MaxPrice;
            ViewBag.Specialites = new SelectList(specialites, "ID", "Name", search.Speciality); //

            return View(doctors);
        }

        [Authorize(Roles = "doctor")]
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
                var NewDoctorReservation = _doctorMapper.MapFromNewResVM(res).Result;
                // DB Logic
                if (res.ResID == 0)
                {
                    NewDoctorReservation.StartTime = startTime.AddDays(1);
                    NewDoctorReservation.EndTime = endTime.AddDays(1);
                    doctorReservationManager.AddDoctorReservation(NewDoctorReservation);
                    TempData["Added"] = $"Reservation on {res.Date.ToString("dddd, dd MMMM yyyy")} from {startTime.ToString("hh:mm tt")} to {endTime.ToString("hh:mm tt")} is added";
                }
                else
                {
                    doctorReservationManager.EditDoctorReservation(NewDoctorReservation);
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

        [Authorize(Roles = "doctor")]
        [HttpPost("Delete-Reservation")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteReservation(int ResID)
        {
            var ID = int.Parse(User.FindFirst("currentId").Value);
            var res = await doctorReservationManager.GetDoctorReservationByID(ResID);
            if(res == null)
            {
                TempData["Error"] = "This reservation doesn't exist";
            }
            else if(res.DoctorID != ID)
            {
                TempData["Error"] = "You aren't authorized to delete this reservation";
            }
            else
            {
                doctorReservationManager.DeleteDoctorReservation(res);
                TempData["Deleted"] = $"Reservation on {res.StartTime.Date.ToString("dddd, dd MMMM yyyy")} from {res.StartTime.ToString("hh:mm tt")} to {res.EndTime.ToString("hh:mm tt")} has been deleted";
            }
            return RedirectToAction("profile", "Doctor", new { id = ID, tab = "calender" });
        }

        [Authorize(Roles = "doctor")]
        public async Task<IActionResult> Edit()
        {
            var doctor = await _doctorManager.GetDoctorByID(int.Parse(User.FindFirst("currentId").Value));
            var viewModel = _doctorMapper.MapToDoctorEdit(doctor);
            return View(viewModel);
        }
        [Authorize(Roles = "doctor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DoctorEditViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (viewModel.Image != null)
                    viewModel.ImageURL = await uploadService.UploadFile(viewModel.Image, oldFilename: viewModel.ImageURL);
                var doctor = await _doctorMapper.MapToDoctorFromEdit(viewModel);
                await _doctorManager.UpdateDoctor(doctor);
                ViewBag.Success = true;
                return View(viewModel);
            }
            return View(viewModel);
        }
        [Authorize(Roles = "doctor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSchedule(ScheduleViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var doctor = await _doctorManager.GetDoctorByID(int.Parse(User.FindFirst("currentId").Value));
                doctor.DefaultStartTime = new DateTime(DateOnly.FromDateTime(DateTime.Now), viewModel.StartTime);
                doctor.DefaultEndTime = new DateTime(DateOnly.FromDateTime(DateTime.Now), viewModel.EndTime);
                //doctor.WorkingDays = (WorkingDays)Convert.ToInt32(string.Join("", viewModel.Days), 2);
                doctor.WorkingDays = (WorkingDays)viewModel.Days.Select(x=>Math.Pow(2,int.Parse(x))).Sum();
                doctor.DefaultMaxReservations = viewModel.ReservationQuota;
                await _doctorManager.UpdateDoctor(doctor);
                TempData["Updated"] = "Schedule Updated!";
                return RedirectToAction("profile", "Doctor", new { tab = "calender" });
            }
            TempData["Error"] = string.Join("\n",
                                ModelState
                                    .Where(m => m.Value.Errors.Any())
                                    .SelectMany(m => m.Value.Errors.Select(e => $"{m.Key}: {e.ErrorMessage}\n"))
                                );
            return RedirectToAction("profile", "Doctor", new { tab = "calender" });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddReview(AddReviewVM rev)
        {
            if(ModelState.IsValid)
            {

            }
            else
            {
                TempData["Error"] = string.Join("\n",
                    ModelState
                        .Where(m => m.Value.Errors.Any())
                        .SelectMany(m => m.Value.Errors.Select(e => $"{m.Key}: {e.ErrorMessage}\n"))
                );
            }
            return Json(rev);
        }                
    }
}