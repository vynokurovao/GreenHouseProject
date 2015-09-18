using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using GreenHouse.Models;

namespace Internship.Controllers
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
    }
}