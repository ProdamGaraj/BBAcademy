using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class BilimContext : DbContext
    {
        public BilimContext(DbContextOptions<BilimContext> options)
            : base(options)
        {
        }

        public DbSet<AnswerOption> Answers { get; set; }
        public DbSet<Certificate> Certificates { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<CertificateTemplate> CertificateTemplates { get; set; }
    }
}