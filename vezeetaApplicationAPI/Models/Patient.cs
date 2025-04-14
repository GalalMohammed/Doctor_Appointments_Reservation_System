using DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace vezeetaApplicationAPI.Models
{
    public class Patient : Person
    {
        [Key]
        public int ID { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Appointment>? Appointments { get; set; }
        public virtual ICollection<Review>? Reviews { get; set; }
    }
}
