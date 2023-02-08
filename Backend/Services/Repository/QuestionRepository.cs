using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models;
using Backend.Models.Enum;
using Backend.Services.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Backend.Services.Repository
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly BBAcademyDb db;
        private ILogger<QuestionRepository> _logger;
        private IAnswerRepository _answerRepository;
        
        public QuestionRepository(BBAcademyDb db, ILogger<QuestionRepository> logger, IAnswerRepository answerRepository)
        {
            this.db = db;
            _logger = logger;
            _answerRepository = answerRepository;
        }
        public async Task<bool> Add(Question entity)
        {
            try
            {

                entity.CreatedAt = DateTime.Now;
                entity.ModifiedAt = DateTime.Now;
                db.Questions.Add(entity);
                if (entity.Answers is not null)
                    foreach (Answer lesson in entity.Answers)
                    {
                        if (await _answerRepository.Get(lesson.Id) is not null)
                        {
                            db.Entry(lesson).State = EntityState.Unchanged;
                        }
                    }
                await db.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ":" + ex.InnerException + ":" + ex.StackTrace);
                return false;
            }
        }

        public async Task<bool> AddRange(ICollection<Question> entity)
        {
            try
            {
                foreach (var item in entity)
                {
                    item.CreatedAt = DateTime.Now;
                    item.ModifiedAt = DateTime.Now;
                }
                db.Questions.AddRange(entity);
                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ":" + ex.InnerException + ":" + ex.StackTrace);
                return false;
            }
        }

        public async Task<Question> Get(long id)
        {
            try
            {
                Question question = await db.Questions.Include("Answers").FirstOrDefaultAsync(b => b.Id == id && !b.Deleted);
                return question;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ":" + ex.InnerException + ":" + ex.StackTrace);
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
                _logger.LogError(ex.Message + ":" + ex.InnerException + ":" + ex.StackTrace);
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
                    _logger.LogError("No such entity to mark");
                    return false;
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ":" + ex.InnerException + ":" + ex.StackTrace);
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
                    if (entity.Answers is not null)
                        foreach (Answer answer in entity.Answers)
                        {
                            await _answerRepository.Update(answer);
                        }
                    await db.SaveChangesAsync();
                    return true;
                }
                else
                {
                    _logger.LogError("No such entity to update");
                    return false;
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ":" + ex.InnerException + ":" + ex.StackTrace);
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
                _logger.LogError(ex.Message + ":" + ex.InnerException + ":" + ex.StackTrace);
                return null;
            }
        }
    }
}