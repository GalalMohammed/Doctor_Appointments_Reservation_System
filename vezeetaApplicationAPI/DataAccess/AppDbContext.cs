using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using vezeetaApplicationAPI.Models;

namespace vezeetaApplicationAPI.DataAccess
{


    public class AppDbContext : DbContext
    {
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Specialties> Specialties { get; set; }
        public DbSet<DoctorReservation> DoctorReservations { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Doctor>()
                .HasOne(d => d.Specialties)
                .WithMany(s => s.Doctors)
                .HasForeignKey(d => d.Specialties_ID);

            modelBuilder.Entity<DoctorReservation>()
                .HasOne(dr => dr.Doctor)
                .WithMany(d => d.DoctorReservations)
                .HasForeignKey(dr => dr.Doctor_ID);

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
                .HasForeignKey(r => r.Patient_ID);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.Doctor)
                .WithMany(d => d.Reviews)
                .HasForeignKey(r => r.Doctor_ID);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=VezeetaDB;Trusted_Connection=True;TrustServerCertificate=True;");
        }
    }

}
