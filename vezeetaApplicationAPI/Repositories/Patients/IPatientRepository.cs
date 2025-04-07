using DAL.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vezeetaApplicationAPI.Models;

namespace DAL.Repositories.Patients
{
    public interface IPatientRepository : IGenericRepository<Patient>
    {
    }
}
