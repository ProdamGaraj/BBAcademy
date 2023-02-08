using System.Collections.Generic;
using Backend.Models;

namespace Backend.ViewModels
{
    public class QuestionViewModel
    {
        public string Data { get; set; }
        public List<Answer> Answers{ get; set; }
    }
}
