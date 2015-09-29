using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GreenHouse.Models
{
    public class NewRoomModel
    {
        public string Name { get; set; }

        [Required]
        public short Capacity { get; set; }

        public bool Wifi { get; set; }

        public bool Projector { get; set; }

        public bool Monitor { get; set; }

        public bool Microphone { get; set; }
    }
}