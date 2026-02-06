using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalonAppointmentSystem.Models
{
    public class StylistTimeSlotModel
    {
        public int TimeSlotId {  get; set; }
        public int StylistId {  get; set; }
        public DateTime SlotDate { get; set; }
        public TimeSpan StartTime {  get; set; }
        public TimeSpan EndTime { get; set; }
        public bool IsBooked {  get; set; }
    }
}