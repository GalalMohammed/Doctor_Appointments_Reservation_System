using MVC.Enums;

namespace MVC.ViewModels
{
    public class doctorProfileVM
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }

        public Gender Gender { get; set; }


        public string Image { get; set; }

        public string Qualifications { get; set; }

        public int Fees { get; set; }

        public List<string> Specialties { get; set; }

        public float Rating { get; set; }

        public int Experience { get; set; }

        public Governorate Governorate { get; set; }
        public string Location { get; set; }

        public string Phone { get; set; }

        public List<Rating> Ratings { get; set; }
        public List<Appointment> Appointments { get; set; }
        public double Latitude { get; internal set; }
        public double Longitude { get; internal set; }
    }
}
