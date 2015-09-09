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
            if (Request.IsAjaxRequest())
            {
                return PartialView();
            }
            
            return View(new RegistrationModel());
        }

        [HttpPost]
        public ActionResult Add(RegistrationModel userInfo)
        {
            if (ModelState.IsValid)
            {
                User user = new User();
                user.FirstName = userInfo.FirstName;
                user.Surname = userInfo.Surname;
                user.Email = userInfo.Email;
                user.Password = userInfo.Password;

                using (Entities db = new Entities())
                {
                    user.Role1 = db.Role.Where(role => role.RoleName.Equals("Client")).First();
                    db.User.Add(user);
                    db.SaveChanges();
                }
                return PartialView(user);
            }
            
            return PartialView("Create", userInfo);
        }
    }
}