using MVC.ViewModels;
using vezeetaApplicationAPI.Models;

namespace MVC.Mappers
{
    public interface IDoctorMapper
    {

        public SpecialityVM MapToSpecialityVM(Specialty specialty);
        public CalenderReservationVM MapToCalenderReservationVM(DoctorReservation reservation);


        public Task<DoctorReservationViewModel> MapToDoctorReservationViewModelAsync(DoctorReservation reservation);


        public Task<docSearchVM> MapToDocSearchVMAsync(Doctor doctor);

        public Task<Doctor> MapFromDocSearchVM(docSearchVM doctorVM);

        public Task<doctorProfileVM> MapToDoctorProfileVM(Doctor doctor);

        public Task<Doctor> MapFromDoctorProfileVM(doctorProfileVM doctorVM);

        public Rating MapToRating(Review review);

        public Review MapFromRating(Rating rating);

        public Doctor MapToDoctorFromRegister(DoctorRegisterViewModel doctorRegisterVM);
        public Task<Doctor> MapToDoctorFromEdit(DoctorEditViewModel doctorRegisterVM);
        public DoctorEditViewModel MapToDoctorEdit(Doctor doctor);
        public NewResVM MapToNewResVM(DoctorReservation doctorReservation);

        public Task<DoctorReservation> MapFromNewResVM(NewResVM newRes);


    }
}
