namespace Backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "InKartCourses", c => c.String());
            AlterColumn("dbo.Users", "Lang", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "Lang", c => c.Int(nullable: false));
            DropColumn("dbo.Users", "InKartCourses");
        }
    }
}
