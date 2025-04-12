using System.ComponentModel.DataAnnotations;

namespace MVC.ViewModels
{
    public class ForgetPasswordVM
    {
        [Required(ErrorMessage ="The is Required")]
        [RegularExpression(@"^[a-zA-Z0-9.]+@[a-zA-Z0-9]+\.com$", ErrorMessage = "Email must match the following pattern: test@test.com")]
        public string Email { get; set;}


    }
}
