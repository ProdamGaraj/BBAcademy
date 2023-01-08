﻿using Backend.Models;
using Backend.Services.Repository;
using Newtonsoft.Json;

namespace Backend.Services
{
    public class ExamService
    {

        public async Task<Exam> CreateExamWithId(string description, string examType, List<long> ids)
        {
            Exam exam = new Exam(description, examType, new List<Question>());
            foreach (long id in ids)
            {
                QuestionRepository qr = new QuestionRepository();
                exam.Questions.Add(await qr.Get(id));
            }
            return exam;
        }
        public async Task<Exam> CreateExamWithType(string description, string examType, Dictionary<QuestionType, int> keyValues)
        {
            Exam exam = new Exam(description, examType, new List<Question>());
            QuestionRepository qr = new QuestionRepository();
            IList<Question> list = await qr.GetConditionalType(keyValues);
            exam.Questions = list;
            return exam;
        }
        public async Task<ExamCheckerResponse> Check(User user, Course course)
        {
            int currentGrade = 0;
            foreach (Question question in course.Exam.Questions)
            {
                currentGrade += question.Answers.FirstOrDefault(x => x.IsChosen == true&&x.IsCorrect==true).Cost;
            }
            int roundingUp = 0;
            double convertingToDecimal = currentGrade;
            convertingToDecimal /= course.Exam.PassingGrade;
            if(convertingToDecimal > 0)
            {
                roundingUp++;
            }
            currentGrade *= 100;
            ExamCheckerResponse ecr = new ExamCheckerResponse() { percent = currentGrade / course.Exam.PassingGrade + roundingUp, passed = currentGrade > course.Exam.PassingGrade };
            if (user.PassedCoursesId is null)
                user.PassedCoursesId = "";
            List<long> ids = JsonConvert.DeserializeObject<List<long>>(user.PassedCoursesId);
            if(ecr.passed)
            {
                ids.Add(course.Id);
                user.PassedCoursesId = JsonConvert.SerializeObject(ids);
                UserRepository ur = new UserRepository();
                await ur.Update(user);
            }
            CertificateService cs = new CertificateService();
            await cs.CreateCertificate(user,course,ecr.passed);
            return ecr;
        }
    }
}
