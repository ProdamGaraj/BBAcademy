﻿namespace Backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Answers",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Content = c.String(),
                        IsCorrect = c.Boolean(nullable: false),
                        Cost = c.Int(nullable: false),
                        Name = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        ModifiedAt = c.DateTime(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        Question_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Questions", t => t.Question_Id)
                .Index(t => t.Question_Id);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Content = c.String(),
                        QuestionType = c.Int(nullable: false),
                        Name = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        ModifiedAt = c.DateTime(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        Exam_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Exams", t => t.Exam_Id)
                .Index(t => t.Exam_Id);
            
            CreateTable(
                "dbo.Certificates",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                        MediaTemplatePath = c.String(),
                        Name = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        ModifiedAt = c.DateTime(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        User_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Duration = c.String(),
                        Description = c.String(),
                        CourseType = c.Int(nullable: false),
                        Name = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        ModifiedAt = c.DateTime(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        Exam_Id = c.Long(),
                        Certificate_Id = c.Long(),
                        User_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Exams", t => t.Exam_Id)
                .ForeignKey("dbo.Certificates", t => t.Certificate_Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.Exam_Id)
                .Index(t => t.Certificate_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Exams",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Description = c.String(),
                        ExamType = c.String(),
                        Name = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        ModifiedAt = c.DateTime(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Lessons",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        LesssonType = c.Int(nullable: false),
                        Description = c.String(),
                        TextContent = c.String(),
                        MediaContentPath = c.String(),
                        Name = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        ModifiedAt = c.DateTime(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        Course_Id = c.Long(),
                        User_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.Course_Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.Course_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Role = c.Int(nullable: false),
                        LastName = c.String(),
                        SurName = c.String(),
                        Email = c.String(),
                        Login = c.String(),
                        PasswordHash = c.String(),
                        PhotoPath = c.String(),
                        Sex = c.Boolean(nullable: false),
                        Organisation = c.String(),
                        JobTitle = c.String(),
                        AboutMe = c.String(),
                        Name = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        ModifiedAt = c.DateTime(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Lessons", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Courses", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Certificates", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Courses", "Certificate_Id", "dbo.Certificates");
            DropForeignKey("dbo.Lessons", "Course_Id", "dbo.Courses");
            DropForeignKey("dbo.Courses", "Exam_Id", "dbo.Exams");
            DropForeignKey("dbo.Questions", "Exam_Id", "dbo.Exams");
            DropForeignKey("dbo.Answers", "Question_Id", "dbo.Questions");
            DropIndex("dbo.Lessons", new[] { "User_Id" });
            DropIndex("dbo.Lessons", new[] { "Course_Id" });
            DropIndex("dbo.Courses", new[] { "User_Id" });
            DropIndex("dbo.Courses", new[] { "Certificate_Id" });
            DropIndex("dbo.Courses", new[] { "Exam_Id" });
            DropIndex("dbo.Certificates", new[] { "User_Id" });
            DropIndex("dbo.Questions", new[] { "Exam_Id" });
            DropIndex("dbo.Answers", new[] { "Question_Id" });
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
