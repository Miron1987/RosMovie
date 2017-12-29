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
        public ActionResult MovieList()
        {

            //return View(results.ToPagedList(pageNumber, pageSize));
            return View(db.Movies);
        }


        [HttpPost]
        public ActionResult MovieList(string name = "Все", string genre = "Все", string director = "Все", string actor = "Все")
        {


            return View(db.Movies);
        }






        [HttpGet]
        public ActionResult Details(int? id)
        {
            //List<Movie> movies = db.Movies
            //    .Where(x => x.Id == id)
            //    .Include(x => x.Reviews)
            //.Include(x => x.Reviews.Select(b => b.UserId))
            //.Include(x => x.Reviews.Select(t => t.MovieId))
            //.Include(x => x.Reviews.Select(e => e.MovieReview))
            //.ToList();

            ViewBag.movie = db.Movies.
                Where(y => y.Id == id)
                .ToList();

            List<Review> reviews = db.Reviews
                .Where(y => y.MovieId == id)
                .ToList();



            //results = results
            //      .Where(y => y.Date.Value.Date >= date1.Value.Date)
            //      .Where(y => y.Date.Value.Date <= date2.Value.Date)
            //      .ToList();

            //    List<Bet> bets = db.Bets
            //        .Where(x => x.UserId == existingUser.Id)
            //        .Include(x => x.BetEvents)
            //        .Include(x => x.BetEvents.Select(e => e.BetEventStatus))
            //        .Include(x => x.BetEvents.Select(e => e.Event))
            //        .Include(x => x.BetEvents.Select((b => b.Match)))
            //        .Include(x => x.BetEvents.Select(v => v.Match.Championship))
            //        .ToList();
            //    switch (type)

            return View(reviews);
        }
    }
}