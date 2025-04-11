using MVC.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace MVC.Validation
{
    public class DifferentPasswordAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var model = (ChangePasswordViewModel)validationContext.ObjectInstance;
            if (model.OldPassword == model.NewPassword)
                return new ValidationResult("old password can't match new password");
            return ValidationResult.Success;
        }
    }
}
