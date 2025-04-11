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
        public Task AddAppointment(int patientId, int doctorReservationId, DateTime appointmentDate);
        public Task<List<Appointment>> GetDoctorAppointments(int doctorId);
        public Task<List<Appointment>> GetPatientAppointments(int patientId, string? specialtyName = null, string? doctorName = null);
    }
}
