using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmartService.ViewModels
{
    public class ContactUsViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
        [Display(Name = "Name/company")]
        public string Username { get; set; }

        [Phone]
        public string Phone { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Your message")]
        [StringLength(300, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 10)]
        public string Message { get; set; }
    }
}
