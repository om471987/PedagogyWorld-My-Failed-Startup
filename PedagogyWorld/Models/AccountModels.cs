using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace PedagogyWorld.Models
{
    public class LocalPasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [System.Web.Mvc.Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterModel
    {
        [Required]
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

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [System.Web.Mvc.Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string State { get; set; }

        public string District { get; set; }

        public string School { get; set; }

        public IEnumerable<State> States { get; set; }

        public IEnumerable<SelectListItem> Subjects { get; set; }

        public int[] SubjectIds { get; set; }

        public IEnumerable<SelectListItem> Grades { get; set; }

        public int[] GradeIds { get; set; }
    }
}
