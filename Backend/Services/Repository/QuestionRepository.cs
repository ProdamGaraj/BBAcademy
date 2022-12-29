using Backend.Models;
using Backend.Services.Repository.Interfaces;
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

        public IList<Question> GetAll()
        {
            using (BBAcademyDb db = new BBAcademyDb())
            {
                IList<Question> myQuestion = db.Questions.ToList();
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
