using MVC.Enums;

namespace MVC.ViewModels
{
    public class FilterSearchVM
    {
        public int PageNum { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public string Name { get; set; } = string.Empty;
        public int Speciality { get; set; } = 0;
        public Governorate Governorate { get; set; } = Governorate.All;
        public Gender Gender { get; set; } = Gender.All;
        public int WaitingTime { get; set; } = 0;
        public double MinPrice { get; set; } = 0;
        public double MaxPrice { get; set; } = 10000;
    }
}
