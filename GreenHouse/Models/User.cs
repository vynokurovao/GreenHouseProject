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
    
    public partial class User
    {
        public User()
        {
            this.Reservation = new HashSet<Reservation>();
        }
    
        public int UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
        public int Role { get; set; }
    
        public virtual ICollection<Reservation> Reservation { get; set; }
        public virtual Role Role1 { get; set; }
    }
}