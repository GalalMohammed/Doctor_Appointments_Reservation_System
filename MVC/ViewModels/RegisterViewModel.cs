using MVC.Validation;
using System.ComponentModel.DataAnnotations;

namespace MVC.ViewModels
{
    public class RegisterViewModel
    {
        [Display(Name = "First Name")]
        [RegularExpression("^[a-zA-Z0-9](?:[a-zA-Z0-9_ -]*[a-zA-Z0-9])?$", ErrorMessage = "Invalid Name Format")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        [RegularExpression("^[a-zA-Z0-9](?:[a-zA-Z0-9_ -]*[a-zA-Z0-9])?$", ErrorMessage = "Invalid Name Format")]
        public string LastName { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Compare("Password")]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        [RegularExpression(@"^[a-zA-Z0-9]+@[a-zA-Z0-9]+\.com$", ErrorMessage = "Email must match the following pattern: test@test.com")]
        public string Email { get; set; }
        [Display(Name = "Phone Number")]
        [RegularExpression("^0\\d{10}$", ErrorMessage = "Invalid Phone Number")]
        public string PhoneNumber { get; set; }
        public City City { get; set; }
        [Display(Name = "Birth Date")]
        [ValidDate]
        public DateOnly BirthDate { get; set; }
    }
}
