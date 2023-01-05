using Backend.Models;
using Backend.Models.Enum;
using Backend.Services.Repository;

namespace Backend.Services
{
    public class ExamCreator
    {
        public async Task<Exam> CreateExamWithId(string description, string examType, List<long> ids)
        {
            Exam exam = new Exam(description, examType, new List<ExamToQuestion>());
            foreach(long id in ids)
            {
                QuestionRepository qr = new QuestionRepository();
                exam.Questions.Add( new ExamToQuestion { QuestionId=id});
            }
            return exam;
        } 
        public async Task<Exam> CreateExamWithType(string description, string examType, Dictionary<QuestionType,int> keyValues)
        {
            Exam exam= new Exam(description, examType, new List<ExamToQuestion>());
            QuestionRepository qr = new QuestionRepository();
            IList<Question> list = await qr.GetConditionalType(keyValues);
            foreach(Question question in list)
            {
                exam.Questions.Add(new ExamToQuestion { QuestionId = question.Id});
            }
            return exam;
        } 
    }
}
