using DAL.Enums;
using DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace vezeetaApplicationAPI.Models
{

    public class Doctor : Person
    {
        [Key]
        public int ID { get; set; }

        public int SpecialtyID { get; set; }
        public virtual Specialty? Specialty { get; set; }

        [DataType(DataType.Currency)]
        public int Fees { get; set; }

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

        public int DefaultMaxReservations { get; set; }

        public virtual ICollection<DoctorReservation>? DoctorReservations { get; set; }
        public virtual ICollection<Review>? Reviews { get; set; }
    }

}
