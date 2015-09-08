namespace GreenHouse.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

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

        public DbSet<AdditionalEquipment> AdditionalEquipment { get; set; }
        public DbSet<Auditorium> Auditorium { get; set; }
        public DbSet<Reservation> Reservation { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<User> User { get; set; }
    }
}
