using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vezeetaApplicationAPI.DataAccess;
using vezeetaApplicationAPI.Models;
using DAL.Repositories.Doctors;

namespace BLLServices.Managers.DoctorManger
{
   
    public class DoctorManger : IDoctorManger
    {
        IMapper _mapper;
        DoctorRepository Repository;
        public DoctorManger(DoctorRepository repo)
        {
            Repository = repo;
            #region using AutoMapper
            var config =
                new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<DoctorVM, Doctor>()
                        .ForMember(D => D.AppUser, opt => opt.MapFrom(src => new AppUser
                        {
                            FirstName = src.FirstName,
                            LastName = src.LastName,
                            Gender = src.Gender,
                            BirthDate = src.BirthDate,
                        }));
                });
            _mapper = config.CreateMapper();
            #endregion


        }
        public async Task AddDoctor(DoctorVM doctorVM)
        {
            var newDoctor = _mapper.Map<Doctor>(doctorVM);
            Repository.Add(newDoctor);

        }
        public async Task UpdateDoctor(DoctorVM doctorVM)
        {
            var doctor = await Repository.GetByID(doctorVM.ID);
            if (doctor is null)
            {
                throw new Exception("Doctor does not exist");
            }
            else
            {
                _mapper.Map(doctorVM, doctor);
                Repository.Update(doctor);
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
        public async Task<List<Doctor>> GetDoctorsOrderedByrating()
        {
            var doctors = await Repository.GetAll();
            var orderedDoctors = doctors.OrderByDescending(d => d.OverallRating).ToList();
            return orderedDoctors;
        }


    }

}

