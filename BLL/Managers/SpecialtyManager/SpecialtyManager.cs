using DAL.Repositories.Specialties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vezeetaApplicationAPI.Models;

namespace BLLServices.Managers.SpecialtyManager
{
    public class SpecialtyManager
    {
        private readonly ISpecialtyRepository _specialtyRepository;
        public SpecialtyManager(ISpecialtyRepository specialtyRepository)
        {
            _specialtyRepository = specialtyRepository;
        }
        public Task<List<Specialty>> GetAllSpecialties()
        {
            return _specialtyRepository.GetAll();
        }
        public Task<Specialty> GetSpecialtyById(int id)
        {
            return _specialtyRepository.GetByID(id);
        }
        public void AddSpecialty(Specialty specialty)
        {
            _specialtyRepository.Add(specialty);
        }
        public void UpdateSpecialty(Specialty specialty)
        {
            _specialtyRepository.Update(specialty);
        }
        public void DeleteSpecialty(Specialty specialty)
        {
            _specialtyRepository.Delete(specialty);
        }
        public Task<ICollection<Doctor>> GetSpecialtyDoctors(int id)
        {
            return _specialtyRepository.GetSpecialtyDoctors(id);
        }

    }
}
