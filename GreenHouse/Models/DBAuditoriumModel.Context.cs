﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GreenHouse.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class Entities : DbContext
    {
        public Entities()
            : base("name=Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AdditionalEquipment> AdditionalEquipment { get; set; }
        public virtual DbSet<Auditorium> Auditorium { get; set; }
        public virtual DbSet<Reservation> Reservation { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<User> User { get; set; }
    
        public virtual int DeleteAuditoriumEquipment(Nullable<int> auditoriumId, Nullable<int> additionalEquipmentId)
        {
            var auditoriumIdParameter = auditoriumId.HasValue ?
                new ObjectParameter("auditoriumId", auditoriumId) :
                new ObjectParameter("auditoriumId", typeof(int));
    
            var additionalEquipmentIdParameter = additionalEquipmentId.HasValue ?
                new ObjectParameter("additionalEquipmentId", additionalEquipmentId) :
                new ObjectParameter("additionalEquipmentId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("DeleteAuditoriumEquipment", auditoriumIdParameter, additionalEquipmentIdParameter);
        }
    
        public virtual int InsertAudEq(Nullable<int> audId, Nullable<int> addEqId)
        {
            var audIdParameter = audId.HasValue ?
                new ObjectParameter("audId", audId) :
                new ObjectParameter("audId", typeof(int));
    
            var addEqIdParameter = addEqId.HasValue ?
                new ObjectParameter("addEqId", addEqId) :
                new ObjectParameter("addEqId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("InsertAudEq", audIdParameter, addEqIdParameter);
        }

        public bool AddReservation(Reservation reservation)
        {
            try
            {
                Reservation.Add(reservation);

                SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool RemoveReservation(Reservation reservation)
        {
            try
            {
                Reservation.Remove(reservation);

                SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
