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

        [Required]
        public int Doctor_ID { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [Required, Range(1, int.MaxValue)]
        public int MaxReservation { get; set; }

        public Doctor Doctor { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
    }
}
