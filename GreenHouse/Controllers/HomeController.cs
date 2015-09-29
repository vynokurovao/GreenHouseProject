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

            if (Session["IsAuthenticated"] == null)
            {
                Session["IsAuthenticated"] = false;
            }

            ReservationManager reservManager = new ReservationManager(DateTime.Now);

            ViewBag.Auditoriums = db.Auditorium;

            ViewBag.id = "td";

            return View(reservManager);
        }

        [HttpGet]
        public ActionResult IndexRoomDate(ReservationAuditoriumDay model)
        {
            string[] stringDate = model.date.Split(' ');

            string[] parts = stringDate[1].Split('.');

            int year = int.Parse(parts[2]), month = int.Parse(parts[1]), day = int.Parse(parts[0]);

            DateTime date = new DateTime(year, month, day, 0, 0, 0);

            return RedirectToAction("Index", "Room", new {date = date, room = model.auditorium });
        }

        [HttpGet]
        public ActionResult Table()
        {
            ReservationManager reservManager = new ReservationManager(DateTime.Now);

            ViewBag.Auditoriums = db.Auditorium;

            ViewBag.id = "td";

            return View(reservManager);
        }

        [HttpPost]
        public ActionResult TableForDate(string date)
        {
            string[] parts = date.Split('.');

            int year = int.Parse(parts[2]), month = int.Parse(parts[1]), day = int.Parse(parts[0]);

            ReservationManager reservManager = new ReservationManager(new DateTime(year, month, day, 0, 0, 0));

            ViewBag.Auditoriums = db.Auditorium;

            ViewBag.id = "td";

            return PartialView("Table", reservManager);
        }

        [HttpPost]
        public ActionResult RemoveReservation(ReservationForRemove reservForRemote)
        {
            int rId = int.Parse(reservForRemote.reservation);

            IQueryable<Reservation> reservations = db.Reservation.Where(r => r.ReservationId.Equals(rId));

            Reservation reserv = new Reservation();
            
            foreach (Reservation r in reservations)
            {
                reserv = r;
            }

            db.RemoveReservation(reserv);

            ReservationManager reservManager = new ReservationManager(reserv.StartDate);

            if (reservForRemote.view == 0)
            {
                ViewBag.id = "td";

                ViewBag.Auditoriums = db.Auditorium;
            }
            else if (reservForRemote.view == 1)
            {
                IQueryable<Auditorium> auditorium = db.Auditorium.Where(auditor => auditor.AuditoriumId.Equals(reserv.TargetAuditorium));

                foreach (Auditorium a in auditorium)
                {
                    reservManager.Table = new List<List<TD>>();

                    reservManager.Table = reservManager.GetDayRoomReservation(reserv.StartDate, a.AuditoriumName);

                    ViewBag.id = "td-day";

                    ViewBag.Room = a.AuditoriumName;

                    ViewBag.Date = reserv.StartDate;

                    ViewBag.Auditoriums = db.Auditorium.Where(auditor => auditor.AuditoriumName.Equals(a.AuditoriumName));
                }

            }
            else if (reservForRemote.view == 2)
            {
                IQueryable<Auditorium> auditorium = db.Auditorium.Where(auditor => auditor.AuditoriumId.Equals(reserv.TargetAuditorium));

                foreach (Auditorium a in auditorium)
                {
                    reservManager.Table = new List<List<TD>>();

                    reservManager.Table = reservManager.GetWeekReservation(reserv.StartDate, a.AuditoriumName);

                    ViewBag.id = "td";

                    ViewBag.week = reservManager.GetDays(reserv.StartDate);
                }
            }

            return PartialView("Table", reservManager);
        }

        [HttpPost]
        public ActionResult RoomDate(ReservationAuditoriumDay model)
        {
            string[] parse = model.date.Split(' ');

            string[] parts = parse[parse.Length - 1].Split('.');

            int year = int.Parse(parts[2]), month = int.Parse(parts[1]), day = int.Parse(parts[0]);

            DateTime date = new DateTime(year, month, day, 0, 0, 0);

            ReservationManager reservManager = new ReservationManager(date);

            if (model.auditorium != null)
            {
                reservManager.Table = new List<List<TD>>();

                reservManager.Table = reservManager.GetDayRoomReservation(date, model.auditorium);

                ViewBag.id = "td-day";

                ViewBag.Room = model.auditorium;

                ViewBag.Date = date;
            }
            else
            {
                ViewBag.id = "td";

                ViewBag.Auditoriums = db.Auditorium;
            }

            return PartialView("Table", reservManager);
        }

        [HttpPost]
        public ActionResult RoomWeek(ReservationAuditoriumDay model)
        {
            string[] parse = model.date.Split(' ');

            string[] parts = parse[parse.Length - 1].Split('.');

            int year = int.Parse(parts[2]), month = int.Parse(parts[1]), day = int.Parse(parts[0]);

            DateTime date = new DateTime(year, month, day, 0, 0, 0);

            ReservationManager reservManager = new ReservationManager(date);

            reservManager.Table = new List<List<TD>>();

            reservManager.Table = reservManager.GetWeekReservation(date, model.auditorium);

            ViewBag.id = "td";

            ViewBag.week = reservManager.GetDays(date);
            
            return PartialView("Table", reservManager);
        }

        [HttpPost]
        public ActionResult AddReservation(NewResevation newReservation)
        {
            Reservation reservation = new Reservation();

            string email = Session["UserEmail"].ToString();

            reservation.CreatedBy = db.User.Where(u => u.Email.Equals(email)).First().UserId;

            reservation.StartDate = new DateTime(newReservation.year, newReservation.month, newReservation.day, newReservation.hour, 0, 0);


            if (newReservation.finish_year == 0)
            {
                reservation.FinishDate = new DateTime(newReservation.year, newReservation.month, newReservation.day, newReservation.hour + 1, 0, 0);
            }
            else
            {
                if (newReservation.view == 1)
                {
                    reservation.FinishDate = new DateTime(newReservation.year, newReservation.month, newReservation.day, newReservation.finish_hour, 0, 0);
                }
                else
                {
                    
                }
                
            }

            reservation.Type = newReservation.type;

            reservation.Purpose = newReservation.purpose;

            if (newReservation.auditorium == 0)
            {
                reservation.TargetAuditorium = db.Auditorium.Where(a => a.AuditoriumName.Equals(newReservation.auditorium_name)).First().AuditoriumId;

                newReservation.auditorium = reservation.TargetAuditorium;
            }
            else
            {
                reservation.TargetAuditorium = newReservation.auditorium;
            }
            db.AddReservation(reservation);

            ReservationManager reservManager = new ReservationManager(reservation.StartDate);

            if (newReservation.view == 0)
            {
                ViewBag.id = "td";

                ViewBag.Auditoriums = db.Auditorium;
            }
            else if (newReservation.view == 1)
            {
                IQueryable<Auditorium> auditorium = db.Auditorium.Where(auditor => auditor.AuditoriumId.Equals(newReservation.auditorium));

                foreach (Auditorium a in auditorium)
                {
                    reservManager.Table = new List<List<TD>>();

                    ViewBag.Room = a.AuditoriumName;

                    ViewBag.Date = reservation.StartDate;

                    reservManager.Table = reservManager.GetDayRoomReservation(reservation.StartDate, a.AuditoriumName);

                    ViewBag.Auditoriums = db.Auditorium.Where(auditor => auditor.AuditoriumName.Equals(a.AuditoriumName));

                    ViewBag.id = "td-day";
                }
            }
            else if (newReservation.view == 2)
            {
                IQueryable<Auditorium> auditorium = db.Auditorium.Where(auditor => auditor.AuditoriumId.Equals(newReservation.auditorium));

                foreach (Auditorium a in auditorium)
                {
                    reservManager.Table = new List<List<TD>>();

                    ViewBag.id = "td";

                    ViewBag.week = reservManager.GetDays(reservation.StartDate);

                    reservManager.Table = reservManager.GetWeekReservation(reservation.StartDate, a.AuditoriumName);
                }
            }

            return PartialView("Table", reservManager);
        }
    }
}