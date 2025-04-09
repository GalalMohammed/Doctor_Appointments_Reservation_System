using BLLServices.Managers.PatientManger;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC.ViewModels;
using vezeetaApplicationAPI.Models;

namespace MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<AppUser> signInManager;
        private readonly UserManager<AppUser> userManager;
        private readonly IPatientManger patientManager;

        public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IPatientManger patientManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.patientManager = patientManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ConfirmEmail(string? token)
        {
            // =============== Function Skeleton ===============
            //if (token != null)
            //{
            //    int? id = GetIdFromToken(token);
            //    if (id != null)
            //    {
            //        SaveToDb(id);
            return View();
            //    }
            //}
            //return View("CustomError");
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginUser)
        {

            // =============== Function Skeleton ===============
            //if (ModelState.IsValid)
            //{
            //    var appUser = await userManager.FindByNameAsync(loginUser.Username);
            //    if (appUser != null)
            //    {
            //        bool correctPassword = await userManager.CheckPasswordAsync(appUser, loginUser.Password);
            //        if (correctPassword)
            //        {
            //            await signInManager.SignInAsync(appUser, loginUser.RememberMe);
            //            return RedirectToAction("Index", "Customer");
            //        }
            //    }
            //}
            ModelState.AddModelError("", "Invalid username or password");
            return View(loginUser);
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel registerUser)
        {
            if (ModelState.IsValid)
            {
                var appUser = new AppUser()
                {
                    FirstName = registerUser.FirstName,
                    LastName = registerUser.LastName,
                    Email = registerUser.Email,
                    PhoneNumber = registerUser.PhoneNumber
                };
                IdentityResult created = await userManager.CreateAsync(appUser, registerUser.Password);
                if (created.Succeeded)
                {
                    // Bypassing Layers
                    //var patient = new PatientVM()
                    //{
                    //    AppUserID = appUser.Id,
                    //    FirstName = registerUser.FirstName,
                    //    LastName = registerUser.LastName,
                    //    Location = registerUser.Location,
                    //    BirthDate = registerUser.BirthDate
                    //};
                    //patientManager.AddPatient()
                    await userManager.AddToRoleAsync(appUser, "patient");
                    await signInManager.SignInAsync(appUser, false);


                    var patient = MapToPatient(patientvm);
                    patient.appuserid= appUser.Id;
                    patientManager.AddPatient(patient);
                    return RedirectToAction("Index", "Home");
                }
                foreach (var error in created.Errors)
                    ModelState.AddModelError("", error.Description);
            }
            return View(registerUser);
        }

        public IActionResult ForgetPassword()
        {

            return View();
        }

        [HttpPost]
        public IActionResult ForgetPassword(ForgetPasswordVM forgetPasswordVM)
        {
            #region need some sort of checking (If Email Already Exist in DB)

            #endregion
            if (ModelState.IsValid)
            {
                return RedirectToAction("ResetPassword");
            }
            return View(forgetPasswordVM);
        }



        [HttpGet]
        public IActionResult ResetPassword()
        {
            return View(new ResetPasswordVM()); // Load the empty form
        }

        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model); // Return the view with validation errors
            }

            // TODO: Implement password reset logic (e.g., update user password in database)

            TempData["SuccessMessage"] = "Password reset successfully!";
            return RedirectToAction("Search", "Doctor"); // Redirect to login page after successful reset
        }
    }
}
