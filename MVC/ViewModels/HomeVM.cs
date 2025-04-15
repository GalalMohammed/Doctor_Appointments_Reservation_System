namespace MVC.ViewModels
{
    public class HomeVM
    {

        public List<string> Specialties { get; set; }
        public List<HomeDoctor> Doctors { get; set; }
        public Dictionary<int,int?> DoctorsPerSpecialtyConunt { get; set; }
    }
}
