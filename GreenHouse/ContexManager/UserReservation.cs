using GreenHouse.Models;

using System.Collections.Generic;

namespace GreenHouse.ContexManager
{
    public class UserReservation
    {
        public List<Reservation> UserReservations;

        public UserReservation(User user)
        {
            UserReservations = new List<Reservation>();

            foreach (Reservation reserv in user.Reservation)
            {
                UserReservations.Add(reserv);
            }
        }

        public UserReservation()
        {
            UserReservations = new List<Reservation>();
        }
    }
}