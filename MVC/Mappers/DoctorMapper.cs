using BLLServices.Managers.AppointmentManager;
using BLLServices.Managers.DoctorReservationManager;
using BLLServices.Managers.ReviewManager;
using BLLServices.Managers.SpecialtyManager;
using MVC.Enums;
using MVC.ViewModels;
using vezeetaApplicationAPI.Models;

namespace MVC.Mappers
{
    public class DoctorMapper
    {
        private IReviewManager _reviewManager;
        private IDoctorReservationManager _doctorReservationManager;
        private ISpecialtyManager _specialtyManager;

        public DoctorMapper(IDoctorReservationManager doctorReservationManager,ISpecialtyManager specialtyManager
            ,IReviewManager reviewManager) 
        {
            _doctorReservationManager = doctorReservationManager;
            _specialtyManager = specialtyManager
            _reviewManager = reviewManager;
        }
        public DoctorReservationViewModel MapToDoctorReservationViewModel(DoctorReservation reservation)
        {
            return new DoctorReservationViewModel
            {
                Day = reservation.EndTime.Day,
                Time = reservation.EndTime.ToString("hh:mm tt")
            };
        }
        //public DoctorReservation MapFromDoctorReservationViewModel(DoctorReservationViewModel reservationVM)
        //{
        //    //return new DoctorReservation
        //    //{
        //    //    StartTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, reservationVM.Day, 0, 0, 0),
        //    //    EndTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, reservationVM.Day, int.Parse(reservationVM.Time.Split(':')[0]), int.Parse(reservationVM.Time.Split(':')[1].Split(' ')[0]), 0)
        //    //};
        //}
        public async Task<docSearchVM> MapToDocSearchVMAsync(Doctor doctor)
        {
            var specialties = await _specialtyManager.GetAllSpecialties();
            var specialtiesList = specialties.Select(s=>s.Name).ToList();
            var avgRating = await _reviewManager.GetDoctorAverageRating(doctor.ID);
            var reservations = await _doctorReservationManager.GetReservationsByDocID(doctor.ID);
            List<DoctorReservationViewModel> appointments = new List<DoctorReservationViewModel>();
            foreach (var reservation in reservations)
            {
                appointments.Add(MapToDoctorReservationViewModel(reservation));
            }
            return new docSearchVM
            {
                ID = doctor.ID,
                Name = $"{doctor.FirstName} {doctor.LastName}",
                Title = null,
                Gender = (Enums.Gender)doctor.Gender, // unite in one enum
                Image = doctor.ImageURL,
                Qualifications = null,
                Fees = (int)doctor.Fees, // remember to change it to double
                Specialties = specialtiesList,
                Rating = avgRating,
                Experience = 0, // remember to change it or remove it
                Governorate = 0, // remember to change it or remove it
                Location = doctor.Location,
                Phone = doctor.AppUser.PhoneNumber,
                Appointments = appointments
            };
        }
        public doctorProfileVM MapToDoctorProfileVM(Doctor doctor)
        {
            throw new NotImplementedException();
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
    }
}
