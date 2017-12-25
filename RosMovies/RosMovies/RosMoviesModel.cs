namespace RosMovies
{
    using RosMovies.Models;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class RosMoviesModel : DbContext
    {
        // Your context has been configured to use a 'RosMoviesModel' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'RosMovies.RosMoviesModel' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'RosMoviesModel' 
        // connection string in the application configuration file.
        public RosMoviesModel()
            : base("name=RosMoviesModel")
        {           
            
        }

        public RosMoviesModel(String sqlConnectionName) :
            base($"Name={sqlConnectionName}")
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<Review> Reviews { get; set; }


        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }

    public class MovieDbInitializer : DropCreateDatabaseAlways<RosMoviesModel>
    {
        protected override void Seed(RosMoviesModel db)
        {
            db.Users.Add(new User { FirstName = "Александр", LastName = "Пушкин", Mail = "mail@mail.ru", Password = "123456", Moderator = true});
            db.Movies.Add(new Movie { Name = "Спасти рядового Райана", Director = "Спилберг", Actors = "Хэнкс", Description = "Спасают солдата"});
            db.Reviews.Add(new Review { MovieId = 1, Score = 6, UserId = 1, MovieReview = "Неплохое кино"});

            base.Seed(db);
        }
    }


    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}