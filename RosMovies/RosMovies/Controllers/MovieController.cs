using PagedList;
using RosMovies.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RosMovies.Controllers
{
    public class MovieController : Controller
    {
        RosMoviesModel db = new RosMoviesModel();

        // GET: Movie
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddMovie (int? id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            var existingUser = db.Users.FirstOrDefault(u => u.Mail == User.Identity.Name);
            if (existingUser.Moderator != true)
            {
                return View("Index", "Home");
            }

            return View();


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddMovie(Movie movie)
        {
            if (ModelState.IsValid)
            {
                var existingMovie = db.Movies.FirstOrDefault(m => m.Name == movie.Name);

                if (existingMovie == null)
                {
                    db.Movies.Add(new Movie
                    {
                        Name = movie.Name,
                        Director = movie.Director,
                        Actors = movie.Actors,
                        Genre = movie.Genre,
                        Description = movie.Description
                    });
                db.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Этот фильм уже есть в базе");
                    return View("AddMovie", "Movie");
                }
            }

            return View();
        }

        [HttpGet]
        public ActionResult MovieList(int? page)
        {
            int pageSize = 3;
            int pageNumber = (page ?? 1);


            List<Movie> movie = db.Movies
                    //.Where(x => x.Name == myQuery)
                    .ToList();


            //return View(results.ToPagedList(pageNumber, pageSize));
            return View(movie.ToPagedList(pageNumber, pageSize));
        }


        [HttpPost]
        public ActionResult MovieList(int? page, string myQuery = "Все")
        {

            ViewBag.MyQuery = myQuery;
            int pageSize = 3;
            int pageNumber = (page ?? 1);


            List<Movie> movie = db.Movies
                    //.Where(x => x.Name == myQuery)
                    .ToList();

            return View(movie.ToPagedList(pageNumber, pageSize));
        }


        public ActionResult NewMovieList(int? page, string myQuery = "Все")
        {

            ViewBag.MyQuery = myQuery;
            int pageSize = 10;
            int pageNumber = (page ?? 1);


            List<Movie> movie = db.Movies
                    //.Where(x => x.Name == myQuery)
                    .ToList();

            return View("MovieList", movie.ToPagedList(pageNumber, pageSize));
        }


        [HttpGet]
        public ActionResult Details(int? id)
        {

            User user = db.Users.FirstOrDefault(x => x.Mail == User.Identity.Name);

            Movie myMovie = db.Movies.FirstOrDefault(x => x.Id == id);

            ViewBag.User = user;
            ViewBag.Movie = myMovie;

            List<Review> reviews = db.Reviews
                .Where(y => y.MovieId == id)
                .ToList();

            return View(reviews);
        }

        [HttpPost]
        public ActionResult Details(Review review)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("MovieList", "Movie");
            }

            db.Reviews.Add(new Review
            {
                MovieId = review.MovieId,
                //UserId = review.UserId,
                UserName = review.UserName,
                UserLastName = review.UserLastName,
                DateCom = review.DateCom,
                MovieReview = review.MovieReview,
                Score = review.Score
            });
            db.SaveChanges();

            return RedirectToAction("MovieList","Movie");
        }
    }
}