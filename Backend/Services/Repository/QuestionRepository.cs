using Backend.Models;
using Backend.Models.Enum;
using Backend.Services.Repository.Interfaces;
using NLog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Backend.Services.Repository
{
    public class QuestionRepository : IQuestionRepository
    {

        Logger logger;
        private readonly BBAcademyDb db;

        public QuestionRepository(BBAcademyDb db)
        {
            logger = LogManager.GetCurrentClassLogger();
            this.db = db;
        }
        public async Task<bool> Add(Question entity)
        {
            try
            {

                entity.CreatedAt = DateTime.Now;
                entity.ModifiedAt = DateTime.Now;
                db.Questions.Add(entity);
                AnswerRepository ar = new AnswerRepository(db);
                if (entity.Answers is not null)
                    foreach (Answer lesson in entity.Answers)
                    {
                        if (await ar.Get(lesson.Id) is not null)
                        {
                            db.Entry(lesson).State = EntityState.Unchanged;
                        }
                    }
                await db.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message + ":" + ex.InnerException + ":" + ex.StackTrace);
                return false;
            }
        }

        public async Task<Question> Get(long id)
        {
            try
            {


                Question Question = await db.Questions.Include("Answers").FirstOrDefaultAsync(b => b.Id == id && !b.Deleted);
                return Question;

            }
            catch (Exception ex)
            {
                logger.Error(ex.Message + ":" + ex.InnerException + ":" + ex.StackTrace);
                return null;
            }
        }

        public async Task<IList<Question>> GetAll()
        {
            try
            {

                IList<Question> myQuestion = await db.Questions.Include("Answers").ToListAsync();
                return myQuestion;

            }
            catch (Exception ex)
            {
                logger.Error(ex.Message + ":" + ex.InnerException + ":" + ex.StackTrace);
                return null;
            }
        }

        public async Task<bool> MarkAsDeleted(Question entity)
        {
            try
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
                    logger.Error("No such entity to mark");
                    return false;
                }

            }
            catch (Exception ex)
            {
                logger.Error(ex.Message + ":" + ex.InnerException + ":" + ex.StackTrace);
                return false;
            }
        }

        public async Task<bool> Update(Question entity)
        {
            try
            {
                var result = await db.Questions.FirstOrDefaultAsync(b => b.Id.Equals(entity.Id));
                if (result != null)
                {
                    entity.ModifiedAt = DateTime.Now;
                    db.Questions.Update(entity);
                    AnswerRepository ar = new AnswerRepository(db);
                    if (entity.Answers is not null)
                        foreach (Answer answer in entity.Answers)
                        {
                            await ar.Update(answer);
                        }
                    await db.SaveChangesAsync();
                    return true;
                }
                else
                {
                    logger.Error("No such entity to update");
                    return false;
                }

            }
            catch (Exception ex)
            {
                logger.Error(ex.Message + ":" + ex.InnerException + ":" + ex.StackTrace);
                return false;
            }
        }
        public async Task<IList<Question>> GetConditionalType(Dictionary<QuestionType, int> keyValue)
        {
            try
            {

                List<Question> questions = new List<Question>();
                for (int i = 0; i < keyValue.Count; i++)
                {
                    int j = (int)keyValue.Keys.ElementAt(i);
                    questions.AddRange(db.Questions.Include("Answers").Where(x => (int)x.QuestionType == j).Take(keyValue.Values.ElementAt(i)));
                }
                return questions;

            }
            catch (Exception ex)
            {
                logger.Error(ex.Message + ":" + ex.InnerException + ":" + ex.StackTrace);
                return null;
            }
        }
    }
}