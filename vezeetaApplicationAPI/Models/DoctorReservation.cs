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
        
        public int DoctorID { get; set; }
        
        [DataType(DataType.Time)]
        public DateTime StartTime { get; set; }
        
        [DataType(DataType.Time)]
        public DateTime EndTime { get; set; }
        
        public int MaxReservation { get; set; }

        public virtual Doctor? Doctor { get; set; }
        public virtual ICollection<Appointment>? Appointments { get; set; }
    }
}
