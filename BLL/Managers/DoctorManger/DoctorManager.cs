using AutoMapper;
using DAL.Repositories.Doctors;
using System.Linq.Expressions;
using vezeetaApplicationAPI.Models;

namespace BLLServices.Managers.DoctorManger
{

    public class DoctorManager : IDoctorManager
    {
        IMapper _mapper;
        IDoctorRepository Repository;
        public DoctorManager(IDoctorRepository repo)
        {
            Repository = repo;



        }
        public async Task AddDoctor(Doctor doctorVM)
        {
            Repository.Add(doctorVM);

        }
        public async Task UpdateDoctor(Doctor doctorVM)
        {
            var doctor = await Repository.GetByID(doctorVM.ID);
            if (doctor is null)
            {
                throw new Exception("Doctor does not exist");
            }
            else
            {
                Repository.Update(doctorVM);
            }
        }
        public async Task GetDoctorInfo(int doctorID)
        {
            var doctor = await Repository.GetByID(doctorID);
            if (doctor is null)
            {
                throw new Exception("Doctor does not exist");
            }
            else
            {
                var reviews = doctor.Reviews;
            }
        }
        public async Task<List<Doctor>> GetAllDoctors()
        {
            var doctors = await Repository.GetAll();
            return doctors.ToList();
        }
        public async Task<List<Doctor>> GetDoctorsOrderedByrating()
        {
            var doctors = await Repository.GetAll();
            var orderedDoctors = doctors.OrderByDescending(d => d.OverallRating).ToList();
            return orderedDoctors;
        }

        public Task<Doctor> GetDoctorByID(int id)
        {
            return Repository.GetByID(id);
        }

        public async Task<List<Doctor>?> GetDoctorCondition(Func<Doctor, bool> condition)
        {
            var doctors = await Repository.GetAllByConditon(condition);
            return doctors;
        }

        public async Task<List<Doctor>?> GetDoctorConditionByPage(int pageNum, int pageSize, Expression<Func<Doctor, bool>> condition)
        {
            var doctors = await Repository.GetFilteredByConditonPages(condition, pageNum, pageSize);
            return doctors;
        }
        public async Task<int> GetDoctorConditionCount(Expression<Func<Doctor, bool>> condition)
        {
            var count = await Repository.GetFilteredByConditonCount(condition);
            return count;
        }
    }

}

