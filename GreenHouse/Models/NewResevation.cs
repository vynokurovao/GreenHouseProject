using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreenHouse.Models
{
    public class NewResevation
    {
        public int auditorium { get; set; }
        public string purpose { get; set; }
        public int year { get; set; }
        public int month { get; set; }
        public int day { get; set; }
        public int hour { get; set; }
    }
}