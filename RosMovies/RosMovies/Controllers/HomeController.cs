using RosMovies.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace RosMovies.Controllers
{
    public class HomeController : Controller
    {
        RosMoviesModel db = new RosMoviesModel();

        public ActionResult Index()
        {
            

            return View(db.Movies);
        }

        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration(User user)
        {
            if (ModelState.IsValid)
            {
                var existingUser = db.Users.FirstOrDefault(u => u.Mail == user.Mail);

                if (existingUser == null)
                {
                    db.Users.Add(new User
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Mail = user.Mail,
                        Password = user.Password,
                        Moderator = false
                    });
                    db.SaveChanges();
                    FormsAuthentication.SetAuthCookie(user.Mail, true);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Пользователь с таким e-mail уже существует");
                    return View(user);
                }
            }
            else
            {
                return View(user);
            }
        }

        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your application description page.";

        //    return View();
        //}

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}
    }
}