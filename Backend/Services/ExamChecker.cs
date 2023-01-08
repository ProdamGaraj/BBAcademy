using Backend.Models;

namespace Backend.Services
{
    public class ExamChecker
    {
        public ExamCheckerResponse Check(Exam exam)
        {
            int currentGrade = 0;
            foreach (Question question in exam.Questions)
            {
                currentGrade += question.Answers.FirstOrDefault(x => x.IsChosen == true).Cost;
            }
            currentGrade *= 100;
            return new ExamCheckerResponse() { percent = currentGrade / exam.PassingGrade+1 ,passed = currentGrade>exam.PassingGrade };
        }
    }
}
