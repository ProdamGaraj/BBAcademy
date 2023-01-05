using Backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Backend
{
    public class BBAcademyDb : DbContext
    {

        public BBAcademyDb(DbContextOptions<BBAcademyDb> options) : base(options) { }

        protected BBAcademyDb(DbContextOptions options) : base(options) { }
        public BBAcademyDb() => Database.EnsureCreated();


        public DbSet<Answer> Answers { get; set; }
        public DbSet<Certificate> Certificates { get; set; }
        public DbSet<CertificateToCourse> CertificateToCourses { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseToExam> CourseToExams { get; set; }
        public DbSet<CourseToLesson> CourseToLessons { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<ExamToQuestion> ExamToQuestions { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuestionToAnswer> QuestionToAnswers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserToCertificate> UserToCertificates { get; set; }
        public DbSet<UserToCourse> UserToCourses { get; set; }
        public DbSet<UserToLesson> UserToLessons { get; set; }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<User>()
        //        .ToTable("Users")
        //        .HasKey(x => x.Id);
        //
        //    modelBuilder.Entity<Course>()
        //        .ToTable("Courses")
        //        .HasKey(x => x.Id);
        //
        //    modelBuilder.Entity<Answer>()
        //        .ToTable("Answers")
        //        .HasKey(x => x.Id);
        //
        //    modelBuilder.Entity<Certificate>()
        //        .ToTable("Certificates")
        //        .HasKey(x => x.Id);
        //
        //    modelBuilder.Entity<CertificateToCourse>()
        //        .ToTable("CertificateToCourses")
        //        .HasKey(x => x.Id);
        //
        //    modelBuilder.Entity<CourseToExam>()
        //        .ToTable("CourseToExams")
        //        .HasKey(x => x.Id);
        //
        //    modelBuilder.Entity<CourseToLesson>()
        //        .ToTable("CourseToLessons")
        //        .HasKey(x => x.Id);
        //
        //    modelBuilder.Entity<Exam>()
        //        .ToTable("Exams")
        //        .HasKey(x => x.Id);
        //
        //    modelBuilder.Entity<ExamToQuestion>()
        //        .ToTable("ExamToQuestions")
        //        .HasKey(x => x.Id);
        //
        //    modelBuilder.Entity<Lesson>()
        //        .ToTable("Lessons")
        //        .HasKey(x => x.Id);
        //
        //    modelBuilder.Entity<Question>()
        //        .ToTable("Questions")
        //        .HasKey(x => x.Id);
        //
        //    modelBuilder.Entity<QuestionToAnswer>()
        //        .ToTable("QuestionToAnswers")
        //        .HasKey(x => x.Id);
        //
        //    modelBuilder.Entity<UserToCertificate>()
        //        .ToTable("UserToCertificates")
        //        .HasKey(x => x.Id);
        //
        //    modelBuilder.Entity<UserToCourse>()
        //        .ToTable("UserToCourses")
        //        .HasKey(x => x.Id);
        //
        //    modelBuilder.Entity<UserToCourse>()
        //        .ToTable("UserToCourses")
        //        .HasKey(x => x.Id);
        //}
    }
}