﻿using PagedList;
using RosMovies.Models;
using RosMovies.ProgectViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace RosMovies.Controllers
{

    public class MovieController : Controller
    {
        RosMoviesModel db = new RosMoviesModel();

        // GET: Movie
        //public ActionResult Index()
        //{
        //    return View();
        //}

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
        public ActionResult AddMovie(Movie movie, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {

                    movie.ImageMimeType = image.ContentType;
                    movie.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(movie.ImageData, 0, image.ContentLength);

                }



                var existingMovie = db.Movies.FirstOrDefault(m => m.Name == movie.Name);

                if (existingMovie == null)
                {
                    //db.Movies.Add(new Movie
                    //{
                    //    Name = movie.Name,
                    //    Director = movie.Director,
                    //    Actors = movie.Actors,
                    //    Genre = movie.Genre,
                    //    Description = movie.Description
                    //});
                    db.Movies.Add(movie);
                    db.SaveChanges();

                    return RedirectToAction("MovieList");
                }
                else
                {
                    ModelState.AddModelError("", "Этот фильм уже есть в базе");
                    return View("AddMovie");
                }
            }

            return View();
        }


        //[HttpPost]
        public ActionResult MovieList(string movieName , string movieDirector,
            string movieActor , string movieGenre , int page = 1)
        {

            int pageSize = 1;
            if (String.IsNullOrEmpty(movieName) &&
                   String.IsNullOrEmpty(movieDirector) &&
                   String.IsNullOrEmpty(movieActor) &&
                   String.IsNullOrEmpty(movieGenre))
            {

                MovieListViewModel model = new MovieListViewModel
                {
                    Movies = db.Movies
                        .OrderBy(m => m.Name)
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize)
                        .ToList(),

                    PagingInfo = new PagingInfo
                    {
                        CurrentPage = page,
                        ItemsPerPage = pageSize,
                        TotalItems = CountControl(movieName, movieDirector,
                                                movieActor, movieGenre)
                    },

                    CurrentMovieActor = movieActor,
                    CurrentMovieDirector = movieDirector,
                    CurrentMovieGenre = movieGenre,
                    CurrentMovieName = movieName

                };
                return View(model);
            }

            else
            {
                MovieListViewModel model = new MovieListViewModel
                {


                    Movies = MakeMovieList(movieName, movieDirector,
                                                movieActor, movieGenre)
                                                .Skip((page - 1) * pageSize)
                                                .Take(pageSize)
                                                .ToList(),


                    PagingInfo = new PagingInfo
                    {
                        CurrentPage = page,
                        ItemsPerPage = pageSize,
                        TotalItems = CountControl(movieName, movieDirector,
                                                movieActor, movieGenre)
                    },

                    CurrentMovieActor = movieActor,
                    CurrentMovieDirector = movieDirector,
                    CurrentMovieGenre = movieGenre,
                    CurrentMovieName = movieName
                };

                return View(model);
            }


        }

        public FileContentResult GetImage(int id)
        {
            //if (movieId == null)
            //{
            //    throw new ArgumentNullException(nameof(movieId));
            //}

            Movie mov = db.Movies.FirstOrDefault(p => p.Id == id);
            if (mov != null)
            {
                return File(mov.ImageData, mov.ImageMimeType);
            }
            else
            {
                return null;
            }
        }


        private IEnumerable<Movie> MakeMovieList(string movieName, string movieDirector,
                                                string movieActor, string movieGenre)
        {
            IEnumerable<Movie> model = db.Movies
                                       .OrderBy(m => m.Name)
                                       .ToList();

            if (!String.IsNullOrEmpty(movieName))
            {
                model = model.Where(m => m.Name == movieName).ToList();
            }

            if (!String.IsNullOrEmpty(movieActor))
            {
                model = model.Where(m => m.Actors.Contains(movieActor)).ToList();
            }

            if (!String.IsNullOrEmpty(movieDirector))
            {
                model = model.Where(m => m.Director == movieDirector).ToList();
            }

            if (!String.IsNullOrEmpty(movieGenre))
            {
                model = model.Where(m => m.Genre == movieGenre).ToList();
            }

            return model;
        }

        private int CountControl (string movieName, string movieDirector,
                                                string movieActor, string movieGenre)
            

        {
            int answer, helper;

            IEnumerable<Movie> model = db.Movies
                                       .OrderBy(m => m.Name)
                                       .ToList();

            answer = model.Count();

            helper = model.Count();

            if (!String.IsNullOrEmpty(movieName))
            {
                model = model.Where(m => m.Name == movieName).ToList();
                helper = model.Count();

                answer = answer < helper ? answer : helper;
            }

            if (!String.IsNullOrEmpty(movieActor))
            {
                model = model.Where(m => m.Actors.Contains(movieActor)).ToList();
                helper = model.Count();

                answer = answer < helper ? answer : helper;
            }

            if (!String.IsNullOrEmpty(movieDirector))
            {
                model = model.Where(m => m.Director == movieDirector).ToList();
                helper = model.Count();

                answer = answer < helper ? answer : helper;
            }

            if (!String.IsNullOrEmpty(movieGenre))
            {
                model = model.Where(m => m.Genre == movieGenre).ToList();
                helper = model.Count();

                answer = answer < helper ? answer : helper;
            }


            return answer;
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

        [HttpGet]
        public ActionResult Edit (int? id)
        {
            Movie movie = db.Movies.FirstOrDefault(m => m.Id == id);

            return View(movie);
        }



        // POST: Movies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Director,Actors,Description,Genre")] Movie movie,  HttpPostedFileBase image)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("MovieList", "Movie");
            }

            if (ModelState.IsValid)
            {

                if (image != null)
                {

                    movie.ImageMimeType = image.ContentType;
                    movie.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(movie.ImageData, 0, image.ContentLength);

                }


                db.Entry(movie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("MovieList");
            }
            return View("Details", movie.Id);
        }

        // GET: Movies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("MovieList", "Movie");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View("Delete", movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("MovieList", "Movie");
            }

            Movie movie = db.Movies.Find(id);
            db.Movies.Remove(movie);
            db.SaveChanges();
            return RedirectToAction("MovieList", "Movie"); ;
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();

            base.Dispose(disposing);
        }
    }
}