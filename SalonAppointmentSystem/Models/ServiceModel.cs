using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SalonAppointmentSystem.Models
{
    public class ServiceModel
    {
        public int ServiceId {  get; set; }
        [Required(ErrorMessage = "Enter Service Name")]
        [DisplayName("Service Name")]
        public string ServiceName { get; set; }

        [Required(ErrorMessage = "Enter Service Price")]
        [DataType(DataType.Currency)]
        [DisplayName("Price")]
        [Range(1,15000)]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Enter Service Duration")]
        [DisplayName("Duration in minutes")]
        [Range(1, 300)]
        public int ServiceDuration {  get; set; }

        [DisplayName("Available?")]
        public bool IsActive { get; set; } = true;
    }
}