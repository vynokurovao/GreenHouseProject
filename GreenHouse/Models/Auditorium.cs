//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GreenHouse.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Auditorium
    {
        public Auditorium()
        {
            this.Reservation = new HashSet<Reservation>();
            this.AdditionalEquipment = new HashSet<AdditionalEquipment>();
        }
    
        public int AuditoriumId { get; set; }
        public string AuditoriumName { get; set; }
        public short Capacity { get; set; }
    
        public virtual ICollection<Reservation> Reservation { get; set; }
        public virtual ICollection<AdditionalEquipment> AdditionalEquipment { get; set; }
    }
}