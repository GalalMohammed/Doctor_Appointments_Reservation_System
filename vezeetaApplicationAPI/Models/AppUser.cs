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
        Male,
        Female
    }
    public class AppUser : IdentityUser
    {
        [Required(ErrorMessage = "First Name is required")]
        [StringLength(50, ErrorMessage = "First Name must be less than 50 characters")]
        [Display(Name = "First Name")]
        [DataType(DataType.Text)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "First Name must contain only letters")]
        public required string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is required")]
        [StringLength(50, ErrorMessage = "Last Name must be less than 50 characters")]
        [Display(Name = "Last Name")]
        [DataType(DataType.Text)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Last Name must contain only letters")]
        public required string LastName { get; set; }
        [Required(ErrorMessage = "Gender is required")]
        public Gender Gender { get; set; }
        [Required(ErrorMessage = "Birth Date is required")]
        [DataType(DataType.Date)]
        [Display(Name = "Birth Date")]
        public DateTime BirthDate { get; set; }
    }
}
