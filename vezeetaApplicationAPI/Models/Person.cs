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

        public int AppUserID { get; set; }
        public AppUser? AppUser { get; set; }
        [Required(ErrorMessage = "Location is required")]
        [DataType(DataType.Text)]
        public required string Location { get; set; }
        public float? Lat { get; set; }
        public float? Lng { get; set; }
    }
}
