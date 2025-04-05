using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vezeetaApplicationAPI.Models
{
    public class Specialty
    {
        public int ID { get; set; }

        [DataType(DataType.Text)]
        [StringLength(50, ErrorMessage = "Specialty Name must be less than 50 characters")]
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Specialty Name must be alphabetic")]
        public required string Name { get; set; }
        
        public required string ImageURL { get; set; }
        
        public virtual ICollection<Doctor>? Doctors { get; set; }
        
        public override string ToString()
        {
            return $"Specialty Name: {Name}, ID: {ID}";
        }
    }
}
