using MVC.Enums;

namespace MVC.ViewModels
{
    public class AppointmentViewModel
    {
        public int Id { get; set; }
        public string Specialty { get; set; }
        public DateTime DateTime { get; set; }
        public Governorate Governorate { get; set; }
        public string Location { get; set; }
        public string Doctor { get; set; }
        public string DoctorImagePath { get; set; }
    }
}
