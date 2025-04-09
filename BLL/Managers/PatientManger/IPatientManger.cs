using DAL.Repositories.Patients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vezeetaApplicationAPI.Models;

namespace BLLServices.Managers.PatientManger
{
    public interface IPatientManger
    {
        public Task AddPatient(Patient patientVM);
        public Task UpdatePatient(Patient patientVM);

        public Task<Patient> GetPatientInfo(int patientID);
        public Task<List<Patient>> GetAllPatients();

    }
}
