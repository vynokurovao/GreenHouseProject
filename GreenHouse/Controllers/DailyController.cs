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

                //var auditoryId = from auditory in db.Auditorium
                //         where auditory.AuditoriumName == room.Name
                //         select auditorium.AuditoriumId;

                //var id = auditoryId.First();
                List<AdditionalEquipment> ae = new List<AdditionalEquipment>();
                if (room.Wifi)
                {
                    var wifi = from addEquipm in db.AdditionalEquipment
                               where addEquipm.AdditionalEquipmentName == "Wifi"
                               select addEquipm;
                    foreach (var ae1 in db.AdditionalEquipment)
                    {
                        if (ae1.AdditionalEquipmentName == "Wifi")
                        {
                            ae1.Auditorium.Add(auditorium);
                        }

                    }
                    

                    ae.Add(wifi.First());
                }

                //if (room.Monitor)
                //{
                //    var monitor = from addEquipm in db.AdditionalEquipment
                //               where addEquipm.AdditionalEquipmentName == "Монитор"
                //               select addEquipm;

                //    auditorium.AdditionalEquipment.Add(monitor.First());
                //}

                //if (room.Projector)
                //{
                //    var projector = from addEquipm in db.AdditionalEquipment
                //                  where addEquipm.AdditionalEquipmentName == "Проектор"
                //                  select addEquipm;

                //    auditorium.AdditionalEquipment.Add(projector.First());
                //}

                //if (room.Microphone)
                //{
                //    var microphone = from addEquipm in db.AdditionalEquipment
                //                    where addEquipm.AdditionalEquipmentName == "Микрофон"
                //                    select addEquipm;

                //    auditorium.AdditionalEquipment.Add(microphone.First());
                //}
                auditorium.AdditionalEquipment = ae;
                db.SaveChanges();
            } 
        }

    }
}