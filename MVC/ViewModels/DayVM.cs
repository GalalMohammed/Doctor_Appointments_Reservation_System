using System.ComponentModel.DataAnnotations;

namespace MVC.ViewModels
{
    public class DayVM
    {
        public bool Exists { get; set; }
        [Display(Name = "From")]
        public TimeOnly StartTime { get; set; }
        [Display(Name = "To")]
        public TimeOnly EndTime { get; set; }
    }
}
