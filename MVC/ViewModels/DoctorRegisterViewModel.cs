using System.ComponentModel.DataAnnotations;

namespace MVC.ViewModels
{
    public class DoctorRegisterViewModel : RegisterBase
    {
        public int SpecialtyID { get; set; }
        public int Fees { get; set; }
        public string? ImageURL { get; set; }
        [DataType(DataType.MultilineText)]
        [StringLength(500, ErrorMessage = "About Doctor must be less than 500 characters")]
        public required string About { get; set; }
    }
}
