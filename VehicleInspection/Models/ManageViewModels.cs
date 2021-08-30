using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace VehicleInspection.Models
{
    public class IndexViewModel
    {
        public bool HasPassword { get; set; }
        public IList<UserLoginInfo> Logins { get; set; }
        public string PhoneNumber { get; set; }
        public bool TwoFactor { get; set; }
        public bool BrowserRemembered { get; set; }
        public string Email { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current PIN")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(4, ErrorMessage = "The {0} must be exactly {2} characters long.", MinimumLength = 4)]
        [DataType(DataType.Password)]
        [Display(Name = "New PIN")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new PIN")]
        [Compare("NewPassword", ErrorMessage = "The new PIN and confirmation PIN do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class ChangeEmailViewModel
    {
        [Display(Name = "Current Email")]
        public string OldEmail { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "New Email")]
        public string NewEmail { get; set; }
    }

    public class ChangeSubscriptionsViewModel
    {
        [Display(Name ="Issue Alerts")]
        public bool IsSubscribedIssues { get; set; }
    }

}