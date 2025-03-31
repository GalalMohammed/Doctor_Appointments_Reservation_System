using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Repositories.DoctorReservations;
using vezeetaApplicationAPI.Models;


namespace BLLServices.Managers
{
    public class DoctorReservationManager
    {
        private readonly IDoctorReservationRepository DR_manager;
        public DoctorReservationManager(IDoctorReservationRepository res)
        {
            DR_manager = res;
        }
        public void EditDoctorReservation(DoctorReservation res)
        {
            DR_manager.Update(res);
        }
        public void DeleteDoctorReservation(DoctorReservation res)
        {
            DR_manager.Delete(res);
        }
        public void AddDoctorReservation(DoctorReservation res)
        {
            DR_manager.Add(res);
        }
        public Task<List<DoctorReservation>> GetReservationsByDocID(int id)
        {
            return DR_manager.GetReservationsByDocID(id);
        }
        public void GenerateCalanderReservation(Doctor doc,int MaxRes)
        {
            WorkingDays days = doc.WorkingDays;
            foreach (WorkingDays day in Enum.GetValues(typeof(WorkingDays)))
            {
                if ((days & day) == day)
                {
                    GenerateRecordDay(doc, day, MaxRes);
                }
            }
            
        }
        private void GenerateRecordDay(Doctor doc, WorkingDays day, int MaxRes)
        {
            DateTime date = GetCorrespondingNextDay(DateTime.Now, day);
            DateTime start = new DateTime(new DateOnly(date.Year,date.Month,date.Day)
                 ,new TimeOnly(doc.DefaultStartTime.Hour,doc.DefaultStartTime.Minute));
            DateTime end = new DateTime(new DateOnly(date.Year, date.Month, date.Day)
                 , new TimeOnly(doc.DefaultEndTime.Hour, doc.DefaultEndTime.Minute));
            DR_manager.Add(new DoctorReservation
            {
                DoctorID = doc.ID,
                StartTime = start,
                EndTime = end,
                MaxReservation = MaxRes
            });
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
    }
}
