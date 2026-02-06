using SalonAppointmentSystem.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalonAppointmentSystem.Models
{
    public class AppointmentModel
    {
        public int AppointmentId {  get; set; }
        public DateTime AppointmentDate {  get; set; }
        public int CustomerId {  get; set; }
        public int StylistId {  get; set; }
        public int ServiceId {  get; set; }
        public int StylistTimeSlotId {  get; set; }

        public AppointmentStatus Status { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}