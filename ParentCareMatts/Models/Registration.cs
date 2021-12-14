﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ParentCareMatts.Models
{
    public class Registration
    {
        public int Id { get; set; }
       // [Required (ErrorMessage = "FirstName is required")]
        public string FirstName { get; set; }
       // [Required(ErrorMessage = "LastName is required")]
        public string LastName { get; set; }
       // [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }
       // [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
       // [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
       // [Required(ErrorMessage = "Your latter password must be the same with the confirm password")]
        public string ConfirmPassword { get; set; }
        public ICollection<Login> Logins { get; set; }
    }
}
