using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RosMovies.Models
{
    public class Movie
    {
        [Display(AutoGenerateField = false)]
        public int Id { get; set; }

        [ScaffoldColumn(false)]
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

        public byte[] ImageData { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ImageMimeType { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }

        public virtual ICollection<User> Users { get; }

        public Movie()
        {
            Reviews = new List<Review>();
            Users = new List<User>();
        }


    }
}