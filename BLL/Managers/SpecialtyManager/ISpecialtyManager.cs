using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vezeetaApplicationAPI.Models;

namespace BLLServices.Managers.SpecialtyManager
{
    public interface ISpecialtyManager
    {
        public Task<List<Specialty>> GetAllSpecialties();
        public Task<Specialty> GetSpecialtyById(int id);
        public void AddSpecialty(Specialty specialty);
        public void UpdateSpecialty(Specialty specialty);
        public void DeleteSpecialty(Specialty specialty);
        public Task<ICollection<Doctor>> GetSpecialtyDoctors(int id);

    }
}
