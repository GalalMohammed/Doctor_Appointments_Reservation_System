using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Models;
using DAL.Enums;

namespace vezeetaApplicationAPI.Models
{
    
    public class Doctor : Person
    {
        [Key]
        public int ID { get; set; }

        public int SpecialtyID { get; set; }
        public virtual Specialty? Specialty { get; set; }
        
        [DataType(DataType.Currency)]
        public decimal Fees { get; set; }
        
        [Range(0, 60)]
        [DataType(DataType.Duration)]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Waiting Time must be a number")]
        public int WaitingTime { get; set; }
        
        [Range(0, 5)]
        public float OverallRating { get; set; }
        
        public required string ImageURL { get; set; }
        
        [DataType(DataType.MultilineText)]
        [StringLength(500, ErrorMessage = "About Doctor must be less than 500 characters")]
        public required string About { get; set; }
        
        [EnumDataType(typeof(WorkingDays))]
        public WorkingDays WorkingDays { get; set; }
        
        [DataType(DataType.Time)]
        public DateTime DefaultStartTime { get; set; }
        
        [DataType(DataType.Time)]
        public DateTime DefaultEndTime { get; set; }
        
        public virtual ICollection<DoctorReservation>? DoctorReservations { get; set; }
        public virtual ICollection<Review>? Reviews { get; set; }
    }

}
