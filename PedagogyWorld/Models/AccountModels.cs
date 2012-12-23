using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;
using PedagogyWorld.Domain;

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
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
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
        [Required, MaxLength(255), MinLength(1)]
        public string UserName { get; set; }

        [Required, MaxLength(256), MinLength(3), RegularExpression(@"^([\w.-]+)@([\w-]+)((.(\w){2,3})+)$")]
        [Display(Name = "Email address")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        public States State { get; set; }

        [Required, MaxLength(255), MinLength(1)]
        public string District { get; set; }

        //[Required, MinLength(1), MaxLength(255)]
        //[Display(Name = "First Name")]
        //public string FirstName { get; set; }

        //[Required, MinLength(1), MaxLength(255)]
        //[Display(Name = "Last Name")]
        //public string LastName { get; set; }

        //[Display(Name = "Grades Teaching")]
        //public Grades GradesTeaching { get; set; }

        //[DataType(DataType.Text)]
        //public string School { get; set; }

        //[Display(Name = "Subjects Teaching")]
        //public Subjects SubjectsTeaching { get; set; }
    }
}
