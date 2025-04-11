using BLLServices.Managers.DoctorManger;
using BLLServices.Managers.DoctorReservationManager;
using BLLServices.Managers.PatientManger;
using BLLServices.Managers.ReviewManager;
using BLLServices.Managers.SpecialtyManager;
using BLLServices.Common;
using DAL.Repositories.DoctorReservations;
using DAL.Repositories.Doctors;
using DAL.Repositories.Generic;
using DAL.Repositories.Patients;
using DAL.Repositories.Reviews;
using DAL.Repositories.Specialties;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MVC.Claims;
using MVC.Mappers;
using vezeetaApplicationAPI.DataAccess;
using vezeetaApplicationAPI.Models;
using BLLServices.Common.EmailService;
using BLLServices.Common.UploadService;
using Microsoft.DotNet.Scaffolding.Shared;

namespace MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("Test")));

            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
            builder.Services.AddScoped<IDoctorReservationRepository, DoctorReservationRepository>();
            builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
            builder.Services.AddScoped<ISpecialtyRepository, SpecialtyRepository>();
            builder.Services.AddScoped<IPatientRepository, PatientRepository>();
            builder.Services.AddScoped<IDoctorReservationManager, DoctorReservationManager>();
            builder.Services.AddScoped<ISpecialtyManager, SpecialtyManager>();
            builder.Services.AddScoped<IDoctorManager, DoctorManager>();
            builder.Services.AddScoped<IReviewManager, ReviewManager>();
            builder.Services.AddScoped<IDoctorMapper, DoctorMapper>();
            builder.Services.AddScoped<IPatientManger, PatientManger>();
            builder.Services.AddScoped<PatientMapper, PatientMapper>();
            #region Common Services
            builder.Services.AddScoped<IEmailService, EmailService>();

            builder.Services.AddScoped<IUploadService, UploadService>(provider =>
            {
                var webHostEnvironment = provider.GetRequiredService<IWebHostEnvironment>();

                string rootPath = webHostEnvironment.WebRootPath; 

                return new UploadService(rootPath);

            });

            #endregion
            builder.Services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
            builder.Services.AddScoped<IUserClaimsPrincipalFactory<AppUser>, CustomClaimsPrincipalFactory>();


            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }



    }
}
