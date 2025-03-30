using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vezeetaApplicationAPI.Models
{
    public class Patient
    {
        public int ID { get; set; }
        [ForeignKey("AppUser")]
        public int AppUserID { get; set; }
        public AppUser? AppUser { get; set; }

        public ICollection<Appointment>? Appointments { get; set; }
        public ICollection<Review>? Reviews { get; set; }
    }
}
