using BLLServices.Common.EmailService;
using BLLServices.Common.PaymentService;
using BLLServices.Common.ReCaptchaService;
using BLLServices.Common.UploadService;
using BLLServices.Managers.AppointmentManager;
using BLLServices.Managers.DoctorManger;
using BLLServices.Managers.DoctorReservationManager;
using BLLServices.Managers.OrderManager;
using BLLServices.Managers.PatientManger;
using BLLServices.Managers.ReviewManager;
using BLLServices.Managers.SpecialtyManager;
using BLLServices.Payment;
using DAL.Repositories.Appointments;
using DAL.Repositories.DoctorReservations;
using DAL.Repositories.Doctors;
using DAL.Repositories.Generic;
using DAL.Repositories.Orders;
using DAL.Repositories.Patients;
using DAL.Repositories.Reviews;
using DAL.Repositories.Specialties;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MVC.Claims;
using MVC.Mappers;
using MVC.Middlewares;
using vezeetaApplicationAPI.DataAccess;
using vezeetaApplicationAPI.Models;

namespace MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            // DbContext Configuration
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("Test")));

            // Services Configuration
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
            builder.Services.AddScoped<IDoctorReservationRepository, DoctorReservationRepository>();
            builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
            builder.Services.AddScoped<ISpecialtyRepository, SpecialtyRepository>();
            builder.Services.AddScoped<IPatientRepository, PatientRepository>();
            builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            builder.Services.AddScoped<IDoctorReservationRepository, DoctorReservationRepository>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<IDoctorReservationManager, DoctorReservationManager>();
            builder.Services.AddScoped<ISpecialtyManager, SpecialtyManager>();
            builder.Services.AddScoped<IDoctorManager, DoctorManager>();
            builder.Services.AddScoped<IReviewManager, ReviewManager>();
            builder.Services.AddScoped<IDoctorMapper, DoctorMapper>();
            builder.Services.AddScoped<IPatientManger, PatientManger>();
            builder.Services.AddScoped<IOrderManager, OrderManager>();
            builder.Services.AddScoped<IAppointmentManager, AppointmentManager>();
            builder.Services.AddScoped<PatientMapper, PatientMapper>();
            builder.Services.AddScoped<HomeMapper, HomeMapper>();
            builder.Services.AddScoped<ReCaptchaService, ReCaptchaService>(provider =>
            {
                string secretKey = builder.Configuration["ReCaptchaSettings:SecretKey"] ?? throw new Exception("SecretKey is not set");
                string verificationUrl = builder.Configuration["ReCaptchaSettings:ApiUrl"] ?? throw new Exception("VerificationUrl is not set");
                return new ReCaptchaService(secretKey, verificationUrl);
            });

            #region Common Services
            builder.Services.AddScoped<IEmailService, EmailService>();
            builder.Services.AddScoped<IPaymentService, PaymentService>();

            builder.Services.AddScoped<IUploadService, UploadService>(provider =>
            {
                var webHostEnvironment = provider.GetRequiredService<IWebHostEnvironment>();

                string rootPath = webHostEnvironment.WebRootPath;

                return new UploadService(rootPath);

            });

            #endregion

            // Identity Configuration
            builder.Services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
            builder.Services.AddScoped<IUserClaimsPrincipalFactory<AppUser>, CustomClaimsPrincipalFactory>();

            builder.Services.AddControllersWithViews();
            builder.Services.AddSingleton(x =>
            new PayPalClient(
                clientId: builder.Configuration["PayPalOptions:ClientId"] ?? throw new Exception("ClientId is not set"),
                clientSecret: builder.Configuration["PayPalOptions:ClientSecret"] ?? throw new Exception("ClientSecret is not set"),
                baseUrl: builder.Configuration["PayPalOptions:Url"] ?? throw new Exception("BaseUrl is not set")
            )
            );
            var app = builder.Build();

            // Exception Handling



            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
                app.UseExceptionHandler("/Home/Error");
            else
            {
                app.UseMiddleware<CustomExceptionHandler>();
                app.UseStatusCodePagesWithReExecute("/Error");
                //app.UseDeveloperExceptionPage();
            }
            // Middleware Configuration
            app.MapStaticAssets();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();
            app.Run();
        }



    }
}
