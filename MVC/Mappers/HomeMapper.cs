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
        public HomeVM MapToHomeVM()
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

                }).ToList()
            };
        }
    }
}
