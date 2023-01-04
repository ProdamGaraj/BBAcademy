using Backend.Services.Repository.ICRUD;
using Microsoft.AspNetCore.Identity;

namespace Backend.Models
{
    public class Course : Entity
    {
        public string Duration { get; set; }
        public string Description { get; set; }
        public CourseType CourseType { get; set; }
        public List<CourseToLesson> Lessons { get; set; }
        public CourseToExam Exam{ get; set; }
        public Course()
        {

        }
        public Course(string duration, string description, CourseType courseType, List<CourseToLesson> lessons, CourseToExam exam,bool deleted= false)
        {
            Duration = duration;
            Description = description;
            CourseType = courseType;
            Lessons = lessons;
            Exam = exam;
            Deleted = deleted;
        }
    }
}