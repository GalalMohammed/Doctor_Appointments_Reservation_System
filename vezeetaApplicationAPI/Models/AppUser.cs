using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vezeetaApplicationAPI.Models
{
    public enum Gender
    {
        Male=1,
        Female
    }
    public class AppUser : IdentityUser
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
    }
}
