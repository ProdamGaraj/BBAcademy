using Backend.Models;
using Backend.Services.Repository;

namespace Backend.Services
{
    public class ExamChecker
    {
        public ExamCheckerResponse Check(Course course)
        {
            int currentGrade = 0;
            foreach (Question question in course.Exam.Questions)
            {
                currentGrade += question.Answers.FirstOrDefault(x => x.IsChosen == true).Cost;
            }
            currentGrade *= 100;
            ExamCheckerResponse ecr = new ExamCheckerResponse() { percent = currentGrade / course.Exam.PassingGrade + 1, passed = currentGrade > course.Exam.PassingGrade };
            if(ecr.passed)
            {
                course.CourseType = CourseType.Passed;
                CourseRepository cr = new CourseRepository();
                cr.Update(course);
            }
            return ecr;
        }
    }
}
