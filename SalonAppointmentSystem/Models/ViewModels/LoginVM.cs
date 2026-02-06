using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SalonAppointmentSystem.Models.ViewModels
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Enter Valid Email")]
        [DisplayName("Email")]
        public string Email {  get; set; }

        [Required(ErrorMessage = "Enter Valid Password")]
        [DisplayName("Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}