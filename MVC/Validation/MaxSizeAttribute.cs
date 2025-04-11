using System.ComponentModel.DataAnnotations;

namespace MVC.Validation
{
    public class MaxSizeAttribute : ValidationAttribute
    {
        private readonly int _maxSize;
        public MaxSizeAttribute(int maxSize)
        {
            _maxSize = maxSize;
        }
        public override bool IsValid(object? value)
        {
            var file = value as IFormFile;
            return file != null && file.Length <= _maxSize * 1024 * 1024;
        }
    }
}
