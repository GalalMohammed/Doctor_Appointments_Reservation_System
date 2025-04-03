using MVC.Validation;
using System.ComponentModel.DataAnnotations;

namespace MVC.ViewModels
{
    public class PatientViewModel
    {
        public int Id { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Old Password")]
        public string? OldPassword { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string? NewPassword { get; set; }
        [RegularExpression(@"^[a-zA-Z0-9]+@[a-zA-Z0-9]+\.com$", ErrorMessage = "Email must match the following pattern: test@test.com")]
        public string Email { get; set; }
        [Display(Name = "Phone Number")]
        [RegularExpression("^0\\d{10}$", ErrorMessage = "Invalid Phone Number")]
        public string PhoneNumber { get; set; }
        public City City { get; set; }
        [Display(Name = "Birth Date")]
        [ValidDate]
        public DateOnly BirthDate { get; set; }
        public List<AppointmentViewModel>? Appointments { get; set; }
    }
}
