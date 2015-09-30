using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using GreenHouse.Models;

namespace GreenHouse.Controllers
{
    public class DailyController : Controller
    {
        [HttpPost]
        public JsonResult BookRoom(string date, string content)
        {
            return Json(new { date = date, content = content });
        }

        [HttpGet]
        public JsonResult GetRooms()
        {
            Entities db = new Entities();
            var model = new List<object>() { };
            foreach (Auditorium auditorium in db.Auditorium)
            {
                Dictionary<string, bool> equipment = new Dictionary<string, bool>();
                foreach (AdditionalEquipment addEq in db.AdditionalEquipment)
                {
                    equipment.Add(addEq.AdditionalEquipmentName, false);
                }

                foreach (AdditionalEquipment ae in auditorium.AdditionalEquipment)
                {
                    if (equipment.ContainsKey(ae.AdditionalEquipmentName))
                    {
                        equipment[ae.AdditionalEquipmentName] = true;
                    }
                }
                model.Add(new
                {
                    number = auditorium.AuditoriumName,
                    places = auditorium.Capacity,
                    wifi = equipment["Wifi"],
                    projector = equipment[ "Проектор"],
                    monitor = equipment["Монитор"],
                    microphone = equipment["Микрофон"]
                });
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void AddNewRoom(NewRoomModel room)
        {
            Entities db = new Entities();

            if (ModelState.IsValid)
            {
                Auditorium auditorium = new Auditorium();

                auditorium.AuditoriumName = room.Name;

                auditorium.Capacity = room.Capacity;

                db.Auditorium.Add(auditorium);

                db.SaveChanges();

                int auditoriumId = db.Auditorium.Where(aud => aud.AuditoriumName.Equals(room.Name)).First().AuditoriumId;

                if (room.Wifi)
                {
                    int wifi = db.AdditionalEquipment.Where(addeq => addeq.AdditionalEquipmentName.Equals("Wifi")).First().AdditionalEquipmentId;

                    db.InsertAudEq(auditoriumId, wifi);
                }

                if (room.Monitor)
                {
                    int monitor = db.AdditionalEquipment.Where(addeq => addeq.AdditionalEquipmentName.Equals("Монитор")).First().AdditionalEquipmentId;

                    db.InsertAudEq(auditoriumId, monitor);
                }

                if (room.Projector)
                {
                    int projector = db.AdditionalEquipment.Where(addeq => addeq.AdditionalEquipmentName.Equals("Проектор")).First().AdditionalEquipmentId;

                    db.InsertAudEq(auditoriumId, projector);
                }

                if (room.Microphone)
                {
                    int microphone = db.AdditionalEquipment.Where(addeq => addeq.AdditionalEquipmentName.Equals("Микрофон")).First().AdditionalEquipmentId;

                    db.InsertAudEq(auditoriumId, microphone);
                }
                
                db.SaveChanges();
            } 
        }

    }
}