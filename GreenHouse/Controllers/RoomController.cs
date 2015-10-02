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

            if (
                (from tr in reservManager.Table from td in tr where td.ReservationId > 0 select td).Any(
                    td => td.FinishDate > DateTime.Now || td.StartDate > DateTime.Now))
            {
                return 0; //reservation is exist
            }

            return result;
        }

        public ActionResult ChangeRoom(NewRoomModel room)
        {
            Auditorium changeAuditorium =
                db.Auditorium.Where(a => a.AuditoriumName.Equals(room.Name)).First();

            changeAuditorium.Capacity = room.Capacity;

            AdditionalEquipment wifi = db.AdditionalEquipment.Where(addeq => addeq.AdditionalEquipmentName.Equals("Wifi")).First();

            AdditionalEquipment monitor = db.AdditionalEquipment.Where(addeq => addeq.AdditionalEquipmentName.Equals("Монитор")).First();

            AdditionalEquipment projector = db.AdditionalEquipment.Where(addeq => addeq.AdditionalEquipmentName.Equals("Проектор")).First();

            AdditionalEquipment microphone = db.AdditionalEquipment.Where(addeq => addeq.AdditionalEquipmentName.Equals("Микрофон")).First();

            if (changeAuditorium.AdditionalEquipment.Contains(wifi))
            {
                if (room.Wifi == false)
                {
                    db.DeleteAuditoriumEquipment(changeAuditorium.AuditoriumId, wifi.AdditionalEquipmentId);
                }
            }
            else
            {
                if (room.Wifi == true)
                {
                    db.InsertAudEq(changeAuditorium.AuditoriumId, wifi.AdditionalEquipmentId);
                }
            }

            if (changeAuditorium.AdditionalEquipment.Contains(monitor))
            {
                if (room.Monitor == false)
                {
                    db.DeleteAuditoriumEquipment(changeAuditorium.AuditoriumId, monitor.AdditionalEquipmentId);
                }
            }
            else
            {
                if (room.Monitor == true)
                {
                    db.InsertAudEq(changeAuditorium.AuditoriumId, monitor.AdditionalEquipmentId);
                }
            }

            if (changeAuditorium.AdditionalEquipment.Contains(projector))
            {
                if (room.Projector == false)
                {
                    db.DeleteAuditoriumEquipment(changeAuditorium.AuditoriumId, projector.AdditionalEquipmentId);
                }
            }
            else
            {
                if (room.Projector == true)
                {
                    db.InsertAudEq(changeAuditorium.AuditoriumId, projector.AdditionalEquipmentId);
                }
            }

            if (changeAuditorium.AdditionalEquipment.Contains(microphone))
            {
                if (room.Microphone == false)
                {
                    db.DeleteAuditoriumEquipment(changeAuditorium.AuditoriumId, microphone.AdditionalEquipmentId);
                }
            }
            else
            {
                if (room.Microphone == true)
                {
                    db.InsertAudEq(changeAuditorium.AuditoriumId, microphone.AdditionalEquipmentId);
                }
            }

            db.SaveChanges();

            ViewBag.Room = room.Name;

            DateTime date = DateTime.Now;

            ReservationManager reservManager = new ReservationManager(date);

            reservManager.Table = new List<List<TD>>();

            reservManager.Table = reservManager.GetDayRoomReservation(date, room.Name);

            ViewBag.Date = date;

            ViewBag.Room = room.Name;

            ViewBag.id = "td-day";

            return View("Room", reservManager);
        }

        public ActionResult IsRoomExist(NewRoomModel room)
        {
            Validation answer = new Validation();

            IQueryable<Auditorium> rooms = db.Auditorium.Where(a => a.AuditoriumName.Equals(room.Name));

            if (rooms.Count() == 0)
            {
                answer.IsValid = false;

                answer.Message = "Указанная аудитория не найдена";
            }
            else
            {
                answer.IsValid = true;

                answer.Message = "Указанная аудитория найдена";
            }

            return Json(answer, JsonRequestBehavior.AllowGet);
        }

    }
}