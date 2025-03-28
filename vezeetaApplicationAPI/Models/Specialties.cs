using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vezeetaApplicationAPI.Models
{
    public class Specialties
    {
        public int ID { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; }

        [Url]
        public string ImageURL { get; set; }

        public ICollection<Doctor> Doctors { get; set; }

        public override string ToString()
        {
            return $"Specialty Name: {Name}, ID: {ID}";
        }
    }
}
