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

        public int PatientID { get; set; }
        
        public int DoctorID { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; } = DateTime.Now;

        [Range(0, 5)]
        public int Rate { get; set; }
        
        [StringLength(500, ErrorMessage = "Description can't be longer than 500 characters")]
        [DataType(DataType.MultilineText)]
        public required string Description { get; set; }
        
        public virtual Doctor? Doctor { get; set; }
        public virtual Patient? Patient { get; set; }
    }
}
