using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vezeetaApplicationAPI.Models;

namespace DAL.Models
{
    public abstract class Person
    {
        [StringLength(50, ErrorMessage = "First Name must be less than 50 characters")]
        [DataType(DataType.Text)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "First Name must contain only letters")]
        public required string FirstName { get; set; }

        [StringLength(50, ErrorMessage = "Last Name must be less than 50 characters")]
        [DataType(DataType.Text)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Last Name must contain only letters")]
        public required string LastName { get; set; }

        public Gender Gender { get; set; }

        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        public int AppUserID { get; set; }
        public virtual AppUser? AppUser { get; set; }
        
        [DataType(DataType.Text)]
        public required string Location { get; set; }
        
        public float? Lat { get; set; }
        
        public float? Lng { get; set; }
    }
}
