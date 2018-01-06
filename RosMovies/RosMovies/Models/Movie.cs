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
        [Display(Name = "Название")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Введите Режиссера")]
        [Display(Name = "Режиссер")]
        public string Director { get; set; }

        [Required(ErrorMessage = "Введите Актеров")]
        [Display(Name = "Актеры")]
        public string Actors { get; set; }

        [Required(ErrorMessage = "Введите описание")]
        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Введите жанр")]
        [Display(Name = "Жанр")]
        public string Genre { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }

        public virtual ICollection<User> Users { get; }

        public Movie()
        {
            Reviews = new List<Review>();
            Users = new List<User>();
        }


    }
}