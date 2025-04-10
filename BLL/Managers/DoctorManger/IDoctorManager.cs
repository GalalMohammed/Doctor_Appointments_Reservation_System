using vezeetaApplicationAPI.Models;

namespace BLLServices.Managers.DoctorManger
{
    public interface IDoctorManager
    {
        public Task AddDoctor(Doctor doctorVM);
        public Task GetDoctorInfo(int doctorID);
        public Task UpdateDoctor(Doctor doctorVM);
        public Task<List<Doctor>> GetDoctorsOrderedByrating();


        public Task<List<Doctor>> GetAllDoctors();
        public Task<Doctor> GetDoctorByID(int id);

        public Task<List<Doctor>?> GetDoctorCondition(Func<Doctor, bool> condition);
    }
}
