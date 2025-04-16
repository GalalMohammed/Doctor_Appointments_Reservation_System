using MVC.Validation;
using System.ComponentModel.DataAnnotations;

namespace MVC.ViewModels
{
    public class ChangeImageVM
    {
        [Required]
        public int ID { get; set; }

        [Required]
        [MaxSize(2, ErrorMessage = "Maximum allowed size is 2 MB")]
        [AllowedExtensions(new string[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".webp", ".svg" })]
        public IFormFile File { get; set; }
    }
}
