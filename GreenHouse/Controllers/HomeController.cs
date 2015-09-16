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
        private DateTime date = new DateTime(0);

        public Entities db = new Entities();

        public ActionResult Index()
        {
            //для инициализации бд
            //DBInitialization init = new DBInitialization();
            //init.Initialization(db);

            ReservationManager reservManager = new ReservationManager(DateTime.Now);

            if (date.Ticks == 0)
            {
                reservManager = new ReservationManager(new DateTime(2015, 9, 9, 0, 0, 0));
            }
            else
            {
                reservManager = new ReservationManager(new DateTime(date.Year, date.Month, date.Day, 0, 0, 0));
            }

            ViewBag.Auditoriums = db.Auditorium;

            return View(reservManager);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult RemoveReservation(string id)
        {
            int rId = int.Parse(id);
            Reservation reserv = db.Reservation.Where(r => r.ReservationId.Equals(rId)).First();

            date = reserv.StartDate;
            
            db.RemoveReservation(reserv);

            return Redirect("Home/Index");
        }
    }
}