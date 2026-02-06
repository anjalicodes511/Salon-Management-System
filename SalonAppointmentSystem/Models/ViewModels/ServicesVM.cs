using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalonAppointmentSystem.Models.ViewModels
{
    public class ServicesVM
    {
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public int ServiceDuration { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
    }
}