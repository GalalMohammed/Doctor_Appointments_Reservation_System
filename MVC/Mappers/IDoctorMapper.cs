using MVC.ViewModels;
using vezeetaApplicationAPI.Models;

namespace MVC.Mappers
{
    public interface IDoctorMapper
    {
        public DoctorReservationViewModel MapToDoctorReservationViewModel(DoctorReservation reservation);


        public Task<docSearchVM> MapToDocSearchVMAsync(Doctor doctor);

        public Task<Doctor> MapFromDocSearchVM(docSearchVM doctorVM);

        public Task<doctorProfileVM> MapToDoctorProfileVM(Doctor doctor);

        public Task<Doctor> MapFromDoctorProfileVM(doctorProfileVM doctorVM);

        public Rating MapToRating(Review review);

        public Review MapFromRating(Rating rating);

        public Doctor MapToDoctorFromRegister(DoctorRegisterViewModel doctorRegisterVM);
        public Task<Doctor> MapToDoctorFromEdit(DoctorEditViewModel doctorRegisterVM);
        public DoctorEditViewModel MapToDoctorEdit(Doctor doctor);

    }
}
