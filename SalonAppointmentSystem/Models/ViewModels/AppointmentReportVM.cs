using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalonAppointmentSystem.Models.ViewModels
{
    public class AppointmentReportVM
    {
        public DateTime AppointmentDate { get; set; }
        public string CustomerName { get; set; }
        public string ServiceName { get; set; }
        public int Status { get; set; }
        public string StylistName { get; set; }
    }
}