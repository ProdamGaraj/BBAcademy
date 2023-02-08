using System.Collections.Generic;
using Backend.Models;

namespace Backend.ViewModels
{
    public class ExamViewModel
    {
        public User User { get; set; }
        public Course Course { get; set; }
        public List<Question> Questions { get; set; }

    }
}
