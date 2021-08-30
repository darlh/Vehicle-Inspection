using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VehicleInspection.Models
{
    public class LoginViewModel
    {
        //[Required]
        //[Display(Name = "Email")]
        //[EmailAddress]
        //public string Email { get; set; }

        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "PIN")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Full Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [StringLength(4, ErrorMessage = "The {0} must be exactly {2} characters long.", MinimumLength = 4)]
        [DataType(DataType.Password)]
        [Display(Name = "PIN")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm PIN")]
        [Compare("Password", ErrorMessage = "The PIN and confirmation PIN do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Manager?")]
        public bool IsManager { get; set; }
    }


}