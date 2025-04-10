using vezeetaApplicationAPI.Models;

namespace BLLServices.Managers.PatientManger
{
    public interface IPatientManger
    {
        public Task AddPatient(Patient patientVM);
        public Task UpdatePatient(Patient patientVM);

        public Task<Patient> GetPatientInfo(int patientID);
        public Task<List<Patient>> GetAllPatients();

        public Task<List<Patient>?> GetPatientCondition(Func<Patient, bool> condition);
    }
}
