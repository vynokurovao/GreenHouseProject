using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GreenHouse.Models;
using GreenHouse.ContexManager;

namespace GreenHouse.Controllers
{
    public class HomeController : Controller
    {

        public Reservation GetAuditoriumReservation(int AuditoriumId, int year, int month, int day, int hour, Entities e)
        {
            Reservation reservation = null;

            foreach (Reservation reserv in e.Reservation)
            {
                if (reserv.TargetAuditorium == AuditoriumId && reserv.StartDate == new DateTime(year, month, day, hour, 0, 0))
                {
                    reservation = reserv;
                }
            }

            return reservation;
        }


        public ActionResult Index()
        {
            Entities db = new Entities();

            //DBInitialization init = new DBInitialization();
            //init.Initialization(db);

            #region ToTables
            List<Reservation> ToTable9 = new List<Reservation>();
            List<Reservation> ToTable10 = new List<Reservation>();
            List<Reservation> ToTable11 = new List<Reservation>();
            List<Reservation> ToTable12 = new List<Reservation>();
            List<Reservation> ToTable13 = new List<Reservation>();
            List<Reservation> ToTable14 = new List<Reservation>();

            
            foreach (Auditorium auditory in db.Auditorium)
            {
                Reservation reserv = GetAuditoriumReservation(auditory.AuditoriumId, 2015, 9, 9, 9, db);
                ToTable9.Add(reserv);

                reserv = GetAuditoriumReservation(auditory.AuditoriumId, 2015, 9, 9, 10, db);
                ToTable10.Add(reserv);

                reserv = GetAuditoriumReservation(auditory.AuditoriumId, 2015, 9, 9, 11, db);
                ToTable11.Add(reserv);

                reserv = GetAuditoriumReservation(auditory.AuditoriumId, 2015, 9, 9, 12, db);
                ToTable12.Add(reserv);

                reserv = GetAuditoriumReservation(auditory.AuditoriumId, 2015, 9, 9, 13, db);
                ToTable13.Add(reserv);

                reserv = GetAuditoriumReservation(auditory.AuditoriumId, 2015, 9, 9, 14, db);
                ToTable14.Add(reserv);
            }
            #endregion

            #region PutToTablesIntoViewBag
            ViewBag.ToTable9 = ToTable9;
            ViewBag.ToTable10 = ToTable10;
            ViewBag.ToTable11 = ToTable11;
            ViewBag.ToTable12 = ToTable12;
            ViewBag.ToTable13 = ToTable13;
            ViewBag.ToTable14 = ToTable14;
            #endregion

            ViewBag.Auditoriums_Count = db.Auditorium.Count();
            ViewBag.Auditoriums = db.Auditorium;

            return View();
        }
    }
}