namespace Backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateUserModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Answers",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Content = c.String(),
                        IsCorrect = c.Boolean(nullable: false),
                        Cost = c.Int(nullable: false),
                        QuestionId = c.Int(nullable: false),
                        Name = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        ModifiedAt = c.DateTime(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        Question_ID = c.Long(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Questions", t => t.Question_ID)
                .Index(t => t.Question_ID);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Content = c.String(),
                        Name = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        ModifiedAt = c.DateTime(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        Exam_ID = c.Long(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Exams", t => t.Exam_ID)
                .Index(t => t.Exam_ID);
            
            CreateTable(
                "dbo.Certificates",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                        MediaTemplatePath = c.String(),
                        Name = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        ModifiedAt = c.DateTime(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        Course_ID = c.Long(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Courses", t => t.Course_ID)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.Course_ID)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Duration = c.String(),
                        Description = c.String(),
                        Name = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        ModifiedAt = c.DateTime(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        Exam_ID = c.Long(),
                        User_Id = c.String(maxLength: 128),
                        User_Id1 = c.String(maxLength: 128),
                        User_Id2 = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Exams", t => t.Exam_ID)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .ForeignKey("dbo.Users", t => t.User_Id1)
                .ForeignKey("dbo.Users", t => t.User_Id2)
                .Index(t => t.Exam_ID)
                .Index(t => t.User_Id)
                .Index(t => t.User_Id1)
                .Index(t => t.User_Id2);
            
            CreateTable(
                "dbo.Exams",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Description = c.String(),
                        ExamType = c.String(),
                        Name = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        ModifiedAt = c.DateTime(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Lessons",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        LesssonType = c.Int(nullable: false),
                        Description = c.String(),
                        TextContent = c.String(),
                        MediaContentPath = c.String(),
                        Name = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        ModifiedAt = c.DateTime(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        Course_ID = c.Long(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Courses", t => t.Course_ID)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.Course_ID)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        ModifiedAt = c.DateTime(nullable: false),
                        LastName = c.String(),
                        SurName = c.String(),
                        Login = c.String(),
                        Password = c.String(),
                        PhotoPath = c.String(),
                        Sex = c.Boolean(nullable: false),
                        Email = c.String(),
                        Organisation = c.String(),
                        JobTitle = c.String(),
                        AboutMe = c.String(),
                        Deleted = c.Boolean(nullable: false),
                        UserName = c.String(),
                        NormalizedUserName = c.String(),
                        NormalizedEmail = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        ConcurrencyStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEnd = c.DateTimeOffset(precision: 7),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Lessons", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Courses", "User_Id2", "dbo.Users");
            DropForeignKey("dbo.Courses", "User_Id1", "dbo.Users");
            DropForeignKey("dbo.Certificates", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Courses", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Certificates", "Course_ID", "dbo.Courses");
            DropForeignKey("dbo.Lessons", "Course_ID", "dbo.Courses");
            DropForeignKey("dbo.Courses", "Exam_ID", "dbo.Exams");
            DropForeignKey("dbo.Questions", "Exam_ID", "dbo.Exams");
            DropForeignKey("dbo.Answers", "Question_ID", "dbo.Questions");
            DropIndex("dbo.Lessons", new[] { "User_Id" });
            DropIndex("dbo.Lessons", new[] { "Course_ID" });
            DropIndex("dbo.Courses", new[] { "User_Id2" });
            DropIndex("dbo.Courses", new[] { "User_Id1" });
            DropIndex("dbo.Courses", new[] { "User_Id" });
            DropIndex("dbo.Courses", new[] { "Exam_ID" });
            DropIndex("dbo.Certificates", new[] { "User_Id" });
            DropIndex("dbo.Certificates", new[] { "Course_ID" });
            DropIndex("dbo.Questions", new[] { "Exam_ID" });
            DropIndex("dbo.Answers", new[] { "Question_ID" });
            DropTable("dbo.Users");
            DropTable("dbo.Lessons");
            DropTable("dbo.Exams");
            DropTable("dbo.Courses");
            DropTable("dbo.Certificates");
            DropTable("dbo.Questions");
            DropTable("dbo.Answers");
        }
    }
}
