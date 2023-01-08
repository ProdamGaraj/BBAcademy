using Backend.Models;
using Backend.Services.Repository;

namespace Backend.Services
{
    public class ExamChecker
    {
        public async Task<ExamCheckerResponse> Check(User user, Course course)
        {
            int currentGrade = 0;
            foreach (Question question in course.Exam.Questions)
            {
                currentGrade += question.Answers.FirstOrDefault(x => x.IsChosen == true).Cost;
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
            if(ecr.passed)
            {
                course.CourseType = CourseType.Passed;
                CourseRepository cr = new CourseRepository();
                cr.Update(course);
            }
            CertificateService cs = new CertificateService();
            await cs.CreateCertificate(user,course,ecr.passed);
            return ecr;
        }
    }
}
