﻿using DAL.Enums;
using DAL.Repositories.DoctorReservations;
using vezeetaApplicationAPI.Models;


namespace BLLServices.Managers.DoctorReservationManager
{
    public class DoctorReservationManager : IDoctorReservationManager
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
            var reservation = DR_manager.GetByID(res.ID).Result;
            if (reservation != null)
                DR_manager.Delete(reservation);
        }
        public void AddDoctorReservation(DoctorReservation res)
        {
            DR_manager.Add(res);
        }
        public async Task<DoctorReservation> GetDoctorReservationByID(int id)
        {
            return await DR_manager.GetByID(id);
        }
        public Task<List<DoctorReservation>> GetReservationsByDocID(int id)
        {
            return DR_manager.GetReservationsByDocID(id);
        }
        public void GenerateCalanderReservation(Doctor doc, int MaxRes)
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
            DateTime start = new DateTime(new DateOnly(date.Year, date.Month, date.Day)
                 , new TimeOnly(doc.DefaultStartTime.Hour, doc.DefaultStartTime.Minute));
            DateTime end = new DateTime(new DateOnly(date.Year, date.Month, date.Day)
                 , new TimeOnly(doc.DefaultEndTime.Hour, doc.DefaultEndTime.Minute));
            DoctorReservation newReservation = new DoctorReservation()
            {
                DoctorID = doc.ID,
                StartTime = start,
                EndTime = end,
                MaxReservation = MaxRes
            };
            if (!IsInCalender(newReservation).Result)
            {
                DR_manager.Add(newReservation);
            }
            else
            {
                Console.WriteLine("Reservation Already Exist");
                DR_manager.Update(newReservation);
            }
        }
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
        private async Task<bool> IsInCalender(DoctorReservation res)
        {
            List<DoctorReservation> reservations = await DR_manager.GetAll();
            return reservations.Any(r => r.DoctorID == res.DoctorID
            && r.StartTime.Day == res.StartTime.Day);
        }
    }
}
