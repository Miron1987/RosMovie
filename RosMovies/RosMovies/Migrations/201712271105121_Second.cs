namespace RosMovies.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Second : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Movies", "User_Id", "dbo.Users");
            DropIndex("dbo.Movies", new[] { "User_Id" });
            CreateTable(
                "dbo.UserMovie",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        MovieId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.MovieId })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Movies", t => t.MovieId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.MovieId);
            
            DropColumn("dbo.Movies", "User_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Movies", "User_Id", c => c.Int());
            DropForeignKey("dbo.UserMovie", "MovieId", "dbo.Movies");
            DropForeignKey("dbo.UserMovie", "UserId", "dbo.Users");
            DropIndex("dbo.UserMovie", new[] { "MovieId" });
            DropIndex("dbo.UserMovie", new[] { "UserId" });
            DropTable("dbo.UserMovie");
            CreateIndex("dbo.Movies", "User_Id");
            AddForeignKey("dbo.Movies", "User_Id", "dbo.Users", "Id");
        }
    }
}
