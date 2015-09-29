using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreenHouse.Models
{
    public class ReservationAuditoriumDay
    {
        public string auditorium { get; set; }
        public string date { get; set; }
        public bool period { get; set; } //true - for a day, false for a week
    }
}