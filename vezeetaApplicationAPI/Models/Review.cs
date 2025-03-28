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

        [Required]
        public int Patient_ID { get; set; }

        [Required]
        public int Doctor_ID { get; set; }

        [Required, Range(1, 5)]
        public int Rate { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        public Doctor Doctor { get; set; }
        public Patient Patient { get; set; }
    }
}
