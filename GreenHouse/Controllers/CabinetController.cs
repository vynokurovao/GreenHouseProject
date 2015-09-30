using GreenHouse.ContexManager;
using GreenHouse.Models;
using System.Linq;
using System.Web.Mvc;
using System.Web.Services.Description;

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

        public ActionResult ValidateUserData(User modifyUser)
        {
            Validation answer = new Validation();

            answer = IsValidName(modifyUser.FirstName);

            if (answer.IsValid)
            {
                answer = IsValidName(modifyUser.Surname);

                if (answer.IsValid)
                {
                    answer = IsValidEmail(modifyUser.Email);
                }
                else
                {
                    answer.Message = "Фамилия " + answer.Message;
                }
            }
            else
            {
                answer.Message = "Имя " + answer.Message;
            }

            return Json(answer, JsonRequestBehavior.AllowGet);
        }

        public Validation IsValidName(string name)
        {
            Validation validation = new Validation {IsValid = true, Message = ""};

            if (name != null)
            {
                if (name.Length < 3)
                {
                    validation.IsValid = false;

                    validation.Message = "слишком короткое. Должно быть больше 3 символов";
                }
                else if (name.Length > 50)
                {
                    validation.IsValid = false;

                    validation.Message = "слишком длинное. Должно быть до 50 символов";
                }
            }

            return validation;
        }

        public Validation IsValidEmail(string email)
        {
            Validation validation = new Validation {IsValid = true, Message = ""};

            if (email == null)
            {
                return validation;
            }

            IQueryable<User> user = db.User.Where(u => u.Email.Equals(email));
            
            int ampersantCount = 0;

            bool allCharsIsLatin = true;

            for (int i = 0; i < email.Length; i++)
            {
                if (email[i] == '@')
                {
                    ampersantCount++;
                }

                int symbol = (char) email[i];

                if (
                    !(symbol == 46 || (symbol >= 48 && symbol <= 57) || (symbol >= 64 && symbol <= 90) ||
                      (symbol >= 97 && symbol <= 122)))
                {
                    allCharsIsLatin = false;
                }
            }

            if (user.Count() != 0)
            {
                validation.IsValid = false;

                validation.Message = "Пользователь с указанным Email уже зарегестрирован";
            }
            else if (email.Length > 320)
            {
                validation.IsValid = false;

                validation.Message = "Email слишком длинный. Должен быть до 320 символов";
            }
            else if (!email.Contains('@'))
            {
                validation.IsValid = false;

                validation.Message = "Email должен содержать символ @";
            }
            else if (email[0] == '@')
            {
                validation.IsValid = false;

                validation.Message = "Первый символ не может быть @";
            }
            else if (ampersantCount != 1)
            {
                validation.IsValid = false;

                validation.Message = "Слишком много символов @ ";
            }else if (!allCharsIsLatin)
            {
                validation.IsValid = false;

                validation.Message = "Используються недопустимые символы";
            }

            return validation;
        }
    }
}