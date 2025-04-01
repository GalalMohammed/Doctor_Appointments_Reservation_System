using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vezeetaApplicationAPI.Models;

namespace BLLServices.Managers.DoctorManger
{
    public class DoctorVM
    {
        public int ID { get; set; }
        public int SpecialtyID { get; set; }
        public decimal Fees { get; set; }
        public int WaitingTime { get; set; }
        public float OverallRating { get; set; }
        public string ImageURL { get; set; }
        public string About { get; set; }
        public WorkingDays WorkingDays { get; set; }
        public DateTime DefaultStartTime { get; set; }
        public DateTime DefaultEndTime { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string Location { get; set; }
        public float? Lat { get; set; }
        public float? Lng { get; set; }
        public int AppUserID { get; set; }
    }
}
