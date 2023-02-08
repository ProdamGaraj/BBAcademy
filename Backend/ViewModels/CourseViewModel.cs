using System.Collections.Generic;
using Backend.Models;

namespace Backend.ViewModels
{
    public class CourseViewModel
    {
        public long IdCourse{get;set;}
        public User User { get; set; }
        public List<Lesson> AllLessons { get; set; }
        public int CurrentLesson { get; set; }
        public Exam Exam { get; set; }
        public bool IsBought { get; set; }
    }
}
