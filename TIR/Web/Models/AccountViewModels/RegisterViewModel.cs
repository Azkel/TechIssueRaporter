using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "UserName")]
        [Required, MinLength(3)]
        public string UserName { get; set; }


        [NotMapped]
        [Required]
        public string Role { get; set; }

        //[NotMapped]
        public static List<SelectListItem> Roles
        {
            get
            {
                return new List<SelectListItem>
        {
            new SelectListItem { Text = "Techniczny", Value = "Techniczny" },
            new SelectListItem { Text = "Sala", Value = "Sala"} };
            }
        }
    }
}
