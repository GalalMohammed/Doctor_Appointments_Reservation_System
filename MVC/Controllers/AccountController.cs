using BLLServices.Common.EmailService;
using BLLServices.Managers.PatientManger;
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
        private readonly IEmailService emailService;

        public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IPatientManger patientManager, PatientMapper patientMapper,IEmailService emailService)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.patientManager = patientManager;
            this.patientMapper = patientMapper;
            this.emailService = emailService;
        }
        public async Task<IActionResult> ConfirmEmail(string email,string token)
        {
            var user = await userManager.FindByEmailAsync(email);

            if(user is  null)
            {
                throw new Exception($"Email is not existing !!\n {email}");   
            }
           

            userManager.ConfirmEmailAsync(user, token);
            return View();
            
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
                    UserName = $"{registerUser.FirstName[0]}_{registerUser.LastName}",
                    Email = registerUser.Email,
                    PhoneNumber = registerUser.PhoneNumber,
                    BirthDate = registerUser.BirthDate.ToDateTime(new TimeOnly(0, 0))
                };

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
            return View(registerUser);
        }

        public IActionResult ForgetPassword()
        {
            ViewBag.emailIsExist = "";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordVM forgetPasswordVM)
        {
            #region need some sort of checking (If Email Already Exist in DB)
            var user =  await userManager.FindByEmailAsync(forgetPasswordVM.Email);
            //object user = null;


            ViewBag.emailIsExist = user is not null? "NotExist" : "Exist"; 
            

            #endregion
            if (ModelState.IsValid&& user is not null)
            {
                var token = await userManager.GeneratePasswordResetTokenAsync(user);
                string url = Url.Action("ResetPassword", "Account", new {email=user.Email,token =token}, Request.Scheme);

                emailService.SendEmail(new Email{ To=forgetPasswordVM.Email ,Subject="Reset Your Password",Body=url });
                
            }

            return View(forgetPasswordVM);

        }



        [HttpGet]
        public IActionResult ResetPassword(string email, string token)
        {
            TempData["email"] = email;
            TempData["token"] = token;

            return View(new ResetPasswordVM()); // Load the empty form
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM model)
        {
            if (ModelState.IsValid) 
            {
                var email = TempData["email"] as string;
                var token = TempData["token"] as string;

                var user = await userManager.FindByEmailAsync(email);
                var result = await userManager.ResetPasswordAsync(user, token, model.ConfirmPassword);

                if (result.Succeeded)
                {
                    TempData["SuccessMessage"] = "Password reset successfully!";
                    return RedirectToAction(nameof(Login));
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, item.Description); 
                    }
                }
            }
            
                return View(model); // Return the view with validation errors
            
                                            
        }
    }
}
