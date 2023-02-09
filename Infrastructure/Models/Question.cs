﻿using Infrastructure.Models.Enum;

namespace Infrastructure.Models
{
    public class Question : Entity
    {
        public long ExamId { get; set; }

        public virtual Exam Exam { get; set; }
        
        public string MediaPath { get; set; }
        public string Content { get; set; }
        public QuestionType QuestionType { get; set; }
        public virtual ICollection<AnswerOption> AnswerOptions { get; set; }
    }
}