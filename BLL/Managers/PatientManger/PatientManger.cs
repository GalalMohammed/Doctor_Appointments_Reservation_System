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
using DAL.Repositories.Patients; 
namespace BLLServices.Managers.PatientManger
{

    public class PatientManger : IPatientManger
    {
        private PatientRepository patientRepository;
        IMapper _mapper;
        Patient _patient;
        public PatientManger(PatientRepository repo)
        {
            patientRepository = repo;
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
             patientRepository.Add(newPatient);

        }
        public async Task UpdatePatient(PatientVM patientVM)
        {
            var patient = await patientRepository.GetByID(patientVM.ID);
            if (patient is null)
            {
                throw new Exception("Patient does not exist");
            }
            else
            {
                _mapper.Map(patientVM, patient);
                patientRepository.Update(patient);

            }
        }

    }
}
