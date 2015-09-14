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
                        return PartialView("Create", userInfo);
                    }
                    else
                    {
                        return PartialView();
                    }
                }
            }
      
            return PartialView("Create", userInfo);
        }
    }
}