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

        public Entities db = new Entities();

        public ActionResult Index()
        {
            //для инициализации бд
            //DBInitialization init = new DBInitialization();
            //init.Initialization(db);

            ReservationManager reservManager = new ReservationManager(DateTime.Now);

            ViewBag.Auditoriums = db.Auditorium;

            return View(reservManager);
        }

        [HttpGet]
        public ActionResult Table()
        {
            ReservationManager reservManager = new ReservationManager(DateTime.Now);

            ViewBag.Auditoriums = db.Auditorium;

            return View(reservManager);
        }

        [HttpPost]
        public ActionResult TableForDate(string date)
        {
            string[] parts = date.Split('.');

            int year = int.Parse(parts[2]), month = int.Parse(parts[1]), day = int.Parse(parts[0]);

            ReservationManager reservManager = new ReservationManager(new DateTime(year, month, day, 0, 0, 0));

            ViewBag.Auditoriums = db.Auditorium;

            return PartialView("Table", reservManager);
        }

        [HttpPost]
        public ActionResult RemoveReservation(string id)
        {
            int rId = int.Parse(id);

            Reservation reserv = db.Reservation.Where(r => r.ReservationId.Equals(rId)).First();

            db.RemoveReservation(reserv);

            ReservationManager reservManager = new ReservationManager(reserv.StartDate);

            ViewBag.Auditoriums = db.Auditorium;

            return PartialView("Table", reservManager);
        }
    }
}