using MVC.Enums;
using MVC.Validation;
using System.ComponentModel.DataAnnotations;

namespace MVC.ViewModels
{
    public class DoctorRegisterViewModel : RegisterBase
    {
        [Display(Name = "Specialty")]
        public int SpecialtyID { get; set; }
        [Range(0, double.MaxValue)]
        public int Fees { get; set; }
        [Range(0, 60)]
        [DataType(DataType.Duration)]
        [Display(Name = "Waiting Time")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Waiting Time must be a number")]
        public int WaitingTime { get; set; }
        public string? ImageURL { get; set; }
        [Display(Name = "Profile Image")]
        [MaxSize(2, ErrorMessage = "Maximum allowed size is 2 MB")]
        [AllowedExtensions(new string[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".webp", ".svg" })]
        public IFormFile Image { get; set; }
        [DataType(DataType.MultilineText)]
        [StringLength(500, ErrorMessage = "About Doctor must be less than 500 characters")]
        public string About { get; set; }
        [MaxLength(200)]
        public string Address { get; set; }
        public Gender Gender { get; set; }
        public double Lat { get; set; } = 30.0594629;
        public double Lng { get; set; } = 31.3406953;
    }
}
