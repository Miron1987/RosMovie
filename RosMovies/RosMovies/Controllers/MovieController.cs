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

        public ActionResult MovieList(int? page, string movieName = "Поиск по названию", string movieDirector = "Поиск по режиссеру",
            string movieActor = "Поиск по актерам", string movieGenre = "Поиск по жанру")
        {
            int pageSize = 1;
            int pageNumber = (page ?? 1);
            ViewBag.MovieName = movieName;
            ViewBag.MovieDirector = movieDirector;
            ViewBag.MovieActor = movieActor;
            ViewBag.MovieGenre = movieGenre;

            List<Movie> movie = db.Movies
                    .OrderBy(x => x.Name)
                    .ToList();
       
            if (movieName != "Поиск по названию")
            {
                movie = movie
                    .Where(x => x.Name.Contains(movieName))
                    .ToList();
            }

            if (movieDirector != "Поиск по режиссеру")
            {
                movie = movie
                    .Where(x => x.Director.Contains(movieDirector))
                    .ToList();
            }

            if (movieActor != "Поиск по актерам")
            {
                movie = movie
                    .Where(x => x.Actors.Contains(movieActor))
                    .ToList();
            }

            if (movieGenre != "Поиск по жанру")
            {
                movie = movie
                    .Where(x => x.Genre.Contains(movieGenre))
                    .ToList();
            }

            return View(movie.ToPagedList(pageNumber, pageSize));
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
                .Where(x => x.MovieReview.Length > 0)
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

            if (review.MovieReview.Length == 0 || review.MovieReview == "Введите не более 250 символов")
            {
                return RedirectToAction("MovieList", "Movie");
            }

            db.Reviews.Add(new Review
            {
                MovieId = review.MovieId,
                UserName = review.UserName,
                UserLastName = review.UserLastName,
                DateCom = review.DateCom,
                MovieReview = review.MovieReview,
                Score = review.Score
            });
            db.SaveChanges();

            return RedirectToAction("Details", "Movie", review.MovieId);
        }

        public JsonResult SearchMovieList(string query)
        {

            List<Movie> movies = db.Movies
                .Where(x => x.Name.Contains(query))
                .OrderBy(x => x.Name)
                .ToList();

            return new JsonResult { Data = movies };
        }
    }
}