using RosMovies.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RosMovies.ProgectViewModels
{
    public class MovieListViewModel
    {
        public IEnumerable<Movie> Movies { get; set; } // список фильмов
        public PagingInfo PagingInfo { get; set; }  // для пагинации
        public string CurrentMovieName { get; set; }
        public string CurrentMovieDirector { get; set; }
        public string CurrentMovieActor { get; set; }
        public string CurrentMovieGenre { get; set; }

    }
}

