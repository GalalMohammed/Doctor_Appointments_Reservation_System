using AutoMapper;
using DAL.Repositories.Patients;
using vezeetaApplicationAPI.Models;
namespace BLLServices.Managers.PatientManger
{

    public class PatientManger : IPatientManger
    {
        private IPatientRepository patientRepository;
        IMapper _mapper;
        Patient _patient;
        public PatientManger(IPatientRepository repo)
        {
            patientRepository = repo;
            #region using AutoMapper
            //var config =
            //    new MapperConfiguration(cfg =>
            //    {
            //        cfg.CreateMap<PatientVM, Patient>()
            //            .ForMember(p => p.AppUser, opt => opt.MapFrom(src => new AppUser
            //            {
            //                FirstName = src.FirstName,
            //                LastName = src.LastName,
            //                Gender = src.Gender,
            //                BirthDate = src.BirthDate,
            //            }));
            //    });
            //_mapper = config.CreateMapper();
            #endregion


        }
        public async Task AddPatient(Patient patient)
        {
            //var newPatient = _mapper.Map<Patient>(patientVM);
            patientRepository.Add(patient);

        }
        public async Task UpdatePatient(Patient patientVM)
        {
            if (patientVM is null)
            {
                throw new Exception("Patient does not exist");
            }
            else
            {
                //_mapper.Map(patientVM, patient);
                patientRepository.Update(patientVM);

            }
        }
        public async Task<Patient> GetPatientInfo(int patientID)
        {
            var patient = await patientRepository.GetByID(patientID);
            if (patient is null)
            {
                throw new Exception("Patient does not exist");
            }
            else
            {
                return patient;
            }
        }
        public async Task<List<Patient>> GetAllPatients()
        {
            var patients = await patientRepository.GetAll();
            if (patients is null)
            {
                throw new Exception("No Patients Exist");
            }
            else
            {
                return patients.ToList();
            }
        }

        public async Task<List<Patient>?> GetPatientCondition(Func<Patient, bool> condition)
        {
            var patients = await patientRepository.GetAllByConditon(condition);
            return patients;
        }
    }
}
