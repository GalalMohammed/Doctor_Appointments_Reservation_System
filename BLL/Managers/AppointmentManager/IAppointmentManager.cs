using BLLServices.Enums;
using vezeetaApplicationAPI.Models;

namespace BLLServices.Managers.AppointmentManager
{
    public interface IAppointmentManager
    {
        public Task<bool> IsReservationFull(int doctorReservationId);
        public Task DeleteAppointmentAsync(int appointmentId);
        public Task<AppointmentCreationStatus> AddAppointment(int patientId, int doctorReservationId);
        public Task<List<Appointment>> GetDoctorAppointments(int doctorId, int reservationId);
        public Task<List<Appointment>> GetPatientAppointments(int patientId, string? specialtyName = null, string? doctorName = null);
        public Task<int> GetAppointmentsCountByDate(int doctorID, DateTime? date);

        public Task<int> GetReservationId(int appointmentId);
    }
}
