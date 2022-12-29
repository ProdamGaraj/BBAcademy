using Backend.Services.Repository.ICRUD;
using Microsoft.AspNetCore.Identity;

namespace Backend.Models
{
    public class Course : Entity
    {
        public string Duration { get; set; }
        public string Description { get; set; }
        public List<Lesson> Lessons { get; set; }
        public Exam Exam{ get; set; }
        public Course()
        {

        }
        public Course(string duration, string description, List<Lesson> lessons, Exam exam,bool deleted= false)
        {
            Duration = duration;
            Description = description;
            Lessons = lessons;
            Exam = exam;
            Deleted = deleted;
        }
    }
}