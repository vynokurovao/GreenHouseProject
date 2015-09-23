using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GreenHouse.Models;

namespace GreenHouse.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public ActionResult Create()
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView();
            }

            return View(new LoginModel());
        }

        [HttpPost]
        public ActionResult Add(LoginModel userInfo)
        {
            if (ModelState.IsValid)
            {
                using (Entities db = new Entities())
                {
                    IQueryable<User> users = db.User
                        .Where(user => user.Email.Equals(userInfo.Email) && user.Password.Equals(userInfo.Password));

                    if (users.AsEnumerable().Count() == 0)
                    {
                        ModelState.AddModelError("", "Данные введены не верно");

                        ViewBag.Close = false;

                        return PartialView("Create", userInfo);
                    }
                    else
                    {
                        foreach (User curuser in users)
                        {
                            Session["IsAuthenticated"] = "true";

                            Session["UserSurname"] = curuser.Surname;

                            Session["UserFirstName"] = curuser.FirstName;

                            string rol = curuser.Role1.RoleName;

                            Session["UserRole"] = rol;

                            Session["UserEmail"] = curuser.Email;
                        }

                        ViewBag.Close = true;

                        return PartialView("Create", userInfo);
                    }
                }
            }

            return PartialView("Create", userInfo);
        }

        [HttpGet]
        public ActionResult LogOut()
        {

            Session["IsAuthenticated"] = "false";

            Session["UserSurname"] = null;

            Session["UserFirstName"] = null;

            Session["UserRole"] = null;

            Session["UserEmail"] = null;

            ViewBag.Close = false;

            return RedirectToAction("Index", "Home");
        }
    }
}