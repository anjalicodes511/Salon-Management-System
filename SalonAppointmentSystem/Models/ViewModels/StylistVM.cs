using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SalonAppointmentSystem.Models.ViewModels
{
    public class StylistVM
    {
        public int StylistId { get; set; }

        [DisplayName("Stylist Name")]
        public string StylistName { get; set; }

        [DisplayName("Start Time")]
        public TimeSpan StartTime { get; set; }

        [DisplayName("End Time")]
        public TimeSpan EndTime { get; set; }

        [DisplayName("Status")]
        public bool IsActive { get; set; } = true;
    }
}