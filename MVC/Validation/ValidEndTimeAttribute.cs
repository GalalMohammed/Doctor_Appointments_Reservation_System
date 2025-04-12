using System.ComponentModel.DataAnnotations;

namespace MVC.Validation
{
    public class ValidEndTimeAttribute : ValidationAttribute
    {
        //protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        //{
        //    var model = (DoctorRegisterViewModel)validationContext.ObjectInstance;
        //    if (model.EndTime <= model.StartTime)
        //        return new ValidationResult("End time must be after start time");
        //    return ValidationResult.Success;
        //}
    }
}
