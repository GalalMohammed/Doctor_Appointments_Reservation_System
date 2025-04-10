using BLLServices.Managers.DoctorManger;
using BLLServices.Managers.PatientManger;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using vezeetaApplicationAPI.Models;

namespace MVC.Claims
{
    public class CustomClaimsPrincipalFactory : UserClaimsPrincipalFactory<AppUser, IdentityRole>
    {
        private readonly IPatientManger patientManager;
        private readonly IDoctorManager doctorManager;

        public CustomClaimsPrincipalFactory(
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IOptions<IdentityOptions> optionsAccessor,
            IPatientManger patientManager,
            IDoctorManager doctorManager)
            : base(userManager, roleManager, optionsAccessor)
        {
            this.patientManager = patientManager;
            this.doctorManager = doctorManager;
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(AppUser user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            var doctors = await doctorManager.GetDoctorCondition(d => d.AppUserID == user.Id);
            var doctor = doctors?.FirstOrDefault();
            var patients = await patientManager.GetPatientCondition(p => p.AppUserID == user.Id);
            var patient = patients?.FirstOrDefault();
            if (doctor != null)
                identity.AddClaim(new Claim("currentId", $"{doctor.ID}"));
            if (patient != null)
                identity.AddClaim(new Claim("currentId", $"{patient.ID}"));
            return identity;
        }
    }

}
