using RosMovies.Models;
using RosMovies.Models.Account;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace RosMovies.Controllers
{
    public class UserController : Controller
    {
        RosMoviesModel db = new RosMoviesModel();

        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLogin user)
        {
            if (ModelState.IsValid)
            {
                var existingUser = db.Users.FirstOrDefault(u => u.Mail == user.Login && u.Password == user.Password);
                if (existingUser == null)
                {
                    ModelState.AddModelError("", "Пользователя с таким логином и паролем нет");
                    return View(user);
                }
                //else if (user.Login == true && existingUser != null)
                //{
                //    FormsAuthentication.SetAuthCookie(user.Login, true);
                //    return RedirectToAction("Index", "Admin");
                //}
                else
                {
                    var id = existingUser.Id;
                    FormsAuthentication.SetAuthCookie(user.Login, true);
                    return RedirectToAction("Details", "User");
                }
            }
            else
            {
                return View(user);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public ActionResult Registration()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
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


        [HttpGet]
        public ActionResult Details()
        {

            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            var user = db.Users.FirstOrDefault(x => x.Mail == User.Identity.Name);
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(user);
        }

       
        //public ActionResult ShowFavoriteMovie


        public ActionResult FavoriteMovies()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            //var existingUser = db.Users.FirstOrDefault(u => u.Mail == User.Identity.Name);
            User user = db.Users.FirstOrDefault(x => x.Mail == User.Identity.Name);

            //List<Movie> movies = user.Movies;

            //List<User> user = db.Users
            //    .Where(x => x.Mail == User.Identity.Name)
            //    .Include(m => m.Movies)
            //    .Include(x => x.Movies.Select(e => e.Name))
            //    .ToList();

            //var current = user.Where(x => x.Mail == User.Identity.Name);

            return View(user);
        }

        //[HttpPost]
        public ActionResult AddFavorite(int? id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("MovieList", "Movie");
            }

            if (id == null)
            {
                return View("Index", "Home");
            }

            User user = db.Users.FirstOrDefault(x => x.Mail == User.Identity.Name);
            Movie movie = db.Movies.FirstOrDefault(x => x.Id == id);

            if (user.Movies.Contains<Movie>(movie))
            {
                user.Movies.Remove(movie);
                db.SaveChanges();

                //    return RedirectToAction("MovieList", "Movie");
            }
            else
            {
                user.Movies.Add(movie);
                db.SaveChanges();
            }

            return RedirectToAction("MovieList", "Movie");
        }


        //[HttpPost]
        //public ActionResult AddFavorite(Movie movie)
        //{
        //    if (!User.Identity.IsAuthenticated)
        //    {
        //        return RedirectToAction("MovieList", "Movie");
        //    }

        //    User user = db.Users.FirstOrDefault(x => x.Mail == User.Identity.Name);
        //    var curMovie = db.Movies.Where(m => m.Id == movie.Id);

        //    //Movie newMovie = new Movie {Name = movie.Name, Actors = movie.Actors, Description = movie.Description,
        //    //        Director = movie.Director, Genre = movie.Genre, Id = movie.Id, Reviews = movie.Reviews};




        //    if (!user.Movies.Contains(movie))
        //    {
        //        try
        //        {
        //            user.Movies.Add(movie);
        //            //db.Entry(user).State = EntityState.Modified;
        //            db.SaveChanges();
        //        }
        //        catch (DbEntityValidationException ex)
        //        {
        //            foreach (DbEntityValidationResult validationError in ex.EntityValidationErrors)
        //            {
        //                Response.Write("Object: " + validationError.Entry.Entity.ToString());
        //                Response.Write("    ");
        //                foreach (DbValidationError err in validationError.ValidationErrors)
        //                {
        //                    Response.Write(err.ErrorMessage + "    ");
        //                }
        //            }
        //        }


        //    }
        //    else
        //    {
        //        user.Movies.Remove(movie);
        //        //db.Entry(user).State = EntityState.Modified;
        //        db.SaveChanges();
        //    }


        //    return RedirectToAction("MovieList", "Movie");
        //}


        protected override void Dispose(bool disposing)
        {
            db.Dispose();

            base.Dispose(disposing);
        }


    }
}