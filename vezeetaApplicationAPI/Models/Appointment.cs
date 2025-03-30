using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vezeetaApplicationAPI.Models
{
    public class Appointment
    {
        public int ID { get; set; }
        
        [Required(ErrorMessage = "Doctor Reservation ID is required")]
        public int DoctorReservationID { get; set; }
        
        [Required(ErrorMessage = "Patient ID is required")]
        public int PatientId { get; set; }
        
        public DoctorReservation? DoctorReservation { get; set; }
        public Patient? Patient { get; set; }
    }
}
