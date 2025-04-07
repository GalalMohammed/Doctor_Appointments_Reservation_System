using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vezeetaApplicationAPI.Models;

namespace BLLServices.Managers.DoctorManger
{
    public interface IDoctorManger
    {
        public  Task AddDoctor(DoctorVM doctorVM);
        public Task GetDoctorInfo(int doctorID);
        public Task UpdateDoctor(DoctorVM doctorVM);
        public Task<List<Doctor>> GetDoctorsOrderedByrating();




    }
}
