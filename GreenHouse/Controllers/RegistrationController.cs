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
            Entities db = new Entities();

            IQueryable<User> existUser = db.User.Where(user => user.Email.Equals(userInfo.Email));

            if (ModelState.IsValid && existUser.Count() == 0)
            {
                User user = new User();

                user.FirstName = userInfo.FirstName;

                user.Surname = userInfo.Surname;

                user.Email = userInfo.Email;

                user.Password = userInfo.Password;

                user.Role1 = db.Role.Where(role => role.RoleName.Equals("Client")).First();

                db.User.Add(user);

                db.SaveChanges();

                Session["IsAuthenticated"] = "true";

                Session["UserSurname"] = user.Surname;

                Session["UserFirstName"] = user.FirstName;

                string rol = user.Role1.RoleName;

                Session["UserRole"] = rol;

                Session["UserEmail"] = user.Email;

                ViewBag.Close = true;

                return PartialView("Create", userInfo);
            }

            ViewBag.Close = false;

            return PartialView("Create", userInfo);
        }
    }
}