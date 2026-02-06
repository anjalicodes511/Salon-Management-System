using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalonAppointmentSystem.Models
{
    public class StylistServiceModel
    {
        public int StylistServiceId {  get; set; }
        public int StylistId {  get; set; }
        public int ServiceId {  get; set; }
        public bool IsActive { get; set; } = true;
    }
}