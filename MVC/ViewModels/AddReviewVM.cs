using System.ComponentModel.DataAnnotations;

namespace MVC.ViewModels
{
    public class AddReviewVM
    {
        [Required]
        public int AppID { get; set; }
        [Required]
        public string Review { get; set; }
        [Required]
        public int Rate { get; set; }

    }
}
