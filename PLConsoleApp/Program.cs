namespace PLConsoleApp
{
    internal class Program
    {
        public enum WorkingDays
        {
            Saturday = 1,
            Sunday = 2,
            Monday = 4,
            Tuesday = 8,
            Wednesday = 16,
            Thursday = 32,
            Friday = 64
        }
        private static DateTime GetCorrespondingNextDay(DateTime date, WorkingDays day)
        {
            int dayValue = (int)day;
            int bitPosition = (int)Math.Log(dayValue, 2);
            // .NET DayOfWeek Enum Values sunday = 0 
            int desiredDayOfWeek = (bitPosition - 1 + 7) % 7;
            int currentDayOfWeek = (int)date.DayOfWeek;
            int daysToAdd = (desiredDayOfWeek - currentDayOfWeek + 7) % 7;
            if (daysToAdd == 0)
            {
                daysToAdd = 7;
            }
            return date.AddDays(daysToAdd);
        }
        static void Main(string[] args)
        {
            WorkingDays days = WorkingDays.Tuesday | WorkingDays.Sunday;
            Console.WriteLine(GetCorrespondingNextDay(DateTime.Now,days));
        }
    }
}
