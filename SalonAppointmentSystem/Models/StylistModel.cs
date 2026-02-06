using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SalonAppointmentSystem.Models
{
    public class StylistModel
    {
        public int StylistId {  get; set; }

        [Required(ErrorMessage = "Enter Sylist Name")]
        [DisplayName("Stylist Name")]
        public string StylistName {  get; set; }

        [Required(ErrorMessage = "Enter Valid Start Time")]
        [DisplayName("Start Time")]
        public TimeSpan StartTime {  get; set; }

        [Required(ErrorMessage = "Enter Valid End Time")]
        [DisplayName("End Time")]
        public TimeSpan EndTime { get; set; }

        [DisplayName("Available?")]
        public bool IsActive { get; set; } = true;
    }
}