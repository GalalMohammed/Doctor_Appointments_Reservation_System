using MVC.Enums;
using MVC.ViewModels;
using vezeetaApplicationAPI.Models;

namespace MVC.Mappers
{
    public class PatientMapper
    {
        public AppointmentViewModel MapToAppointmentViewModel(Appointment appointment)
        {
            return new AppointmentViewModel
            {
                Id = appointment.ID,
                DateTime = appointment.AppointmentDate,
                Doctor = appointment.DoctorReservation?.Doctor?.FirstName + " " + appointment.DoctorReservation?.Doctor?.LastName,
                Specialty = appointment.DoctorReservation?.Doctor?.Specialty?.Name ?? "Not Available",
                Governorate = appointment.DoctorReservation.Doctor.Governorate,
                Location = appointment.DoctorReservation.Doctor.Location,
                DoctorImagePath = appointment.DoctorReservation?.Doctor?.ImageURL ?? "default_doctor.png"
            };
        }
        public PatientViewModel MapToPatientViewModel(Patient patient)
        {
            throw new NotImplementedException();
        }
        public Patient MapToPatient(PatientViewModel patientViewModel)
        {
            Patient patient = new()
            {
                FirstName = patientViewModel.FirstName,
                LastName = patientViewModel.LastName,
                BirthDate = patientViewModel.BirthDate.ToDateTime(new TimeOnly()),
                Location = patientViewModel.Governorate.ToString(),
            };
            if (patientViewModel.Id != 0)
            {
                patient.ID = patientViewModel.Id;
            }
            return patient;
        }
        public Appointment MapToAppointment(AppointmentViewModel appointmentViewModel, int patientId, int doctorReservationId)
        {
            Appointment appointment =  new()
            {
                PatientId = patientId,
                DoctorReservationID = doctorReservationId,
                AppointmentDate = appointmentViewModel.DateTime
            };
            if (appointmentViewModel.Id != 0)
            {
                appointment.ID = appointmentViewModel.Id;
            }
            return appointment;
        }
    }
}
