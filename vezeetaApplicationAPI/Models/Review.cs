using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vezeetaApplicationAPI.Models
{
    public class Review
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Patient ID is required")]
        public int PatientID { get; set; }
        
        [Required(ErrorMessage = "Doctor ID is required")]
        public int DoctorID { get; set; }

        [Required(ErrorMessage = "Rate is required")]
        [Range(0, 5)]
        public int Rate { get; set; }
        
        [Required(ErrorMessage = "Description is required")]
        [StringLength(500, ErrorMessage = "Description can't be longer than 500 characters")]
        [DataType(DataType.MultilineText)]
        public required string Description { get; set; }
        
        public Doctor? Doctor { get; set; }
        public Patient? Patient { get; set; }
    }
}
