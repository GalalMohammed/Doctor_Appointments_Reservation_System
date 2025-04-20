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
        
        public int? DoctorReservationID { get; set; }
        
        public int PatientId { get; set; }

        public DateTime AppointmentDate { get; set; }
        public virtual DoctorReservation? DoctorReservation { get; set; }
        public virtual Patient? Patient { get; set; }
    }
}
