using GreenHouse.Models;

namespace GreenHouse.ContexManager
{
    public class TD : Reservation
    {
        public int Hour { get; set; }

        public override Auditorium Auditorium { get; set; }
        public override User User { get; set; }
    }
}