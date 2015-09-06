using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GreenHouse.Models;

namespace GreenHouse.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            Entities db = new Entities();

            //IEnumerable<Reservation> TodayReservation = from r in db.Reservation
            //                                            where r.StartDate.Date == DateTime.Now.Date
            //                                            select r;

            ViewBag.Auditoriums = db.Auditorium;

            return View();
        }
    }
}