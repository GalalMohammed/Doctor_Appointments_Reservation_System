using vezeetaApplicationAPI.Models;

namespace DAL.Models
{
    public class Order
    {
        public required string Id { get; set; }
        public int PatientId { get; set; }
        public int DoctorReservationId { get; set; }
        public bool Status { get; set; }
        public Patient? Patient { get; set; }
        public DoctorReservation? DoctorReservation { get; set; }
    }
}
