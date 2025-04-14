namespace MVC.ViewModels
{
    public class CalenderReservationVM
    {
        public DateTime from { get; set; }
        public DateTime to { get; set; }
        public string title { get; set; } = string.Empty;
        public string color { get; set; } = "#004085";
        public string colorBorder { get; set; } = "#B3E5FC";
        public string status { get; set; } = string.Empty;

        public int ResID { get; set; }
        public int MaxRes { get; set; } = 10;
        public bool isAllDay { get; set; } = true;

    }
}
