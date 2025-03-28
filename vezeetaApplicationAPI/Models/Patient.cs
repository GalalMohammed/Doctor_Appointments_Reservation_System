using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vezeetaApplicationAPI.Models
{
    public class Patient
    {
        public int ID { get; set; }

        [Required, MaxLength(50)]
        public string FirstName { get; set; }

        [Required, MaxLength(50)]
        public string LastName { get; set; }

        [Required, Phone]
        public string MobileNumber { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        public string Gender { get; set; }

        [MaxLength(100)]
        public string Address { get; set; }

        public ICollection<Appointment> Appointments { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
}
