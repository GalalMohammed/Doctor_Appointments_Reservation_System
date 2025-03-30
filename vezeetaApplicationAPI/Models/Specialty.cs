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

        [Required(ErrorMessage = "Specialty Name is required")]
        [DataType(DataType.Text)]
        [StringLength(50, ErrorMessage = "Specialty Name must be less than 50 characters")]
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Specialty Name must be alphabetic")]
        public required string Name { get; set; }
        
        [Required(ErrorMessage = "Specialty Image is required")]
        [Display(Name = "Specialty Image")]
        public required string ImageURL { get; set; }
        
        public ICollection<Doctor>? Doctors { get; set; }
        
        public override string ToString()
        {
            return $"Specialty Name: {Name}, ID: {ID}";
        }
    }
}
