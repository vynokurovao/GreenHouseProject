using GreenHouse.ContexManager;
using GreenHouse.Models;
using System.Web.Mvc;

namespace GreenHouse.Controllers
{
    public class CabinetController : Controller
    {
        public Entities db = new Entities();

        // GET: Cabinet
        public ActionResult Index()
        {
            UserReservation userReservation = new UserReservation();

            foreach (User user in db.User)
            {
                if (Session["UserEmail"].ToString() == user.Email)
                {
                    userReservation = new UserReservation(user);
                }
            }
            return View(userReservation);
        }

        [HttpGet]
        public ActionResult ChangePlace()
        {
            UserReservation userReservation = new UserReservation();

            foreach (User user in db.User)
            {
                if (Session["UserEmail"].ToString() == user.Email)
                {
                    userReservation = new UserReservation(user);
                }
            }
            return View(userReservation);
        }

        [HttpPost]
        public ActionResult Save(User modifyUser)
        {
            Entities db = new Entities();

            UserReservation userReservation = new UserReservation();

            foreach (User user in db.User)
            {
                if (Session["UserEmail"].ToString() == user.Email)
                {
                    if (modifyUser.FirstName != null)
                    {
                        user.FirstName = modifyUser.FirstName;

                        Session["UserFirstName"] = modifyUser.FirstName;
                    }

                    if (modifyUser.Surname != null)
                    {
                        user.Surname = modifyUser.Surname;

                        Session["UserSurname"] = modifyUser.Surname;
                    }

                    if (modifyUser.Email != null)
                    {
                        user.Email = modifyUser.Email;

                        Session["UserEmail"] = modifyUser.Email;
                    }

                    userReservation = new UserReservation(user);
                }
            }

            db.SaveChanges();

            return PartialView("ChangePlace", userReservation);
        }

        [HttpPost]
        public ActionResult SavePassword(Password newpass)
        {
            Entities db = new Entities();

            UserReservation userReservation = new UserReservation();

            foreach (User user in db.User)
            {
                if (Session["UserEmail"].ToString() == user.Email)
                {
                    if (newpass.password != null && newpass.confirm!=null)
                    {
                        user.Password = newpass.password;

                        userReservation = new UserReservation(user);
                    }
                }
            }

            db.SaveChanges();

            return PartialView("ChangePlace",userReservation);
        }
    }
}