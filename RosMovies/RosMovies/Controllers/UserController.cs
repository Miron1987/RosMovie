using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RosMovies.Controllers
{
    public class UserController : Controller
    {
        RosMoviesModel db = new RosMoviesModel();

        // GET: User
        public ActionResult Index()
        {
            return View();
        }


    }
}