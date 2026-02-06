using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalonAppointmentSystem.Models.ViewModels
{
    public class StylistServiceVM
    {
        public int StylistId { get; set; }
        public int ServiceId { get; set; }

        public string ServiceName { get; set; }
        public bool IsAssigned { get; set; }
    }
}