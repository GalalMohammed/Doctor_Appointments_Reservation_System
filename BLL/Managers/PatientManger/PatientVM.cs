using MVC.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vezeetaApplicationAPI.Models;

namespace BLLServices.Managers.PatientManger
{
    public class PatientVM
    {
        public int ID { get; set; }
        public int AppUserID { get; set; }


        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Gender Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public required string Location { get; set; }
        public float? Lat { get; set; }
        public float? Lng { get; set; }
    }
}
