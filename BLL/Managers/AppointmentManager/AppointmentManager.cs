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
        public async Task AddAppointment(int patientId, int doctorReservationId, DateTime appointmentDate)
        {
            DateTime baseDateTime = DateOnly.FromDateTime(appointmentDate).ToDateTime(new TimeOnly(0, 0));
            IEnumerable<Appointment> alreadyReserved = await appointmentRepository.GetAllByConditon(app => app.DoctorReservationID == doctorReservationId && app.AppointmentDate > baseDateTime );
            DoctorReservation? reservation = await reservationRepository.GetByID(doctorReservationId);
            int reserved = alreadyReserved.Count();
            if (reservation != null && reservation.MaxReservation > reserved)
            {
                double delay = reserved * (reservation.Doctor?.WaitingTime ?? .5);
                appointmentDate = reservation.StartTime.AddHours(delay);
                Appointment appointment = new()
                {
                    PatientId = patientId,
                    DoctorReservationID = doctorReservationId,
                    AppointmentDate = appointmentDate
                };
                appointmentRepository.Add(appointment);
            }
            else
            {
                throw new Exception("No available slots");
            }
        }

        public async Task DeleteAppointmentAsync(int appointmentId)
        {
            Appointment? appointment = await appointmentRepository.GetByID(appointmentId);
            if (appointment != null)
                appointmentRepository.Delete(appointment);
        }

        public async Task<List<Appointment>> GetDoctorAppointments(int doctorId)
        {
            IEnumerable<DoctorReservation> reservations = await reservationRepository.GetReservationsByDocID(doctorId);
            List<int> reservationsIds = [.. reservations.Select(r => r.ID)];
            List<Appointment> appointments = await appointmentRepository.GetAllByConditon(app => reservationsIds.Contains(app.DoctorReservationID) && app.AppointmentDate >= DateTime.Now);
            return [.. appointments.OrderBy(app => app.AppointmentDate)];
        }

        public async Task<List<Appointment>> GetPatientAppointments(int patientId, string? specialtyName = null, string? doctorName = null)
        {
            IEnumerable<DoctorReservation> reservations;
            if (specialtyName != null)
                reservations = await reservationRepository.GetAllByConditon(r => r != null && r.Doctor != null && r.Doctor.Specialty != null && r.Doctor.Specialty.Name == specialtyName);
            else
                reservations = await reservationRepository.GetAll();
            if (doctorName != null)
                reservations = reservations.Where(r => r != null && r.Doctor != null && ($"{r.Doctor.FirstName} {r.Doctor.LastName}".StartsWith(doctorName, StringComparison.OrdinalIgnoreCase) || (r.Doctor.FirstName.StartsWith(doctorName, StringComparison.OrdinalIgnoreCase)) || (r.Doctor.LastName.StartsWith(doctorName, StringComparison.OrdinalIgnoreCase))));
            List<int> reservationsIds = [.. reservations.Select(r => r.ID)];
            List<Appointment> appointments = await appointmentRepository.GetAllByConditon(app => reservationsIds.Contains(app.DoctorReservationID) && app.PatientId == patientId && app.AppointmentDate >= DateTime.Now);
            return [.. appointments.OrderBy(app => app.AppointmentDate)];
        }
    }
}
