namespace RosMovies.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fourth1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reviews", "UserName", c => c.String());
            AddColumn("dbo.Reviews", "UserLastName", c => c.String());
            AddColumn("dbo.Reviews", "DateCom", c => c.DateTime(nullable: false));
            DropColumn("dbo.Reviews", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reviews", "UserId", c => c.Int(nullable: false));
            DropColumn("dbo.Reviews", "DateCom");
            DropColumn("dbo.Reviews", "UserLastName");
            DropColumn("dbo.Reviews", "UserName");
        }
    }
}
