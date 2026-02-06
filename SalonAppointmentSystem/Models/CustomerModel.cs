using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SalonAppointmentSystem.Models
{
    public class CustomerModel
    {
        public int CustomerId {  get; set; }

        public string Role { get; set; } = "User";

        [Required(ErrorMessage ="Enter Your Name")]
        [DisplayName("Name")]
        public string FullName {  get; set; }

        [Required(ErrorMessage = "Enter Valid Phone Number ")]
        [MaxLength(10)]
        [MinLength(10)]
        [DisplayName("Phone No.")]
        public string Phone {  get; set; }

        [Required(ErrorMessage = "Enter Valid Email")]
        [DisplayName("Email")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter Valid Password")]
        [RegularExpression(
    @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&#])[A-Za-z\d@$!%*?&#]{8,}$",
    ErrorMessage = "Password must be at least 8 characters and include uppercase, lowercase, number, and special character."
)]
        [DisplayName("Password")]
        [DataType(DataType.Password)]
        public string Password {  get; set; }

        [Required(ErrorMessage = "Confirm Password is a required field!")]
        [DisplayName("Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password is not identical!")]
        public string ConfirmPassword {  get; set; }

        [Required(ErrorMessage = "Enter Valid Gender")]
        [DisplayName("Gender")]
        public string Gender {  get; set; }
        public bool IsActive { get; set; } = true;
    }
}