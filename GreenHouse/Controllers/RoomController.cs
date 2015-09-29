using GreenHouse.ContexManager;
using GreenHouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GreenHouse.Controllers
{
    public class RoomController : Controller
    {
        private Entities db = new Entities();

        // GET: Room
        public ActionResult Index(DateTime date, string room)
        {
            if (Session["IsAuthenticated"] == null)
            {
                Session["IsAuthenticated"] = false;
            }

            ReservationManager reservManager = new ReservationManager(date);

            reservManager.Table = new List<List<TD>>();

            reservManager.Table = reservManager.GetDayRoomReservation(date, room);

            ViewBag.Date = date;

            ViewBag.Room = room;

            ViewBag.Auditoriums = db.Auditorium.Where(auditor => auditor.AuditoriumName.Equals(room));

            ViewBag.id = "td-day";

            return View("Room", reservManager);
        }

        public ActionResult ShowRoom(string room)
        {
            if (Session["IsAuthenticated"] == null)
            {
                Session["IsAuthenticated"] = false;
            }

            DateTime date = DateTime.Now;

            ReservationManager reservManager = new ReservationManager(date);

            reservManager.Table = new List<List<TD>>();

            reservManager.Table = reservManager.GetDayRoomReservation(date, room);

            ViewBag.Date = date;

            ViewBag.Room = room;

            ViewBag.id = "td-day";

            return View("Room", reservManager);
        }

        public int IsCanBlockOnPeriod(ReservationAuditoriumDay model)
        {
            int result = 1;

            string[] parts = model.date.Split('.');

            int year = int.Parse(parts[2]), month = int.Parse(parts[1]), day = int.Parse(parts[0]);

            DateTime date = new DateTime(year, month, day, DateTime.Now.Hour + 1, 0, 0);

            if (date < DateTime.Now)
            {
                return 2; //in the past
            }

            ReservationManager reservManager = new ReservationManager(date);

            reservManager.Table = new List<List<TD>>();

            if (model.period)
            {
                reservManager.Table = reservManager.GetDayRoomReservation(date, model.auditorium);
            }
            else
            {
                reservManager.Table = reservManager.GetWeekReservation(date, model.auditorium);
            }

            foreach (List<TD> tr in reservManager.Table)
            {
                foreach (TD td in tr)
                {
                    if (td.ReservationId > 0)
                    {
                        if (td.FinishDate > DateTime.Now || td.StartDate > DateTime.Now)
                        {
                            return 0; //reservation is exist
                        }
                    }
                }
            }

            return result;
        }

        //public DateTime GetLastDayWeek(Date _date)
        //{
        //    DateTime date = new DateTime(_date.year, _date.month, _date.day, 0, 0, 0);

        //    DayOfWeek dayOfWeek = date.DayOfWeek;

        //    DateTime startdate = date;

        //    startdate = startdate.Subtract(new TimeSpan((int)dayOfWeek - 1, 0, 0, 0));

        //    startdate.AddDays(7);

        //    return startdate;
        //}

    }
}