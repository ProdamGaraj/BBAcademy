﻿using Backend.Models;
using Backend.Services.Repository;

namespace Backend.Services
{
    public class ExamCreator
    {
        public async Task<Exam> CreateExamWithId(string description, string examType, List<long> ids)
        {
            Exam exam = new Exam(description, examType, new List<Question>());
            foreach(long id in ids)
            {
                QuestionRepository qr = new QuestionRepository();
                exam.Questions.Add( await qr.Get(id));
            }
            return exam;
        } 
        public async Task<Exam> CreateExamWithType(string description, string examType, Dictionary<QuestionType,int> keyValues)
        {
            Exam exam= new Exam(description, examType, new List<Question>());
            QuestionRepository qr = new QuestionRepository();
            IList<Question> list = await qr.GetConditionalType(keyValues);
            exam.Questions.AddRange(list);
            return exam;
        } 
    }
}