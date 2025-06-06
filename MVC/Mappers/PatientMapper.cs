﻿using BLLServices.Managers.PatientManger;
using BLLServices.Managers.ReviewManager;
using MVC.Enums;
using MVC.ViewModels;
using vezeetaApplicationAPI.Models;

namespace MVC.Mappers
{
    public class PatientMapper
    {
        private readonly IPatientManger patientManager;
        private readonly IReviewManager reviewManager;

        public PatientMapper(IPatientManger patientManager, IReviewManager reviewManager)
        {
            this.patientManager = patientManager;
            this.reviewManager = reviewManager;
        }
        public AppointmentViewModel MapToAppointmentViewModel(Appointment appointment)
        {
            return new AppointmentViewModel
            {
                Id = appointment.ID,
                StartTime = appointment.DoctorReservation.StartTime,
                EndTime = appointment.DoctorReservation.EndTime,
                Doctor = appointment.DoctorReservation?.Doctor?.FirstName + " " + appointment.DoctorReservation?.Doctor?.LastName,
                Specialty = appointment.DoctorReservation?.Doctor?.Specialty?.Name ?? "Not Available",
                Governorate = appointment.DoctorReservation?.Doctor?.Governorate ?? Governorate.All,
                Location = appointment.DoctorReservation?.Doctor?.Location ?? "Not Available",
                DoctorImagePath = appointment.DoctorReservation?.Doctor?.ImageURL ?? "default_doctor.png",
                IsExists = reviewManager.GetByCondition(r => r.DoctorID == appointment.DoctorReservation.DoctorID && r.PatientID == appointment.PatientId).Result.Any()
            };
        }
        public PatientViewModel MapToPatientViewModel(Patient patient)
        {
            return new PatientViewModel
            {
                Id = patient.ID,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                Email = patient?.AppUser?.Email ?? "Not Available",
                PhoneNumber = patient?.AppUser?.PhoneNumber ?? "Not Available",
                Governorate = patient?.Governorate ?? Governorate.All,
                BirthDate = DateOnly.FromDateTime(patient?.BirthDate ?? new DateTime()),
                Appointments = patient?.Appointments?.Select(MapToAppointmentViewModel).ToList()
            };
        }
        public async Task<Patient> MapToPatient(PatientViewModel patientViewModel)
        {
            if (patientViewModel.Id != 0)
            {
                var patient = await patientManager.GetPatientInfo(patientViewModel.Id);
                patient.AppUser.PhoneNumber = patientViewModel.PhoneNumber;
                patient.Governorate = patientViewModel.Governorate;
                return patient;
            }
            return new Patient
            {
                FirstName = patientViewModel.FirstName,
                LastName = patientViewModel.LastName,
                BirthDate = patientViewModel.BirthDate.ToDateTime(new TimeOnly(0, 0)),
                Governorate = patientViewModel.Governorate,
                Location = patientViewModel.Governorate.ToString()
            };
        }
        public Appointment MapToAppointment(AppointmentViewModel appointmentViewModel, int patientId, int doctorReservationId)
        {
            Appointment appointment = new()
            {
                PatientId = patientId,
                DoctorReservationID = doctorReservationId,
                AppointmentDate = appointmentViewModel.StartTime
            };
            if (appointmentViewModel.Id != 0)
            {
                appointment.ID = appointmentViewModel.Id;
            }
            return appointment;
        }
        public Patient MapToPatient(RegisterViewModel registerViewModel)
        {
            return new Patient
            {
                FirstName = registerViewModel.FirstName,
                LastName = registerViewModel.LastName,
                BirthDate = registerViewModel.BirthDate.ToDateTime(new TimeOnly(0, 0)),
                Governorate = registerViewModel.Governorate,
                Location = registerViewModel.Governorate.ToString()
            };
        }
    }
}