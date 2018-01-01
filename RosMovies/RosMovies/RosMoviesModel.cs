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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasMany(c => c.Movies)
                .WithMany(s => s.Users)
                .Map(t => t.MapLeftKey("UserId")
                .MapRightKey("MovieId")
                .ToTable("UserMovie"));
        }
        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }

    public class MovieDbInitializer : DropCreateDatabaseAlways<RosMoviesModel>
    {
        protected override void Seed(RosMoviesModel db)
        {

            db.Users.Add(new User { FirstName = "Александр", LastName = "Пушкин", Mail = "mail@mail.ru", Password = "123456", Moderator = true });


            db.Movies.Add(new Movie { Name = "Спасти рядового Райана", Director = "Спилберг", Actors = "Хэнкс", Description = "Капитан Джон Миллер получает тяжелое задание. " +
                "Вместе с отрядом из восьми человек Миллер должен отправиться в тыл врага на поиски рядового Джеймса Райана, три родных брата которого почти одновременно " +
                "погибли на полях сражений.Командование приняло решение демобилизовать Райана и отправить его на родину к безутешной матери. Но для того, чтобы найти и спасти солдата," +
                " крошечному отряду придется пройти через все круги ада…", Genre = "Драма" });
            db.Movies.Add(new Movie { Name = "Иван Васильевич меняет профессию", Director = "Гайдай", Actors = "Демьяненко, Яковлев, Куравлев", Description = "Инженер-изобретатель " +
                "Тимофеев сконструировал машину времени, которая соединила его квартиру с далеким шестнадцатым веком — точнее, с палатами государя Ивана Грозного. Туда-то и попадают " +
                "тезка царя пенсионер-общественник Иван Васильевич Бунша и квартирный вор Жорж Милославский.На их место в двадцатом веке переселяется великий государь. Поломка машины" +
                " приводит ко множеству неожиданных и забавных событий…", Genre = "Комедия" });
            db.Movies.Add(new Movie { Name = "Служебный роман", Director = "Рязанов", Actors = "Мягков, Фрейндлих, Басилашвили", Description = "Анатолий Ефремович Новосельцев, рядовой " +
                "служащий одного статистического управления, — человек робкий и застенчивый. Для него неплохо бы получить вакантное место зав. отделом, но он не знает как подступиться к" +
                " этому делу. Старый приятель Самохвалов советует ему приударить за Людмилой Прокофьевной Калугиной, — сухарем в юбке и директором заведения…", Genre = "Комедия" });
            db.Movies.Add(new Movie { Name = "В осаде", Director = "Дэвис", Actors = "Сигал", Description = "Террористы — бывшая элита коммандос — под видом сопровождения рок-группы, " +
                "которая должна выступить перед военными моряками, пробираются на корабль с ядерным оружием. Расправившись с основной частью команды, они пытаются шантажировать правительство" +
                " США.Все для захватчиков судна складывалось благополучно до тех пор, пока они не решили изолировать повара — бывшего морского пехотинца. Кок не мог стерпеть такого издевательства: " +
                "он практически в одиночку хоронит планы подонков один за другим.", Genre = "Боевик" });
            db.Movies.Add(new Movie { Name = "Крик", Director = "Крэйвен", Actors = "Кэмпбелл, Кокс, Аркетт", Description = "По городу прокатилась волна жестоких убийств, жертвами становятся беззащитные" +
                " люди. Новой жертвой телефонного маньяка из городка Вудсборо может стать ещё одна девушка — Сидни Прескотт. Год назад у неё была изнасилована и убита мать, теперь маньяк угрожает ей самой." +
                " Но в первой ночной схватке с убийцей, девушка смогла спастись. Полиция находит на месте покушения маску и плащ. Впрочем, такие маски можно купить в лавке ужасов, и проку для следствия от " +
                "найденных улик нет никакого. Нет, вроде, и мотивов для убийств, если только не принять за главную версию, что маньяк — страстный поклонник фильмов ужасов и убивает свои жертвы только" +
                " ради удовольствия…", Genre = "Ужасы" });
            db.Movies.Add(new Movie { Name = "Список Шиндлера", Director = "Спилберг", Actors = "Нисон, Кингсли, Файнс", Description = "Лента рассказывает реальную историю загадочного Оскара Шиндлера, члена " +
                "нацистской партии, преуспевающего фабриканта, спасшего во время Второй Мировой войны более 1100 евреев. Это триумф одного человека, не похожего на других, и драма тех, кто, благодаря ему, выжил " +
                "в ужасный период человеческой истории.", Genre = "Драма" });
            db.Movies.Add(new Movie { Name = "Звездные войны: Эпизод 1 - Скрытая угроза", Director = "Лукас", Actors = "Нисон, МакГрегор, Портман", Description = "Мирная и процветающая планета Набу. Торговая федерация," +
                " не желая платить налоги, вступает в прямой конфликт с королевой Амидалой, правящей на планете, что приводит к войне. На стороне королевы и республики в ней участвуют два рыцаря-джедая: учитель и ученик, " +
                "Квай-Гон-Джин и Оби-Ван Кеноби…", Genre = "Драма" });
          
            
            //db.Reviews.Add(new Review { MovieId = 1, Score = 6, UserId = 1, MovieReview = "Неплохое кино" });

            base.Seed(db);
        }
    }


    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}