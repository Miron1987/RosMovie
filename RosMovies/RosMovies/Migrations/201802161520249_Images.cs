namespace RosMovies.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Images : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movies", "ImageData", c => c.Binary());
            AddColumn("dbo.Movies", "ImageMimeType", c => c.String());
            AddColumn("dbo.Users", "ImageData", c => c.Binary());
            AddColumn("dbo.Users", "ImageMimeType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "ImageMimeType");
            DropColumn("dbo.Users", "ImageData");
            DropColumn("dbo.Movies", "ImageMimeType");
            DropColumn("dbo.Movies", "ImageData");
        }
    }
}
