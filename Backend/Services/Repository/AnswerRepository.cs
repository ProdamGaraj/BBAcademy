using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Models;
using Backend.Services.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Backend.Services.Repository
{
    public class AnswerRepository : IAnswerRepository
    {
        private readonly BBAcademyDb db;
        private ILogger<AnswerRepository> _logger;
        public AnswerRepository(BBAcademyDb db, ILogger<AnswerRepository> logger)
        {
            this.db = db;
            _logger = logger;
        }
        public async Task<bool> Add(Answer entity)
        {
            try
            {

                entity.CreatedAt = DateTime.Now;
                entity.ModifiedAt = DateTime.Now;
                db.Answers.Add(entity);
                await db.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ":" + ex.InnerException + ":" + ex.StackTrace);
                return false;
            }
        }

        public async Task<bool> AddRange(ICollection<Answer> entity)
        {
            try
            {
                foreach (var item in entity)
                {
                    item.CreatedAt = DateTime.Now;
                    item.ModifiedAt = DateTime.Now;
                }
                db.Answers.AddRange(entity);
                await db.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ":" + ex.InnerException + ":" + ex.StackTrace);
                return false;
            }
        }

        public async Task<Answer> Get(long id)
        {
            try
            {

                Answer Answer = await db.Answers.FirstOrDefaultAsync(b => b.Id == id && !b.Deleted);
                return Answer;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ":" + ex.InnerException + ":" + ex.StackTrace);
                return null;
            }
        }

        public async Task<IList<Answer>> GetAll()
        {
            try
            {

                IList<Answer> myAnswer = await db.Answers.ToListAsync();
                return myAnswer;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ":" + ex.InnerException + ":" + ex.StackTrace);
                return null;
            }
        }

        public async Task<bool> MarkAsDeleted(Answer entity)
        {
            try
            {

                var result = await db.Answers.FirstOrDefaultAsync(b => b.Id.Equals(entity.Id));
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

        public async Task<bool> Update(Answer entity)
        {
            try
            {

                var result = await db.Answers.FirstOrDefaultAsync(b => b.Id.Equals(entity.Id));
                if (result != null)
                {
                    entity.ModifiedAt = DateTime.Now;
                    db.Answers.Update(entity);
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
    }
}
