namespace Backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
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
                        Course_ID = c.Long(),
                        User_ID = c.Long(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Courses", t => t.Course_ID)
                .ForeignKey("dbo.Users", t => t.User_ID)
                .Index(t => t.Course_ID)
                .Index(t => t.User_ID);
            
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
                        Exam_ID = c.Long(),
                        User_ID = c.Long(),
                        Profession_ID = c.Long(),
                        User_ID1 = c.Long(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Exams", t => t.Exam_ID)
                .ForeignKey("dbo.Users", t => t.User_ID)
                .ForeignKey("dbo.Professions", t => t.Profession_ID)
                .ForeignKey("dbo.Users", t => t.User_ID1)
                .Index(t => t.Exam_ID)
                .Index(t => t.User_ID)
                .Index(t => t.Profession_ID)
                .Index(t => t.User_ID1);
            
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
                        Course_ID = c.Long(),
                        User_ID = c.Long(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Courses", t => t.Course_ID)
                .ForeignKey("dbo.Users", t => t.User_ID)
                .Index(t => t.Course_ID)
                .Index(t => t.User_ID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        LastName = c.String(),
                        SurName = c.String(),
                        PhotoPath = c.String(),
                        Sex = c.Boolean(nullable: false),
                        Email = c.String(),
                        JobTitle = c.String(),
                        AboutMe = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        ModifiedAt = c.DateTime(nullable: false),
                        Key_ID = c.Long(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Keys", t => t.Key_ID)
                .Index(t => t.Key_ID);
            
            CreateTable(
                "dbo.Keys",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Data = c.String(),
                        Available = c.Boolean(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Name = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        ModifiedAt = c.DateTime(nullable: false),
                        Profession_ID = c.Long(),
                        Organisation_ID = c.Long(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Professions", t => t.Profession_ID)
                .ForeignKey("dbo.Organisations", t => t.Organisation_ID)
                .Index(t => t.Profession_ID)
                .Index(t => t.Organisation_ID);
            
            CreateTable(
                "dbo.Professions",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        ModifiedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Organisations",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        ModifiedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Keys", "Organisation_ID", "dbo.Organisations");
            DropForeignKey("dbo.Lessons", "User_ID", "dbo.Users");
            DropForeignKey("dbo.Courses", "User_ID1", "dbo.Users");
            DropForeignKey("dbo.Users", "Key_ID", "dbo.Keys");
            DropForeignKey("dbo.Keys", "Profession_ID", "dbo.Professions");
            DropForeignKey("dbo.Courses", "Profession_ID", "dbo.Professions");
            DropForeignKey("dbo.Courses", "User_ID", "dbo.Users");
            DropForeignKey("dbo.Certificates", "User_ID", "dbo.Users");
            DropForeignKey("dbo.Certificates", "Course_ID", "dbo.Courses");
            DropForeignKey("dbo.Lessons", "Course_ID", "dbo.Courses");
            DropForeignKey("dbo.Courses", "Exam_ID", "dbo.Exams");
            DropForeignKey("dbo.Questions", "Exam_ID", "dbo.Exams");
            DropForeignKey("dbo.Answers", "Question_ID", "dbo.Questions");
            DropIndex("dbo.Keys", new[] { "Organisation_ID" });
            DropIndex("dbo.Keys", new[] { "Profession_ID" });
            DropIndex("dbo.Users", new[] { "Key_ID" });
            DropIndex("dbo.Lessons", new[] { "User_ID" });
            DropIndex("dbo.Lessons", new[] { "Course_ID" });
            DropIndex("dbo.Courses", new[] { "User_ID1" });
            DropIndex("dbo.Courses", new[] { "Profession_ID" });
            DropIndex("dbo.Courses", new[] { "User_ID" });
            DropIndex("dbo.Courses", new[] { "Exam_ID" });
            DropIndex("dbo.Certificates", new[] { "User_ID" });
            DropIndex("dbo.Certificates", new[] { "Course_ID" });
            DropIndex("dbo.Questions", new[] { "Exam_ID" });
            DropIndex("dbo.Answers", new[] { "Question_ID" });
            DropTable("dbo.Organisations");
            DropTable("dbo.Professions");
            DropTable("dbo.Keys");
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
