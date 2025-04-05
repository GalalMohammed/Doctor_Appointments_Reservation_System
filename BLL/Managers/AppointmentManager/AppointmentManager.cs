using DAL.Repositories.Appointments;
using DAL.Repositories.DoctorReservations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vezeetaApplicationAPI.Models;

namespace BLLServices.Managers.AppointmentManager
{
    public class AppointmentManager(IAppointmentRepository appointmentRepository, IDoctorReservationRepository reservationRepository) : IAppointmentManager
    {
        public void AddAppointment(int patientId, int doctorReservationId)
        {
            Appointment appointment = new()
            {
                PatientId = patientId,
                DoctorReservationID = doctorReservationId
            };
            appointmentRepository.Add(appointment);
        }

        public async Task DeleteAppointmentAsync(int patientId, int doctorReservationId)
        {
            List<Appointment> appointments = await appointmentRepository.GetAll();
            Appointment? appointment = appointments.Find(app => app.PatientId == patientId && app.DoctorReservationID == doctorReservationId);
            if (appointment != null)
                appointmentRepository.Delete(appointment);
        }

        public async Task<List<Appointment>> GetDoctorAppointments(int doctorId)
        {
            IEnumerable<DoctorReservation> reservations = await reservationRepository.GetReservationsByDocID(doctorId);
            List<int> reservationsIds = [.. reservations.Select(r => r.ID)];
            List<Appointment> appointments = await appointmentRepository.GetAll();
            return appointments.FindAll(app => reservationsIds.Contains(app.DoctorReservationID));
        }

        public async Task<List<Appointment>> GetPatientAppointments(int patientId, string? specialtyName = null, string? doctorName = null)
        {
            IEnumerable<DoctorReservation> reservations = await reservationRepository.GetAll();
            if (specialtyName != null)
                reservations = reservations.Where(r => r != null && r.Doctor != null && r.Doctor.Specialty != null && r.Doctor.Specialty.Name == specialtyName);
            if (doctorName != null)
                reservations = reservations.Where(r => r != null && r.Doctor != null && ($"{r.Doctor.FirstName} {r.Doctor.LastName}".StartsWith(doctorName, StringComparison.OrdinalIgnoreCase) || (r.Doctor.FirstName.StartsWith(doctorName, StringComparison.OrdinalIgnoreCase)) || (r.Doctor.LastName.StartsWith(doctorName, StringComparison.OrdinalIgnoreCase))));
            List<int> reservationsIds = [.. reservations.Select(r => r.ID)];
            List<Appointment> appointments = await appointmentRepository.GetAll();
            return appointments.FindAll(app => reservationsIds.Contains(app.DoctorReservationID) && app.PatientId == patientId);
        }
    }
}
