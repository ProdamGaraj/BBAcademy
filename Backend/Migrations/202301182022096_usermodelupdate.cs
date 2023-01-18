namespace Backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class usermodelupdate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Courses", "User_Id", "dbo.Users");
            DropIndex("dbo.Courses", new[] { "User_Id" });
            AddColumn("dbo.Users", "BoughtCourses", c => c.String());
            DropColumn("dbo.Courses", "User_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Courses", "User_Id", c => c.Long());
            DropColumn("dbo.Users", "BoughtCourses");
            CreateIndex("dbo.Courses", "User_Id");
            AddForeignKey("dbo.Courses", "User_Id", "dbo.Users", "Id");
        }
    }
}
