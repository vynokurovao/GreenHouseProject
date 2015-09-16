using GreenHouse.ContexManager;

namespace GreenHouse.Models
{
    using System.Data.Entity;

    public partial class Entities : DbContext
    {
        public Entities()
            : base("name=Entities")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            DBInitialization init = new DBInitialization();
            init.Initialization(this);
        }

        public DbSet<AdditionalEquipment> AdditionalEquipment { get; set; }
        public DbSet<Auditorium> Auditorium { get; set; }
        public DbSet<Reservation> Reservation { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<User> User { get; set; }

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
