using MVC.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace MVC.Validation
{
    public class ValidEndTimeAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var model = (ScheduleViewModel)validationContext.ObjectInstance;
            if (model.EndTime != null || model.StartTime != null)
            {
                if (model.EndTime <= model.StartTime.AddMinutes(30))
                    return new ValidationResult("End time must be after start time by at least 30 minutes");
            }
            return ValidationResult.Success;
        }
    }
}
