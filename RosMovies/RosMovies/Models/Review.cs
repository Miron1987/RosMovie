using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RosMovies.Models
{
    public class Review
    {
        //мнение зрителей о фильме

        public int Id { get; set; }

        public int MovieId { get; set; }

        //public int UserId { get; set; }

        public string UserName { get; set; }

        public string UserLastName { get; set; }

        public DateTime DateCom { get; set; }

        //[StringLength(250, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 250 символов")]
        public string MovieReview { get; set; }

        public int Score { get; set; }
    }
}