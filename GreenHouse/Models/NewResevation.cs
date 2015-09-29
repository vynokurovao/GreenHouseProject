using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreenHouse.Models
{
    public class NewResevation
    {
        public int auditorium { get; set; }
        public string auditorium_name { get; set; }
        public string purpose { get; set; }
        public int year { get; set; }
        public int month { get; set; }
        public int day { get; set; }
        public int hour { get; set; }
        public int finish_year { get; set; }
        public int finish_month { get; set; }
        public int finish_day { get; set; }
        public int finish_hour { get; set; }
        public bool type { get; set; }
        public byte view { get; set; } // 0 - for all auditoiums, 1 - for 1 auditorium, 2 - for a week
    }
}