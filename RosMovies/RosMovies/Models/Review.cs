using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RosMovies.Models
{
    public class Review
    {
        //мнение зрителей о фильме

        public int Id { get; set; }

        public int MovieId { get; set; }

        public int UserId { get; set; }

        public string MovieReview { get; set; }

        public int Score { get; set; }
    }
}