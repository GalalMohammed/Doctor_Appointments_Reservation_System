using AutoMapper;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using vezeetaApplicationAPI.DataAccess;
using vezeetaApplicationAPI.Models;

namespace BLLServices.Managers.PatientManger
{

    public class PatientManger
    {
        AppDbContext _context;
        IMapper _mapper;
        Patient _patient;
        public PatientManger(AppDbContext context)
        {
            _context = context;
            #region using AutoMapper
            var config =
                new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<PatientVM, Patient>()
                        .ForMember(p => p.AppUser, opt => opt.MapFrom(src => new AppUser
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
        public async Task AddPatient(PatientVM patientVM)
        {
            var newPatient = _mapper.Map<Patient>(patientVM);
            _context.Patients.Add(newPatient);

            await _context.SaveChangesAsync();
        }
        public async Task UpdatePatient(PatientVM patientVM)
        {
            var patient = await _context.Patients
                .Include(p => p.AppUser)
                .FirstOrDefaultAsync(p => p.ID == patientVM.ID);
            if (patient is null)
            {
                throw new Exception("Patient does not exist");
            }
            else
            {
                _mapper.Map(patientVM, patient);

                await _context.SaveChangesAsync();
            }
        }


    }
}
