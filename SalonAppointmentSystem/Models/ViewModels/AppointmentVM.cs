using SalonAppointmentSystem.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalonAppointmentSystem.Models.ViewModels
{
    public class AppointmentVM
    {
        public int AppointmentId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string StylistName {  get; set; }
        public string ServiceName {  get; set; }

        public TimeSpan StartTime {  get; set; }
        public TimeSpan EndTime { get; set; }

        public AppointmentStatus Status { get; set; }
    }
}