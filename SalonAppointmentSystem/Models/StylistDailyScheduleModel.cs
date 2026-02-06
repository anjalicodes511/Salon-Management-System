using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalonAppointmentSystem.Models
{
    public class StylistDailyScheduleModel
    {
        public int StylistDailyScheduleId { get; set; }
        public int StylistId { get; set; }

        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public int SlotDuration { get; set; }

        public TimeSpan? BreakStart { get; set; }
        public TimeSpan? BreakEnd { get; set; }

        public bool IsActive { get; set; } = true;
    }
}