namespace MVC.ViewModels
{
    public class AppointmentViewModel
    {
        public int Id { get; set; }
        public string Specialty { get; set; }
        public DateTime DateTime { get; set; }
        public City Location { get; set; }
        public string Doctor { get; set; }
        public string DoctorImagePath { get; set; }
    }
}
