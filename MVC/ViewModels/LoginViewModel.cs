using System.ComponentModel.DataAnnotations;

namespace MVC.ViewModels
{
    public class LoginViewModel
    {
        [RegularExpression(@"^[a-zA-Z0-9]+@[a-zA-Z0-9]+\.com$", ErrorMessage = "Email must match the following pattern: test@test.com")]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
