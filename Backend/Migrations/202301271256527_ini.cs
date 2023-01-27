namespace Backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ini : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Users", "InKartCourses", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "InKartCourses");
            DropColumn("dbo.Courses", "Price");
        }
    }
}
