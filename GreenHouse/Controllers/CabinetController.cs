using GreenHouse.ContexManager;
using GreenHouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GreenHouse.Controllers
{
    public class CabinetController : Controller
    {

        // GET: Cabinet
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ChangePlace()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Save(User ModifyUser)
        {
            Entities db = new Entities();

            foreach (User user in db.User)
            {
                if (Session["UserEmail"].ToString() == user.Email)
                {
                    if (ModifyUser.FirstName != null)
                    {
                        user.FirstName = ModifyUser.FirstName;

                        Session["UserFirstName"] = ModifyUser.FirstName;
                    }

                    if (ModifyUser.Surname != null)
                    {
                        user.Surname = ModifyUser.Surname;

                        Session["UserSurname"] = ModifyUser.Surname;
                    }

                    if (ModifyUser.Email != null)
                    {
                        user.Email = ModifyUser.Email;

                        Session["UserEmail"] = ModifyUser.Email;
                    }
                }
            }

            db.SaveChanges();

            db.Dispose();

            return PartialView("ChangePlace");
        }

        [HttpPost]
        public ActionResult SavePassword(Password newpass)
        {
            Entities db = new Entities();

            foreach (User user in db.User)
            {
                if (Session["UserEmail"].ToString() == user.Email)
                {
                    if (newpass.password != null && newpass.confirm!=null)
                    {
                        user.Password = newpass.password;
                    }
                }
            }

            db.SaveChanges();

            db.Dispose();

            return PartialView("ChangePlace");
        }
    }
}