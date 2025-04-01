using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vezeetaApplicationAPI.DataAccess;
using vezeetaApplicationAPI.Models;

namespace BLLServices.Managers.DoctorManger
{
   
    public class DoctorManger
    {
        AppDbContext _context;
        IMapper _mapper;
        public DoctorManger(AppDbContext context)
        {
            _context = context;
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
            _context.Doctors.Add(newDoctor);

            await _context.SaveChangesAsync();
        }
        public async Task UpdateDoctor(DoctorVM doctorVM)
        {
            var doctor = await _context.Doctors
                .Include(d => d.AppUser)
                .FirstOrDefaultAsync(d => d.ID == doctorVM.ID);
            if (doctor is null)
            {
                throw new Exception("Doctor does not exist");
            }
            else
            {
                _mapper.Map(doctorVM, doctor);

                await _context.SaveChangesAsync();
            }
        }
        public async Task GetDoctorInfo(int doctorID)
        {
            var doctor = await _context.Doctors
                .Include(d => d.Reviews).Include(d => d.Specialty).Include(d => d.DoctorReservations)
                .FirstOrDefaultAsync(d => d.ID == doctorID);
            if (doctor is null)
            {
                throw new Exception("Doctor does not exist");
            }
            else
            {
                var reviews = doctor.Reviews;
            }
        }


    }

}

