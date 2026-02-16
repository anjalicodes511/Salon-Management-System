using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalonAppointmentSystem.Models.ViewModels
{
    public class MonthlyRevenueVM
    {
        public string Month { get; set; }
        public decimal TotalRevenue { get; set; }
    }
}