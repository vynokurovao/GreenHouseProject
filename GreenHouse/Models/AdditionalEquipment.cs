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
    
    public partial class AdditionalEquipment
    {
        public AdditionalEquipment()
        {
            this.Auditorium = new HashSet<Auditorium>();
        }
    
        public int AdditionalEquipmentId { get; set; }
        public string AdditionalEquipmentName { get; set; }
    
        public virtual ICollection<Auditorium> Auditorium { get; set; }
    }
}
