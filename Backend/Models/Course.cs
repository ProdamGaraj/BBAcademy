using Backend.Models.Enum;
using Backend.Services.Repository.ICRUD;
using Microsoft.AspNetCore.Identity;

namespace Backend.Models
{
    public class Course : Entity
    {
        public string MediaPath { get; set; }
        public string Duration { get; set; }
        public string Description { get; set; }
        public CourseType CourseType { get; set; }
        public ICollection<Lesson> Lessons { get; set; }
        public Exam Exam{ get; set; }
        public decimal Price { get; set; }
        public Course()
        {

        }
        public Course(string mediaPath, string duration, string description, CourseType courseType, ICollection<Lesson> lessons, Exam exam,bool deleted = false, decimal price = 0)
        {
            MediaPath = mediaPath;
            Duration = duration;
            Description = description;
            CourseType = courseType;
            Lessons = lessons;
            Exam = exam;
            Deleted = deleted;
            Price = price;
        }
    }
}