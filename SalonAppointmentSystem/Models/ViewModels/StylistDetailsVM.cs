using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalonAppointmentSystem.Models.ViewModels
{
    public class StylistDetailsVM
    {
        public int StylistId {  get; set; }
        public string StylistName {  get; set; }
        public TimeSpan StartTime {  get; set; }
        public TimeSpan EndTime { get; set; }
        public List<StylistServiceVM> Services { get; set; } = new List<StylistServiceVM>();
    }
}