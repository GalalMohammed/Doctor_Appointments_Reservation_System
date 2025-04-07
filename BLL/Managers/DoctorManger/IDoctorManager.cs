using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vezeetaApplicationAPI.Models;

namespace BLLServices.Managers.DoctorManger
{
    public interface IDoctorManager
    {
        public  Task AddDoctor(DoctorVM doctorVM);
        public Task GetDoctorInfo(int doctorID);
        public Task UpdateDoctor(DoctorVM doctorVM);
        public Task<List<Doctor>> GetDoctorsOrderedByrating();


        public Task<List<Doctor>> GetAllDoctors();
        public Task<Doctor> GetDoctorByID(int id);


    }
}
