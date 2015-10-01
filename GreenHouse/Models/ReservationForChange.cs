using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreenHouse.Models
{
    public class ReservationForChange
    {
        public string reservation { get; set; }
        public string purpose { get; set; }
        public int id { get; set; }
        public byte view { get; set; } // 0 - for all auditoiums, 1 - for 1 auditorium, 2 - for a week
    }
}