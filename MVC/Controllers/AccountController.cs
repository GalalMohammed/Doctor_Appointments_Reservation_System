using BLLServices.Common.EmailService;
using BLLServices.Common.ReCaptchaService;
using BLLServices.Common.UploadService;
using BLLServices.Managers.DoctorManger;
using BLLServices.Managers.DoctorReservationManager;
using BLLServices.Managers.PatientManger;
using BLLServices.Managers.SpecialtyManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC.Mappers;
using MVC.ViewModels;
using System.Security.Claims;
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
        private readonly IDoctorManager doctorManager;
        private readonly IDoctorMapper doctorMapper;
        private readonly ISpecialtyManager specialtyManager;
        private readonly IUploadService uploadService;
        private readonly IDoctorReservationManager reservationManager;
        private readonly ReCaptchaService reCaptchaService;

        public AccountController(
            SignInManager<AppUser> signInManager,
            UserManager<AppUser> userManager,
            IPatientManger patientManager,
            PatientMapper patientMapper,
            IEmailService emailService,
            IDoctorManager doctorManager,
            IDoctorMapper doctorMapper,
            ISpecialtyManager specialtyManager,
            IUploadService uploadService,
            IDoctorReservationManager reservationManager,
            ReCaptchaService reCaptchaService
            )
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.patientManager = patientManager;
            this.patientMapper = patientMapper;
            this.emailService = emailService;
            this.doctorManager = doctorManager;
            this.doctorMapper = doctorMapper;
            this.specialtyManager = specialtyManager;
            this.uploadService = uploadService;
            this.reservationManager = reservationManager;
            this.reCaptchaService = reCaptchaService;
        }
        public async Task<IActionResult> ConfirmEmail(string email, string token)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user is null)
            {
                throw new Exception($"Email is not existing !!\n {email}");
            }
            var result = await userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
                return View();
            throw new Exception();
        }
        public IActionResult Login()
        {
            LoginViewModel loginUser = new()
            {
                ExternalLogins = signInManager.GetExternalAuthenticationSchemesAsync().Result
            };
            ViewBag.IconClasses = new Dictionary<string, string>
            {
                { "Google", "fa-brands fa-google-plus-g" },
                { "Facebook", "fa-brands fa-facebook-f" },
                { "Microsoft", "fa-brands fa-windows" }
            };
            return View(loginUser);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginUser)
        {
            loginUser.ExternalLogins = signInManager.GetExternalAuthenticationSchemesAsync().Result;
            ViewBag.IconClasses = new Dictionary<string, string>
            {
                { "Google", "fa-brands fa-google-plus-g" },
                { "Facebook", "fa-brands fa-facebook-f" },
                { "Microsoft", "fa-brands fa-windows" }
            };
            if (ModelState.IsValid)
            {
                var appUser = await userManager.FindByEmailAsync(loginUser.Email);
                if (appUser != null)
                {
                    if (!appUser.EmailConfirmed)
                    {
                        ViewBag.emailNotConfirmed = true;
                        return View(loginUser);
                    }
                    bool correctPassword = await userManager.CheckPasswordAsync(appUser, loginUser.Password);
                    if (correctPassword)
                    {
                        await signInManager.SignInAsync(appUser, false);
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            ModelState.AddModelError("", "Invalid email or password");
            return View(loginUser);
        }
        public IActionResult ExternalLogin(string provider, string returnUrl = "")
        {
            // Request a redirect to the external login provider
            var redirectUrl = Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl });
            var properties = signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return new ChallengeResult(provider, properties);
        }
        public async Task<IActionResult> ExternalLoginCallback(string _ = "", string remoteError = "")
        {
            LoginViewModel loginUser = new()
            {
                ExternalLogins = await signInManager.GetExternalAuthenticationSchemesAsync()
            };
            ViewBag.IconClasses = new Dictionary<string, string>
            {
                { "Google", "fa-brands fa-google-plus-g" },
                { "Facebook", "fa-brands fa-facebook-f" },
                { "Microsoft", "fa-brands fa-microsoft" }
            };
            if (!string.IsNullOrEmpty(remoteError))
            {
                ModelState.AddModelError(string.Empty, $"Error from external login provider: {remoteError}");
                return View("Login", loginUser);
            }
            var loginInfo = await signInManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                ModelState.AddModelError(string.Empty, "Error loading external login information.");
                return View("Login", loginUser);
            }
            string? email = loginInfo.Principal.FindFirstValue(ClaimTypes.Email);
            if (email == null) {
                ModelState.AddModelError(string.Empty, "Email not found in external login.");
                return View("Login", loginUser);
            }
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Email not found. Please register first.");
                return RedirectToAction(nameof(Register));
            }
            // Check if the user already has a login for this provider
            var userLogins = await userManager.GetLoginsAsync(user);
            if (!userLogins.Any(x => x.LoginProvider == loginInfo.LoginProvider && x.ProviderKey == loginInfo.ProviderKey))
            {
                // Add the login information to the user
                var result = await userManager.AddLoginAsync(user, loginInfo);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError(string.Empty, "Error adding external login.");
                    return View("Login", loginUser);
                }
            }
            // Sign in the user with the external login provider
            // Check if the user is already signed in
            if (signInManager.IsSignedIn(User))
            {
                // If the user is already signed in, we can just add the login information
                await signInManager.UpdateExternalAuthenticationTokensAsync(loginInfo);
                return RedirectToAction("Index", "Home");
            }
            // If the user is not signed in, we need to sign them in
            Microsoft.AspNetCore.Identity.SignInResult signInResult = await signInManager.ExternalLoginSignInAsync(loginInfo.LoginProvider, loginInfo.ProviderKey, isPersistent: false, bypassTwoFactor: true);
            if (!signInResult.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View("Login", loginUser);
            }
            if (!user.EmailConfirmed)
            {
                await userManager.ConfirmEmailAsync(user, await userManager.GenerateEmailConfirmationTokenAsync(user));
            }
            return RedirectToAction("Index", "Home");
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
                // Validate reCAPTCHA
                string googleReCaptchaResponse = Request.Form["g-recaptcha-response"].ToString();

                // Verify the token
                bool isValidCaptcha = await reCaptchaService.ValidateReCaptcha(googleReCaptchaResponse);
                if (!isValidCaptcha)
                {
                    ModelState.AddModelError("", "Invalid reCAPTCHA. Please try again.");
                    return View(registerUser);
                }
                // Proceed with user registration
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
                        await patientManager.AddPatient(newPatient);
                        await userManager.AddToRoleAsync(appUser, "patient");
                        emailService.SendEmail(new Email
                        {
                            To = registerUser.Email,
                            Subject = "Confirm Your Email",
                            Link = Url.Action("ConfirmEmail", "Account", new { email = registerUser.Email, token = await userManager.GenerateEmailConfirmationTokenAsync(appUser) }, Request.Scheme),
                            Template = MailTemplates.ConfirmEmailTemplate
                        }, $"{registerUser.FirstName} {registerUser.LastName}");
                        return RedirectToAction("NeedToConfirm");
                    }
                    foreach (var error in created.Errors)
                        ModelState.AddModelError("", error.Description);
                }
                ModelState.AddModelError("", "Email already exists");
            }
            return View(registerUser);
        }
        [Route("account/doctor-register")]
        public async Task<IActionResult> DoctorRegister()
        {
            var specialties = await specialtyManager.GetAllSpecialties();
            ViewBag.Specialties = new SelectList(specialties, "ID", "Name");
            return View(new DoctorRegisterViewModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("account/doctor-register")]
        public async Task<IActionResult> DoctorRegister(DoctorRegisterViewModel doctorRegister)
        {
            if (ModelState.IsValid)
            {
                var appUser = new AppUser()
                {
                    FirstName = doctorRegister.FirstName,
                    LastName = doctorRegister.LastName,
                    UserName = doctorRegister.Email,
                    Email = doctorRegister.Email,
                    PhoneNumber = doctorRegister.PhoneNumber,
                    BirthDate = doctorRegister.BirthDate.ToDateTime(new TimeOnly(0, 0))
                };
                var existingUser = await userManager.FindByEmailAsync(doctorRegister.Email);
                if (existingUser == null)
                {
                    IdentityResult created = await userManager.CreateAsync(appUser, doctorRegister.Password);
                    if (created.Succeeded)
                    {
                        var newDoctor = doctorMapper.MapToDoctorFromRegister(doctorRegister);
                        newDoctor.AppUserID = appUser.Id;
                        var imageURL = await uploadService.UploadFile(doctorRegister.Image);
                        newDoctor.ImageURL = imageURL;
                        await doctorManager.AddDoctor(newDoctor);
                        await userManager.AddToRoleAsync(appUser, "doctor");
                        reservationManager.GenerateCalanderReservation(newDoctor, 20);
                        emailService.SendEmail(new Email
                        {
                            To = doctorRegister.Email,
                            Subject = "Confirm Your Email",
                            Link = Url.Action("ConfirmEmail", "Account", new { email = doctorRegister.Email, token = await userManager.GenerateEmailConfirmationTokenAsync(appUser) }, Request.Scheme),
                            Template = MailTemplates.ConfirmEmailTemplate
                        }, $"{doctorRegister.FirstName} {doctorRegister.LastName}");
                        return RedirectToAction("NeedToConfirm");
                    }
                    foreach (var error in created.Errors)
                        ModelState.AddModelError("", error.Description);
                }
                ModelState.AddModelError("", "Email already exists");
            }
            var specialties = await specialtyManager.GetAllSpecialties();
            ViewBag.Specialties = new SelectList(specialties, "ID", "Name");
            return View(doctorRegister);
        }
        public IActionResult NeedToConfirm()
        {
            return View();
        }

        public IActionResult ForgetPassword()
        {
            ViewBag.emailIsExist = "";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordVM forgetPasswordVM)
        {
            #region need some sort of checking (If Email Already Exist in DB)
            var user = await userManager.FindByEmailAsync(forgetPasswordVM.Email);
            //object user = null;


            ViewBag.emailIsExist = user is not null ? "NotExist" : "Exist";


            #endregion
            if (ModelState.IsValid && user is not null)
            {
                var token = await userManager.GeneratePasswordResetTokenAsync(user);
                string url = Url.Action("ResetPassword", "Account", new { email = user.Email, token = token }, Request.Scheme);

                emailService.SendEmail(new Email { To = forgetPasswordVM.Email, Subject = "Reset Your Password", Link = url, Template = MailTemplates.ForgotPasswordTemplate }, $"{user.FirstName} {user.LastName}");

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
        [ValidateAntiForgeryToken]
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
        [Route("account/change-password")]
        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        [Route("account/change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel changePasswordViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(User.Identity.Name);
                var result = await userManager.ChangePasswordAsync(user, changePasswordViewModel.OldPassword, changePasswordViewModel.NewPassword);
                if (result.Succeeded)
                {
                    ViewBag.Success = true;
                    return View();
                }
                ViewBag.Success = false;
                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeImage(ChangeImageVM image)
        {
            if (ModelState.IsValid)
            {
                var doc = await doctorManager.GetDoctorByID(image.ID);
                if (doc == null) TempData["Error"] = $"Doctor with ID {image.ID} was not found";


                doc.ImageURL = await uploadService.UploadFile(image.File);


                await doctorManager.UpdateDoctor(doc);

                TempData["Updated"] = "Image was updated successfully";
                return RedirectToAction("profile","doctor",new {id = image.ID});
            }
            else
            {
                TempData["Error"] = string.Join("\n",
                    ModelState
                        .Where(m => m.Value.Errors.Any())
                        .SelectMany(m => m.Value.Errors.Select(e => $"{m.Key}: {e.ErrorMessage}\n"))
                );
                return RedirectToAction("profile", "doctor", new { id = image.ID });
            }
        }
    }
}
