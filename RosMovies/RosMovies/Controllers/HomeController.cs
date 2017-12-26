using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RosMovies.Controllers
{
    public class HomeController : Controller
    {
        RosMoviesModel db = new RosMoviesModel();

        public ActionResult Index()
        {
            

            return View(db.Movies);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}