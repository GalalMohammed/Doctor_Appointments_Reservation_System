using BLLServices.Managers.AppointmentManager;
using BLLServices.Managers.DoctorManger;
using BLLServices.Managers.DoctorReservationManager;
using BLLServices.Managers.ReviewManager;
using BLLServices.Managers.SpecialtyManager;
using DAL.Enums;
using MVC.ViewModels;
using vezeetaApplicationAPI.Models;

namespace MVC.Mappers
{
    public class DoctorMapper : IDoctorMapper
    {
        private IReviewManager _reviewManager;
        private IDoctorManager _doctorManager;
        private IDoctorReservationManager _doctorReservationManager;
        private ISpecialtyManager _specialtyManager;
        private IAppointmentManager _appointmentManager;

        public DoctorMapper(IDoctorReservationManager doctorReservationManager, ISpecialtyManager specialtyManager
            , IDoctorManager doctorManager, IReviewManager reviewManager, IAppointmentManager appointmentManager)
        {
            _doctorReservationManager = doctorReservationManager;
            _specialtyManager = specialtyManager;
            _reviewManager = reviewManager;
            _doctorManager = doctorManager;
            _appointmentManager = appointmentManager;
        }

        public CalenderReservationVM MapToCalenderReservationVM(DoctorReservation reservation)
        {
            return new CalenderReservationVM()
            {
                ResID = reservation.ID,
                from = reservation.StartTime,
                to = reservation.EndTime,
                title = $"{reservation.StartTime.ToString("hh:mm tt")} to {reservation.EndTime.ToString("hh:mm tt")}",
                isAllDay = false,
                MaxRes = reservation.MaxReservation
            };
        }

        public SpecialityVM MapToSpecialityVM(Specialty specialty)
        {
            return new SpecialityVM()
            {
                ID = specialty.ID,
                Name = specialty.Name,
            };
        }

        public async Task<DoctorReservationViewModel> MapToDoctorReservationViewModelAsync(DoctorReservation reservation)
        {
            return new DoctorReservationViewModel
            {
                Day = reservation.EndTime.Day,
                Time = $"{reservation.StartTime.ToString("hh:mm tt")}|{reservation.EndTime.ToString("hh:mm tt")}",
                ResID = reservation.ID,
                IsAvailable = reservation.MaxReservation > await _appointmentManager.GetAppointmentsCountByDate(reservation.DoctorID, reservation.StartTime)
            };
        }
        public Task<DoctorReservation> MapFromDoctorReservationViewModel(DoctorReservationViewModel reservationVM)
        {
            return _doctorReservationManager.GetDoctorReservationByID(reservationVM.ResID);

        }
        public async Task<docSearchVM> MapToDocSearchVMAsync(Doctor doctor)
        {
            //var specialtiesList = specialties.Select(s=>s.Name).ToList();
            var avgRating = await _reviewManager.GetDoctorAverageRating(doctor.ID);
            var reservations = await _doctorReservationManager.GetReservationsByDocID(doctor.ID);
            List<DoctorReservationViewModel> appointments = new List<DoctorReservationViewModel>();
            foreach (var reservation in reservations)
            {
                appointments.Add(await MapToDoctorReservationViewModelAsync(reservation));
            }

            return new docSearchVM
            {
                ID = doctor.ID,
                Name = $"{doctor.FirstName} {doctor.LastName}",
                Title = string.Empty,
                Gender = doctor.Gender, // unite in one enum
                Image = doctor.ImageURL,
                Qualifications = string.Empty,
                Fees = (int)doctor.Fees, // remember to change it to double
                Speciality = doctor.Specialty.Name,//(await _specialtyManager.GetSpecialtyById(doctor.SpecialtyID)).Name,
                Rating = avgRating,
                WaitingTime = doctor.WaitingTime,
                Governorate = doctor.Governorate, // add to database
                Location = doctor.Location,
                Phone = doctor.AppUser?.PhoneNumber ?? "01203203320",
                Appointments = appointments
            };
        }
        public async Task<Doctor> MapFromDocSearchVM(docSearchVM doctorVM)
        {
            return await _doctorManager.GetDoctorByID(doctorVM.ID);
        }
        public async Task<doctorProfileVM> MapToDoctorProfileVM(Doctor doctor)
        {
            var specialties = await _specialtyManager.GetAllSpecialties();
            //var specialtiesList = specialties.Select(s => s.Name).ToList();
            var avgRating = await _reviewManager.GetDoctorAverageRating(doctor.ID);
            var reservations = await _doctorReservationManager.GetReservationsByDocID(doctor.ID);
            List<DoctorReservationViewModel> appointments = new List<DoctorReservationViewModel>();
            foreach (var reservation in reservations)
            {
                appointments.Add(await MapToDoctorReservationViewModelAsync(reservation));
            }
            var reviews = await _reviewManager.GetDoctorReviews(doctor.ID);
            List<Rating> ratings = new List<Rating>();
            foreach (var review in reviews)
            {
                ratings.Add(MapToRating(review));
            }
            var viewModel = new doctorProfileVM
            {
                ID = doctor.ID,
                Name = $"{doctor.FirstName} {doctor.LastName}",
                Title = string.Empty,
                Gender = doctor.Gender,
                Image = doctor.ImageURL,
                Qualifications = string.Empty,
                Fees = doctor.Fees, 
                Speciality = doctor.Specialty.Name, //(await _specialtyManager.GetSpecialtyById(doctor.SpecialtyID)).Name,
                Rating = avgRating,
                WaitingTime = doctor.WaitingTime, 
                Governorate = doctor.Governorate,
                Location = doctor.Location,
                Phone = doctor.AppUser?.PhoneNumber ?? "01203203320",
                Appointments = appointments,
                Ratings = ratings,
                Latitude = (float)doctor.Lat, // change it to float
                Longitude = (float)doctor.Lng,
                Schedule = new()
                {
                    ReservationQuota = doctor.DefaultMaxReservations,
                    StartTime = TimeOnly.FromDateTime(doctor.DefaultStartTime),
                    EndTime = TimeOnly.FromDateTime(doctor.DefaultEndTime),
                }
            };
            var workingDayString = Convert.ToString((int)doctor.WorkingDays, 2).PadLeft(7, '0').Reverse().ToArray();
            for (int i = 0; i < 7; i++)
                viewModel.Schedule.Days[i] = (workingDayString[i] == '1') ? "1" : "0";
            return viewModel;
        }
        public async Task<Doctor> MapFromDoctorProfileVM(doctorProfileVM doctorVM)
        {
            var doctor = await _doctorManager.GetDoctorByID(doctorVM.ID);
            doctor.Lat = (float)doctorVM.Latitude;
            doctor.Lng = (float)doctorVM.Longitude;
            return doctor;
        }
        public Rating MapToRating(Review review)
        {
            return new Rating
            {
                ID = review.ID,
                PatientName = $"{review.Patient.FirstName} {review.Patient.LastName}",
                Review = review.Description,
                Rate = review.Rate,
                Date = review.Date.ToString("yyyy-MM-dd"),
                DocID = review.DoctorID
            };
        }
        public Review MapFromRating(Rating rating)
        {
            return new Review
            {
                ID = rating.ID,
                Description = rating.Review,
                Rate = (int)rating.Rate,
                Date = DateTime.Parse(rating.Date),
                DoctorID = rating.DocID
            };
        }
        public NewResVM MapToNewResVM(DoctorReservation doctorReservation)
        {
            return new NewResVM()
            {
                ResID = doctorReservation.ID,
                ID = doctorReservation.DoctorID,
                Date = doctorReservation.StartTime.Date,
                StartTime = doctorReservation.StartTime.TimeOfDay,
                EndTime = doctorReservation.EndTime.TimeOfDay,
                MaxRes = doctorReservation.MaxReservation
            };
        }
        public async Task<DoctorReservation> MapFromNewResVM(NewResVM newRes)
        {
            var res = await _doctorReservationManager.GetDoctorReservationByID(newRes.ResID);
            if (res == null)
            {
                res = new DoctorReservation();
            }
            res.ID = newRes.ResID;
            res.DoctorID = newRes.ID;
            res.MaxReservation = newRes.MaxRes;
            res.StartTime = newRes.Date.Date.Add(newRes.StartTime);
            res.EndTime = newRes.Date.Date.Add(newRes.EndTime);
            return res;
        }
        public Doctor MapToDoctorFromRegister(DoctorRegisterViewModel doctorRegisterVM)
            => new Doctor()
            {
                FirstName = doctorRegisterVM.FirstName,
                LastName = doctorRegisterVM.LastName,
                BirthDate = doctorRegisterVM.BirthDate.ToDateTime(new TimeOnly(0, 0)),
                SpecialtyID = doctorRegisterVM.SpecialtyID,
                Fees = doctorRegisterVM.Fees,
                WaitingTime = doctorRegisterVM.WaitingTime,
                ImageURL = doctorRegisterVM.ImageURL,
                About = doctorRegisterVM.About,
                WorkingDays = WorkingDays.Saturday | WorkingDays.Sunday | WorkingDays.Monday | WorkingDays.Tuesday | WorkingDays.Wednesday | WorkingDays.Thursday | WorkingDays.Friday,
                Governorate = doctorRegisterVM.Governorate,
                Location = doctorRegisterVM.Address,
                Gender = doctorRegisterVM.Gender,
                Lng = doctorRegisterVM.Lng,
                Lat = doctorRegisterVM.Lat,
                DefaultStartTime = new DateTime(DateOnly.FromDateTime(DateTime.Now), new TimeOnly(9, 0)),
                DefaultEndTime = new DateTime(DateOnly.FromDateTime(DateTime.Now), new TimeOnly(10, 0)),
                DefaultMaxReservations = 10,
            };

        public async Task<Doctor> MapToDoctorFromEdit(DoctorEditViewModel doctorEditVM)
        {
            var doctor = await _doctorManager.GetDoctorByID(doctorEditVM.Id);

            doctor.About = doctorEditVM.About;
            doctor.ImageURL = doctorEditVM.ImageURL;
            doctor.Location = doctorEditVM.Address;
            doctor.Fees = doctorEditVM.Fees;
            doctor.Lat = doctorEditVM.Lat;
            doctor.Lng = doctorEditVM.Lng;
            doctor.Governorate = doctorEditVM.Governorate;
            doctor.WaitingTime = doctorEditVM.WaitingTime;
            doctor.AppUser.PhoneNumber = doctorEditVM.PhoneNumber;

            return doctor;
        }
        public DoctorEditViewModel MapToDoctorEdit(Doctor doctor)
            => new DoctorEditViewModel()
            {
                Id = doctor.ID,
                About = doctor.About,
                Address = doctor.Location,
                Gender = doctor.Gender,
                BirthDate = DateOnly.FromDateTime(doctor.BirthDate),
                Email = doctor.AppUser.Email,
                Fees = doctor.Fees,
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                ImageURL = doctor.ImageURL,
                Lat = doctor.Lat.Value,
                Lng = doctor.Lng.Value,
                Governorate = doctor.Governorate,
                PhoneNumber = doctor.AppUser.PhoneNumber,
                Specialty = doctor.Specialty.Name,
                WaitingTime = doctor.WaitingTime,
            };

    }

}
