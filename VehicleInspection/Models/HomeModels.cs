using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VehicleInspection.Models
{
    public class HomeModels
    {
    }

    public class ContactViewModel
    {
        [Required]
        [Display(Name ="Your Name")]
        public string FromName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name ="Your email")]
        public string FromEmail { get; set; }

        [Required]
        public string EmailMessage { get; set; }
    }
}