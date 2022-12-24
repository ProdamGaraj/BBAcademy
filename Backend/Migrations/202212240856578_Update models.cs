namespace Backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Updatemodels : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Certificates", "UserName", c => c.String());
            AddColumn("dbo.Certificates", "UserSurname", c => c.String());
            AddColumn("dbo.Certificates", "UserLastname", c => c.String());
            AddColumn("dbo.Certificates", "UserId", c => c.Int(nullable: false));
            AddColumn("dbo.Exams", "ExamType", c => c.String());
            AddColumn("dbo.Users", "PhotoPath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "PhotoPath");
            DropColumn("dbo.Exams", "ExamType");
            DropColumn("dbo.Certificates", "UserId");
            DropColumn("dbo.Certificates", "UserLastname");
            DropColumn("dbo.Certificates", "UserSurname");
            DropColumn("dbo.Certificates", "UserName");
        }
    }
}
