using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RosMovies.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите Название")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Введите Режиссера")]
        public string Director { get; set; }

        [Required(ErrorMessage = "Введите Актеров")]
        public string Actors { get; set; }

        [Required(ErrorMessage = "Введите описание")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Введите жанр")]
        public string Genre { get; set; }

        public List<Review> Reviews { get; set; }

    }
}