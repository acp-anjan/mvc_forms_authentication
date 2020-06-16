using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mvc_forms_authentication.ViewModels
{
    public class UserViewModel
    {
        [Display(Name = "User Name")]
        [Required(ErrorMessage = "User Name is requried.")]
        [Remote("UserNameExists", "Account", ErrorMessage = "Username already exists.")]
        public string UserName { get; set; }

        [Display(Name = "User Email")]
        [Required(ErrorMessage = "User Email is requried.")]
        [Remote("EmailExists", "Account", ErrorMessage = "User Email already exists.")]
        [EmailAddress(ErrorMessage = "Enter valid email address.")]
        public string UserEmail { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password is requried.")]
        [DataType(DataType.Password)]
        public string UserPassword { get; set; }

        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "Confirm Password is requried.")]
        [System.ComponentModel.DataAnnotations.Compare("UserPassword", ErrorMessage = "Password & Confirm Password should be same.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First Name is requried.")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last Name is requried.")]
        public string LastName { get; set; }
    }
}