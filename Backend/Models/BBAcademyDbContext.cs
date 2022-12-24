using System.Data.Entity;
using Backend.Models;

namespace Backend
{
    public class BBAcademyDb:DbContext
    {
        DbSet<User> Users { get; set; }
        DbSet<Organisation> Organizations { get; set; }
        DbSet<Course> Courses { get; set; }
        DbSet<Certificate> Certificates { get; set; }
        DbSet<Exam> Exams { get; set; }
        DbSet<Lesson> Lessons { get; set; }
        DbSet<Question> Questions { get; set; }
        DbSet<Answer> Answers { get; set; }
    }
}
