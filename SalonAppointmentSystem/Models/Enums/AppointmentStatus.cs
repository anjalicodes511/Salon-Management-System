using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalonAppointmentSystem.Models.Enums
{
    public enum AppointmentStatus
    {
        Booked=1,
        Cancelled=2,
        Completed=3,
        NoShow=4
    }
}