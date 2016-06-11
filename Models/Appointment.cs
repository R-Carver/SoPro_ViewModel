using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public DateTime MyAppointment { get; set; }

        public virtual Contract Contract { get; set; }
    }
}