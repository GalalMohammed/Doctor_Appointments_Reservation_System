using BLLServices.Enums;
using DAL.Repositories.Appointments;
using DAL.Repositories.DoctorReservations;
using vezeetaApplicationAPI.Models;

namespace BLLServices.Managers.AppointmentManager
{
    public class AppointmentManager(IAppointmentRepository appointmentRepository, IDoctorReservationRepository reservationRepository) : IAppointmentManager
    {
        public async Task<bool> IsReservationFull(int doctorReservationId)
        {
            IEnumerable<Appointment> alreadyReserved = await appointmentRepository.GetAllByConditon(app => app.DoctorReservationID == doctorReservationId);
            DoctorReservation? reservation = await reservationRepository.GetByID(doctorReservationId);
            int reserved = alreadyReserved.Count();
            if (reservation != null && reservation.MaxReservation > reserved)
                return false;
            return true;
        }
        public async Task<AppointmentCreationStatus> AddAppointment(int patientId, int doctorReservationId)
        {
            if (!await IsReservationFull(doctorReservationId))
            {
                IEnumerable<Appointment> alreadyReserved = await appointmentRepository.GetAllByConditon(app => app.DoctorReservationID == doctorReservationId && app.PatientId == patientId);
                if (alreadyReserved.Any())
                    return AppointmentCreationStatus.AlreadyReserved;
                Appointment appointment = new()
                {
                    PatientId = patientId,
                    DoctorReservationID = doctorReservationId
                };
                appointmentRepository.Add(appointment);
                return AppointmentCreationStatus.Succeeded;
            }
            else
                return AppointmentCreationStatus.MaxReservationsExceeded;
        }
        public void UpdateAppointment(Appointment appointment)
        {
            appointmentRepository.Update(appointment);
        }

        public async Task DeleteAppointmentAsync(int appointmentId)
        {
            Appointment? appointment = await appointmentRepository.GetByID(appointmentId);
            if (appointment != null)
                appointmentRepository.Delete(appointment);
        }
        public async Task<Appointment> GetAppointmentById(int appointmentId)
        {
            return await appointmentRepository.GetByID(appointmentId);
        }

        public async Task<List<Appointment>> GetDoctorAppointments(int doctorId, int reservationId)
        {
            IEnumerable<DoctorReservation> reservations = await reservationRepository.GetAllByConditon(reservation => reservation.ID == reservationId && reservation.DoctorID == doctorId);
            List<int> reservationsIds = [.. reservations.Select(r => r.ID)];
            List<Appointment> appointments = await appointmentRepository.GetAllByConditon(app => reservationsIds.Contains((int)app.DoctorReservationID));
            return [.. appointments];
        }

        public async Task<List<Appointment>> GetPatientAppointments(int patientId, string? specialtyName = null, string? doctorName = null)
        {
            IEnumerable<Appointment> appointments;
            if (specialtyName != null)
                appointments = await appointmentRepository.GetAllByConditon(r => r.PatientId == patientId && r != null && r.DoctorReservation != null && r.DoctorReservation.Doctor != null && r.DoctorReservation.Doctor.Specialty != null && r.DoctorReservation.Doctor.Specialty.Name == specialtyName && r.DoctorReservation.StartTime >= DateTime.Now);
            else
                appointments = await appointmentRepository.GetAllByConditon(r => r.PatientId == patientId && r.DoctorReservation != null && r.DoctorReservation.StartTime >= DateTime.Now);
            if (doctorName != null)
                appointments = appointments.Where(r => r != null && r.DoctorReservation != null && r.DoctorReservation.Doctor != null && ($"{r.DoctorReservation.Doctor.FirstName} {r.DoctorReservation.Doctor.LastName}".StartsWith(doctorName, StringComparison.OrdinalIgnoreCase) || r.DoctorReservation.Doctor.FirstName.StartsWith(doctorName, StringComparison.OrdinalIgnoreCase) || r.DoctorReservation.Doctor.LastName.StartsWith(doctorName, StringComparison.OrdinalIgnoreCase)));
            return [.. appointments];
        }

        public async Task<int> GetAppointmentsCountByDate(int doctorID, DateTime? date)
        {
            return await appointmentRepository.GetCountByDate(doctorID, date);
        }
        public async Task<int?> GetReservationId(int appointmentId)
        {
            var appointment = await appointmentRepository.GetByID(appointmentId);
            return appointment.DoctorReservationID;
        }
        public async Task<List<Appointment>> GetAppointmentsByReservationId(int reservationId)
        {
            return await appointmentRepository.GetAllByConditon(app => app.DoctorReservationID == reservationId);
        }

    }
}
