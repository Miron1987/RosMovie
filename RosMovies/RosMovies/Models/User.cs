using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RosMovies.Models
{
    public class User
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "Введите имя")]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Введите фамилию")]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }


        [Required(ErrorMessage = "Введите Email")]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Mail { get; set; }


        [Required(ErrorMessage = "Введите пароль")]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        public bool Moderator { get; set; }

        public virtual ICollection<Movie> Movies { get; set; }

        public User()
        {
            Movies = new List<Movie>();
        }
       

    }
}