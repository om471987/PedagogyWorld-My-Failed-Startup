using PedagogyWorld.CustomAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PedagogyWorld.Models
{
    public class RegisterModel
    {
        public RegisterModel()
        {
            State = 1;
            District = 1;
            School = 1;
        }

        [Required]
        [Remote("DoesUserNameExist", "Account", HttpMethod = "POST", ErrorMessage = "User name already exists. Please enter a different one.")]
        [Display(Name = "User Name")]
        [RegularExpression("^[a-zA-Z0-9_-]{3,15}$", ErrorMessage = "Name must be alphabets, numbers, hiphen and underscore.")]
        [StringLength(12, ErrorMessage = "The {0} must be in the range of {2} and {1} characters.", MinimumLength = 3)]
        public string UserName { get; set; }

        [Display(Name = "First Name")]
        [RegularExpression("^[a-zA-Z]{1,20}$", ErrorMessage = "First Name must contain only alphabets.")]
        [StringLength(20, ErrorMessage = "The {0} must be in the range of {2} and {1} characters.", MinimumLength = 1)]
        public string First { get; set; }

        [Display(Name = "Last Name")]
        [RegularExpression("^[a-zA-Z]{1,20}$", ErrorMessage = "Last Name must contain only alphabets.")]
        [StringLength(20, ErrorMessage = "The {0} must be in the range of {2} and {1} characters.", MinimumLength = 1)]
        public string Last { get; set; }

        [Required]
        [StringLength(15, ErrorMessage = "The {0} must be in the range of {2} and {1} characters.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [System.Web.Mvc.Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [EmailAddress]
        [Remote("DoesEmailExist", "Account", HttpMethod = "POST", ErrorMessage = "Email Address already exists. Please enter a different one.")]
        public string Email { get; set; }

        public int State { get; set; }
        public int District { get; set; }
        public int School { get; set; }

        public IEnumerable<State> States { get; set; }
        public IEnumerable<District> Districts { get; set; }
        public IEnumerable<School> Schools { get; set; }

        public IEnumerable<SelectListItem> Subjects { get; set; }
        [MustSelect(ErrorMessage = "Must select atleast one subject.")]
        public int[] SubjectIds { get; set; }

        public IEnumerable<SelectListItem> Grades { get; set; }
        [MustSelect(ErrorMessage = "Must select atleast one Grade.")]
        public int[] GradeIds { get; set; }
    }
}