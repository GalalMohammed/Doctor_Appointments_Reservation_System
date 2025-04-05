using DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vezeetaApplicationAPI.Models
{
    public class Patient : Person
    {
        public int ID { get; set; }
        
        public int AppUserID { get; set; }
        public virtual AppUser? AppUser { get; set; }

        public virtual ICollection<Appointment>? Appointments { get; set; }
        public virtual ICollection<Review>? Reviews { get; set; }
    }
}
