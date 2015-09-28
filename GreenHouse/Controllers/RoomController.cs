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

            return View("Room", reservManager);
        }
    }
}