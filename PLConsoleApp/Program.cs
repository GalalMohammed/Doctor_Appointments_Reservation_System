using BLLServices.Managers.AppointmentManager;
using DAL.Enums;
using vezeetaApplicationAPI.Models;

namespace PLConsoleApp
{
    
    internal class Program
    {
        public static DateTime GetCorrespondingNextDay(DateTime date, WorkingDays day)
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
            var day = WorkingDays.Wednesday;
            Console.WriteLine(GetCorrespondingNextDay(DateTime.Now, day));
        }
    }
}
