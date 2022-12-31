using Backend.Models;
using Backend.Services.Repository.Interfaces;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace Backend.Services.Repository
{
    public class QuestionRepository:IQuestionRepository
    {
        public void Add(Question entity)
        {
            using (BBAcademyDb db = new BBAcademyDb())
            {
                entity.CreatedAt = DateTime.Now;
                entity.ModifiedAt = DateTime.Now;
                db.Questions.Add(entity);
                db.SaveChanges();
            }
        }

        public Question Get(long id)
        {
            using (BBAcademyDb db = new BBAcademyDb())
            {
                Question Question = db.Questions.SingleOrDefault(b => b.Id == id && !b.Deleted);
                return Question;
            }
        }
        public IList<Question> GetConditionalType(Dictionary<QuestionType,int> keyValue)
        {
            using (BBAcademyDb db = new BBAcademyDb())
            {
                List<Question> questions = new List<Question>();
                for (int i = 0; i < keyValue.Count; i++)
                {
                    int j = (int)keyValue.Keys.ElementAt(i);
                    questions.AddRange(db.Questions.Where(x => (int)x.QuestionType==j).Take(keyValue.Values.ElementAt(i)));
                }
                return questions;
            }
        }

        public IList<Question> GetAll()
        {
            using (BBAcademyDb db = new BBAcademyDb())
            {
                IList<Question> myQuestion = db.Questions.ToList();
                return myQuestion;
            }
        }
        public IList<Question> GetAllCustomId(long id)
        {
            using (BBAcademyDb db = new BBAcademyDb())
            {
                List<Question> myQuestion = new List<Question>();
                return myQuestion;
            }
        }

        public void MarkAsDeleted(Question entity)
        {
            using (BBAcademyDb db = new BBAcademyDb())
            {
                
                var result = db.Questions.SingleOrDefault(b => b.Id.Equals(entity.Id));
                if (result != null)
                {
                    result.Deleted = true;
                    result.ModifiedAt = DateTime.Now;
                    db.SaveChanges();
                }
            }
        }

        public void Update(Question entity)
        {
            using (BBAcademyDb db = new BBAcademyDb())
            {
                var result = db.Questions.SingleOrDefault(b => b.Id.Equals(entity.Id));
                if (result != null)
                {
                    entity.ModifiedAt = DateTime.Now;
                    db.Questions.AddOrUpdate(entity);
                    db.SaveChanges();
                }
            }
        }
    }
}
