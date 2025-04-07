using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using vezeetaApplicationAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using DAL.Models;

namespace vezeetaApplicationAPI.DataAccess
{


    public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<AppUser>(options)
    {
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Specialty> Specialties { get; set; }
        public DbSet<DoctorReservation> DoctorReservations { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Ignore<Person>();
            modelBuilder.Entity<Doctor>()
                .HasOne(d => d.Specialty)
                .WithMany(s => s.Doctors)
                .HasForeignKey(d => d.SpecialtyID);

            modelBuilder.Entity<DoctorReservation>()
                .HasOne(dr => dr.Doctor)
                .WithMany(d => d.DoctorReservations)
                .HasForeignKey(dr => dr.DoctorID);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.DoctorReservation)
                .WithMany(dr => dr.Appointments)
                .HasForeignKey(a => a.DoctorReservationID);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Patient)
                .WithMany(p => p.Appointments)
                .HasForeignKey(a => a.PatientId);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.Patient)
                .WithMany(p => p.Reviews)
                .HasForeignKey(r => r.PatientID);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.Doctor)
                .WithMany(d => d.Reviews)
                .HasForeignKey(r => r.DoctorID);
        }
    }

}
