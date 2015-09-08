using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GreenHouse.Models;

namespace GreenHouse.Controllers
{
    public class RegistrationController : Controller
    {
        [HttpGet]
        public ActionResult Create()
        {
            Models.User user = new Models.User();
            return View(user);
        }

        [HttpPost]
        public ActionResult Add(Models.User user)
        {
            using (Entities db = new Entities())
            {
                user.Role1 = db.Role.Where(role => role.RoleName.Equals("Client")).First();
                db.User.Add(user);
                db.SaveChanges();
            }
            return View(user);
        }
    }
}