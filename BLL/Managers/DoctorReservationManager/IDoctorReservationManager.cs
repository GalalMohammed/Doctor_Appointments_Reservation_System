using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vezeetaApplicationAPI.Models;

namespace BLLServices.Managers.DoctorReservationManager
{
    public interface IDoctorReservationManager
    {
        public void EditDoctorReservation(DoctorReservation res);
        public void DeleteDoctorReservation(DoctorReservation res);
        public void AddDoctorReservation(DoctorReservation res);
        public Task<List<DoctorReservation>> GetReservationsByDocID(int id);
        public void GenerateCalanderReservation(Doctor doc, int MaxRes);
        public DoctorReservation GetDoctorReservationByID(int id);
    }
}
