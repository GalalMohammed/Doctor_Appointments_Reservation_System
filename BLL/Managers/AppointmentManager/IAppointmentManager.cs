using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vezeetaApplicationAPI.Models;

namespace BLLServices.Managers.AppointmentManager
{
    public interface IAppointmentManager
    {
        public Task DeleteAppointmentAsync(int appointmentId);
        public Task AddAppointment(int patientId, int doctorReservationId);
        public Task<List<Appointment>> GetDoctorAppointments(int doctorId, int reservationId);
        public Task<List<Appointment>> GetPatientAppointments(int patientId, string? specialtyName = null, string? doctorName = null);
        public Task<int> GetAppointmentsCountByDate(int doctorID, DateTime? date);

    }
}
