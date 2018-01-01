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
            int pageSize = 2;
            int pageNumber = (page ?? 1);
            ViewBag.Page = page;

            List<Movie> movie = db.Movies
                    .OrderBy(x => x.Name)
                    //.Where(x => x.Name == myQuery)
                    .ToList();


            //return View(results.ToPagedList(pageNumber, pageSize));
            return View(movie.ToPagedList(pageNumber, pageSize));
        }


        [HttpPost]
        public ActionResult MovieList(int page = 1, string quest = "All", string myQuery = "")
        {
            ViewBag.Page = page;
            ViewBag.MyQuery = myQuery;
            int pageSize = 3;
            int pageNumber = page;
            //int pageNumber = (page ?? 1);


            List<Movie> movie = db.Movies
                    .OrderBy(x => x.Name)
                    //.Where(x => x.Name == myQuery)
                    .ToList();

            switch (quest)
            {
                case "name":
                    movie = movie
                        .Where(x => x.Name == myQuery)
                        .ToList();
                    break;

                case "director":
                    movie = movie
                        .Where(x => x.Director == myQuery)
                        .ToList();
                    break;

                case "actor":
                    movie = movie
                        .Where(x => x.Actors == myQuery)
                        .ToList();
                    break;

                case "genre":
                    movie = movie
                        .Where(x => x.Genre == myQuery)
                        .ToList();
                    break;

            }
            return View(movie.ToPagedList(pageNumber, pageSize));
        }


        //public ActionResult NewMovieList(int? page, string quest = "All", string myQuery = "")
        //{

        //    ViewBag.MyQuery = myQuery;
        //    int pageSize = 1;
        //    int pageNumber = (page ?? 1);


        //    List<Movie> movie = db.Movies
        //            //.Where(x => x.Name == myQuery)
        //            .ToList();

        //    switch (quest)
        //    {
        //        case "name":
        //            movie = movie
        //                .Where(x => x.Name.Contains(myQuery))
        //                .ToList();
        //            break;

        //        case "director":
        //            movie = movie
        //                .Where(x => x.Director.Contains(myQuery))
        //                .ToList();
        //            break;

        //        case "actor":
        //            movie = movie
        //                .Where(x => x.Actors.Contains(myQuery))
        //                .ToList();
        //            break;

        //        case "genre":
        //            movie = movie
        //                .Where(x => x.Genre.Contains(myQuery))
        //                .ToList();
        //            break;

        //    }

        //    return View("MovieList", movie.ToPagedList(pageNumber, pageSize));
        //}


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