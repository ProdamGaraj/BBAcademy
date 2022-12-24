namespace Backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Certificates",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Description = c.String(),
                        ImageTemplatePath = c.String(),
                        CertificateType = c.Int(nullable: false),
                        Name = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        ChangedAt = c.DateTime(nullable: false),
                        User_ID = c.Long(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.User_ID)
                .Index(t => t.User_ID);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Description = c.String(),
                        CourseType = c.Int(nullable: false),
                        Name = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        ChangedAt = c.DateTime(nullable: false),
                        Certificate_ID = c.Long(),
                        Exam_ID = c.Long(),
                        User_ID = c.Long(),
                        User_ID1 = c.Long(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Certificates", t => t.Certificate_ID)
                .ForeignKey("dbo.Exams", t => t.Exam_ID)
                .ForeignKey("dbo.Users", t => t.User_ID)
                .ForeignKey("dbo.Users", t => t.User_ID1)
                .Index(t => t.Certificate_ID)
                .Index(t => t.Exam_ID)
                .Index(t => t.User_ID)
                .Index(t => t.User_ID1);
            
            CreateTable(
                "dbo.Exams",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        ChangedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Description = c.String(),
                        Name = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        ChangedAt = c.DateTime(nullable: false),
                        Exam_ID = c.Long(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Exams", t => t.Exam_ID)
                .Index(t => t.Exam_ID);
            
            CreateTable(
                "dbo.Lessons",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Description = c.String(),
                        VideoPath = c.String(),
                        Name = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        ChangedAt = c.DateTime(nullable: false),
                        Course_ID = c.Long(),
                        User_ID = c.Long(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Courses", t => t.Course_ID)
                .ForeignKey("dbo.Users", t => t.User_ID)
                .Index(t => t.Course_ID)
                .Index(t => t.User_ID);
            
            CreateTable(
                "dbo.Organisations",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        KeyCount = c.Int(nullable: false),
                        Description = c.String(),
                        Name = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        ChangedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        LastName = c.String(),
                        SurName = c.String(),
                        Email = c.String(),
                        Login = c.String(),
                        Password = c.String(),
                        JobTitle = c.String(),
                        Status = c.Int(nullable: false),
                        AboutMe = c.String(),
                        Name = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        ChangedAt = c.DateTime(nullable: false),
                        Organisation_ID = c.Long(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Organisations", t => t.Organisation_ID)
                .Index(t => t.Organisation_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "Organisation_ID", "dbo.Organisations");
            DropForeignKey("dbo.Lessons", "User_ID", "dbo.Users");
            DropForeignKey("dbo.Courses", "User_ID1", "dbo.Users");
            DropForeignKey("dbo.Courses", "User_ID", "dbo.Users");
            DropForeignKey("dbo.Certificates", "User_ID", "dbo.Users");
            DropForeignKey("dbo.Lessons", "Course_ID", "dbo.Courses");
            DropForeignKey("dbo.Courses", "Exam_ID", "dbo.Exams");
            DropForeignKey("dbo.Questions", "Exam_ID", "dbo.Exams");
            DropForeignKey("dbo.Courses", "Certificate_ID", "dbo.Certificates");
            DropIndex("dbo.Users", new[] { "Organisation_ID" });
            DropIndex("dbo.Lessons", new[] { "User_ID" });
            DropIndex("dbo.Lessons", new[] { "Course_ID" });
            DropIndex("dbo.Questions", new[] { "Exam_ID" });
            DropIndex("dbo.Courses", new[] { "User_ID1" });
            DropIndex("dbo.Courses", new[] { "User_ID" });
            DropIndex("dbo.Courses", new[] { "Exam_ID" });
            DropIndex("dbo.Courses", new[] { "Certificate_ID" });
            DropIndex("dbo.Certificates", new[] { "User_ID" });
            DropTable("dbo.Users");
            DropTable("dbo.Organisations");
            DropTable("dbo.Lessons");
            DropTable("dbo.Questions");
            DropTable("dbo.Exams");
            DropTable("dbo.Courses");
            DropTable("dbo.Certificates");
        }
    }
}
