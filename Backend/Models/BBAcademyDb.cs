using Backend.Models;
using Microsoft.EntityFrameworkCore;
//using System.Data.Entity;


namespace Backend
{
    public class BBAcademyDb : DbContext
    {
        public BBAcademyDb(DbContextOptions<BBAcademyDb> options)
        : base(options)
        {
           //Database.EnsureCreated();
        }

        public DbSet<Answer> Answers { get; set; }
        public DbSet<Certificate> Certificates { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<CertificateTemplate> CertificateTemplates { get; set; }
    }
}