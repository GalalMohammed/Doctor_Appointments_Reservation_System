using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace vezeetaApplicationAPI.Models
{

    public class Doctor
    {
        public int ID { get; set; }

        [Required, MaxLength(50)]
        public string FirstName { get; set; }

        [Required, MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        public int Specialties_ID { get; set; }

        [Required, Range(0, double.MaxValue)]
        public decimal Fees { get; set; }

        [Range(0, 60)]
        public int WaitingTime { get; set; }

        public string Location { get; set; }

        [Range(0, 5)]
        public float OverallRating { get; set; }

        [Url]
        public string URLImage { get; set; }

        public string About { get; set; }

        public int WorkingDays { get; set; }

        public DateTime DefaultStartTime { get; set; }

        public DateTime DefaultEndTime { get; set; }

        public Specialties Specialties { get; set; }
        public ICollection<DoctorReservation> DoctorReservations { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }

}
