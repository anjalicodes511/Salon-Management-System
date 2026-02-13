using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalonAppointmentSystem.Models.ViewModels
{
    public class CustomerDetailsVM
    {
        public int CustomerId { get; set; }
        public string FullName {  get; set; }
        public string Phone {  get; set; }
        public string Email { get; set; }
        public string Gender {  get; set; }
        public bool IsActive { get; set; }

        public int TotalAppointments {  get; set; }
    }
}