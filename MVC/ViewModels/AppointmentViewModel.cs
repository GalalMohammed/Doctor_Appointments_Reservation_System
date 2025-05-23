﻿using MVC.Enums;

namespace MVC.ViewModels
{
    public class AppointmentViewModel
    {
        public int Id { get; set; }
        public string Specialty { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Governorate Governorate { get; set; }
        public string Location { get; set; }
        public string Doctor { get; set; }
        public string DoctorImagePath { get; set; }
        public bool IsExists { get; set; }
    }
}
