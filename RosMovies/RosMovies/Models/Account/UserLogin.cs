using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RosMovies.Models.Account
{
    public class UserLogin
    {
        [Required(ErrorMessage = "Введите Почту")]
        [EmailAddress]
        [Display(Name = "E-mail")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Введите пароль")]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
    }
}