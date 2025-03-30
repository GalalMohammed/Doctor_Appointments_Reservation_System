using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Models;

namespace vezeetaApplicationAPI.Models
{
    [Flags]
    public enum WorkingDays
    {
        Saturday = 1,
        Sunday = 2,
        Monday = 4,
        Tuesday = 8,
        Wednesday = 16,
        Thursday = 32,
        Friday = 64
    }
    public class Doctor : Person
    {
        public int ID { get; set; }

        //[ForeignKey("AppUser")]
        //public int AppUserID { get; set; }
        //public AppUser? AppUser { get; set; }

        [Required(ErrorMessage = "Doctor Speciality is required")]
        [Display(Name = "Specialty")]
        public int SpecialtyID { get; set; }
        public Specialty? Specialty { get; set; }
        
        [Required(ErrorMessage = "Doctor Name is required")]
        [DataType(DataType.Currency)]
        public decimal Fees { get; set; }
        
        [Required(ErrorMessage = "Doctor Waiting Time is required")]
        [Range(0, 60)]
        [Display(Name = "Waiting Time")]
        [DataType(DataType.Duration)]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Waiting Time must be a number")]
        public int WaitingTime { get; set; }
        
        //[Required(ErrorMessage = "Doctor Location is required")]
        //[DataType(DataType.Text)]
        //public required string Location { get; set; }
        
        [Range(0, 5)]
        [Display(Name = "Overall Rating")]
        public float OverallRating { get; set; }
        
        [Display(Name = "Doctor Image")]
        [Required(ErrorMessage = "Doctor Image is required")]
        public required string ImageURL { get; set; }
        
        [Required(ErrorMessage = "About Doctor is required")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "About Doctor")]
        [StringLength(500, ErrorMessage = "About Doctor must be less than 500 characters")]
        public required string About { get; set; }
        
        [Required(ErrorMessage = "Doctor Working Days is required")]
        [Display(Name = "Working Days")]
        [EnumDataType(typeof(WorkingDays))]
        public WorkingDays WorkingDays { get; set; }
        
        [Required(ErrorMessage = "Doctor Default Start Time is required")]
        [Display(Name = "Default Start Time")]
        [DataType(DataType.Time)]
        public DateTime DefaultStartTime { get; set; }
        
        [Required(ErrorMessage = "Doctor Default End Time is required")]
        [Display(Name = "Default End Time")]
        [DataType(DataType.Time)]
        public DateTime DefaultEndTime { get; set; }
        
        public ICollection<DoctorReservation>? DoctorReservations { get; set; }
        public ICollection<Review>? Reviews { get; set; }
    }

}
