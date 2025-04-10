using BLLServices.Managers.PatientManger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC.Mappers;
using MVC.ViewModels;
using vezeetaApplicationAPI.Models;

namespace MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<AppUser> signInManager;
        private readonly UserManager<AppUser> userManager;
        private readonly IPatientManger patientManager;
        private readonly PatientMapper patientMapper;

        public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IPatientManger patientManager, PatientMapper patientMapper)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.patientManager = patientManager;
            this.patientMapper = patientMapper;
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
            if (ModelState.IsValid)
            {
                var appUser = await userManager.FindByEmailAsync(loginUser.Email);
                if (appUser != null)
                {
                    bool correctPassword = await userManager.CheckPasswordAsync(appUser, loginUser.Password);
                    if (correctPassword)
                    {
                        await signInManager.SignInAsync(appUser, false);
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            ModelState.AddModelError("", "Invalid username or password");
            return View(loginUser);
        }
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
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
                    UserName = registerUser.Email,
                    Email = registerUser.Email,
                    PhoneNumber = registerUser.PhoneNumber,
                    BirthDate = registerUser.BirthDate.ToDateTime(new TimeOnly(0, 0))
                };
                var existingUser = await userManager.FindByEmailAsync(registerUser.Email);
                if (existingUser == null)
                {
                    IdentityResult created = await userManager.CreateAsync(appUser, registerUser.Password);
                    if (created.Succeeded)
                    {
                        var newPatient = patientMapper.MapToPatient(registerUser);
                        newPatient.AppUserID = appUser.Id;
                        patientManager.AddPatient(newPatient);
                        await userManager.AddToRoleAsync(appUser, "patient");
                        await signInManager.SignInAsync(appUser, false);
                        return RedirectToAction("Index", "Home");
                    }
                    foreach (var error in created.Errors)
                        ModelState.AddModelError("", error.Description);
                }
                ModelState.AddModelError("", "Email already exists");
            }
            return View(registerUser);
        }

        [Authorize]
        public IActionResult ForgetPassword()
        {

            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
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
        [Authorize]
        [ValidateAntiForgeryToken]
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
        [Route("account/change-password")]
        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        [Route("account/change-password")]
        public IActionResult ChangePassword(string oldPassword, string newPassword)
        {
            return View();
        }
    }
}
