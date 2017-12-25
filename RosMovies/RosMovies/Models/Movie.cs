using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RosMovies.Models
{
    public class Movie
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Director { get; set; }

        public string Actors { get; set; }

        public string Description { get; set; }



        public List<Review> Reviews { get; set; }
    }
}