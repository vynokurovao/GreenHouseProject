using GreenHouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreenHouse.ContexManager
{
    public class ReservationManager
    {
        static Entities db;

        private List<List<Reservation>> table;

        public List<List<Reservation>> Table
        {
            get
            {
                return table;
            }
            set
            {
                table = value;
            }
        }

        public ReservationManager(DateTime date)
        {
            db = new Entities();

            Table = GetDateReservation(date);
        }

        private Reservation GetAuditoriumReservation(int AuditoriumId, DateTime date)
        {
            Reservation reservation = null;

            foreach (Reservation reserv in db.Reservation)
            {
                if (reserv.TargetAuditorium == AuditoriumId && reserv.StartDate == date)
                {
                    reservation = reserv;

                    return reservation;
                }
            }

            return reservation;
        }

        public List<List<Reservation>> GetDateReservation(DateTime date)
        {
            List<List<Reservation>> table = new List<List<Reservation>>();

            for (int i = 9; i <= 21; i++)
            {
                List<Reservation> row = new List<Reservation>();

                foreach (Auditorium auditory in db.Auditorium)
                {
                    Reservation reserv = GetAuditoriumReservation(auditory.AuditoriumId, new DateTime(date.Year, date.Month, date.Day, i, 0, 0));

                    row.Add(reserv);
                }

                table.Add(row);
            }

            return table;
        }
    }
}