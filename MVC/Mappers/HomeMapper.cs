using BLLServices.Managers.DoctorManger;
using BLLServices.Managers.SpecialtyManager;
using MVC.ViewModels;
using vezeetaApplicationAPI.Models;

namespace MVC.Mappers
{
    public class HomeMapper
    {
        private readonly ISpecialtyManager _specialtyManager;
        private readonly IDoctorManager _doctorManger;
        public HomeMapper(ISpecialtyManager specialtyManager, IDoctorManager doctorManger)
        {
            _specialtyManager = specialtyManager;
            _doctorManger = doctorManger;
        }
        public HomeDoctor MapToHomeDoctor(Doctor doctor)
        {
            throw new NotImplementedException();
        }
        public async HomeVM MapToHomeVM()
        {
            return new HomeVM()
            {
                Specialties = _specialtyManager.GetAllSpecialties().Result.Select(s => s.Name).ToList(),
                Doctors = _doctorManger.GetDoctorsOrderedByrating().Result.
                Select(doc => new HomeDoctor()
                {
                    ID = doc.ID,
                    Name = doc.FirstName + " " + doc.LastName,
                    Specialty = doc.Specialty?.Name,
                    Image = doc.ImageURL,
                    Rating = doc.OverallRating,
                    Location = doc.Location,

                }).ToList(),
                DoctorsPerSpecialtyConunt = new()
                {
                    { 0,  _specialtyManager.GetSpecialtyDoctors(0).Result.Count},
                    { 1,  _specialtyManager.GetSpecialtyDoctors(1).Result.Count},
                    { 2,  _specialtyManager.GetSpecialtyDoctors(2).Result.Count},
                    { 3,  _specialtyManager.GetSpecialtyDoctors(3).Result.Count},
                    { 4,  _specialtyManager.GetSpecialtyDoctors(4).Result.Count},
                    { 5,  _specialtyManager.GetSpecialtyDoctors(5).Result.Count},
                    { 6,  _specialtyManager.GetSpecialtyDoctors(6).Result.Count},
                    { 7,  _specialtyManager.GetSpecialtyDoctors(7).Result.Count},
                    
                }

                
            };
        }
    }
}
