using Backend.Models;
using Backend.Services.Repository.Interfaces;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace Backend.Services.Repository
{
    public class QuestionRepository : IQuestionRepository
    {
        public async Task<bool> Add(Question entity)
        {
            try
            {
                using (BBAcademyDb db = new BBAcademyDb())
                {
                    entity.CreatedAt = DateTime.Now;
                    entity.ModifiedAt = DateTime.Now;
                    db.Questions.Add(entity);
                    await db.SaveChangesAsync();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<Question> Get(long id)
        {
            try
            {
                using (BBAcademyDb db = new BBAcademyDb())
                {
                    Question Question = await db.Questions.Include("QuestionToAnswers").FirstOrDefaultAsync(b => b.Id == id && !b.Deleted);
                    return Question;
                }
            }
            catch (Exception ex)
            {
                return new Question();
            }
        }

        public async Task<IList<Question>> GetAll()
        {
            try
            {
                using (BBAcademyDb db = new BBAcademyDb())
                {
                    IList<Question> myQuestion = await db.Questions.Include("QuestionToAnswers").ToListAsync();
                    return myQuestion;
                }
            }
            catch (Exception ex)
            {
                return new List<Question>();
            }
        }
        public async Task<Question> GetWithoutContext(long id)
        {
            try
            {
                using (BBAcademyDb db = new BBAcademyDb())
                {
                    Question Question = await db.Questions.FirstOrDefaultAsync(b => b.Id == id && !b.Deleted);
                    return Question;
                }
            }
            catch (Exception ex)
            {
                return new Question();
            }
        }

        public async Task<IList<Question>> GetAllWithoutContext()
        {
            try
            {
                using (BBAcademyDb db = new BBAcademyDb())
                {
                    IList<Question> myQuestion = await db.Questions.ToListAsync();
                    return myQuestion;
                }
            }
            catch (Exception ex)
            {
                return new List<Question>();
            }
        }

        public async Task<bool> MarkAsDeleted(Question entity)
        {
            try
            {
                using (BBAcademyDb db = new BBAcademyDb())
                {
                    var result = await db.Questions.FirstOrDefaultAsync(b => b.Id.Equals(entity.Id));
                    if (result != null)
                    {
                        result.Deleted = true;
                        result.ModifiedAt = DateTime.Now;
                        await db.SaveChangesAsync();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> Update(Question entity)
        {
            try
            {
                using (BBAcademyDb db = new BBAcademyDb())
                {
                    var result = await db.Questions.FirstOrDefaultAsync(b => b.Id.Equals(entity.Id));
                    if (result != null)
                    {
                        entity.ModifiedAt = DateTime.Now;
                        db.Questions.Update(entity);
                        await db.SaveChangesAsync();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<IList<Question>> GetConditionalType(Dictionary<QuestionType, int> keyValue)
        {
            try
            {
                using (BBAcademyDb db = new BBAcademyDb())
                {
                    List<Question> questions = new List<Question>();
                    for (int i = 0; i < keyValue.Count; i++)
                    {
                        int j = (int)keyValue.Keys.ElementAt(i);
                        questions.AddRange(db.Questions.Include("Answers").Where(x => (int)x.QuestionType == j).Take(keyValue.Values.ElementAt(i)));
                    }
                    return questions;
                }
            }
            catch (Exception ex)
            {
                return new List<Question>();
            }
        }
    }
}
