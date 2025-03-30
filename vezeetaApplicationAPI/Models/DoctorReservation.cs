using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vezeetaApplicationAPI.Models
{
    public class DoctorReservation
    {
        public int ID { get; set; }
        
        [Required(ErrorMessage = "Doctor ID is required")]
        public int DoctorID { get; set; }
        
        [Required(ErrorMessage = "Start Time is required")]
        [DataType(DataType.Time)]
        [Display(Name = "Start Time")]
        public DateTime StartTime { get; set; }
        
        [Required(ErrorMessage = "End Time is required")]
        [DataType(DataType.Time)]
        [Display(Name = "End Time")]
        public DateTime EndTime { get; set; }
        
        [Required(ErrorMessage = "Reservation Date is required")]
        [Display(Name = "Max Reservations")]
        public int MaxReservation { get; set; }
        public Doctor? Doctor { get; set; }
        public ICollection<Appointment>? Appointments { get; set; }
    }
}
