using Backend.Models;
using Backend.Services.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using NLog;
using System.Data.Entity.Migrations;

namespace Backend.Services.Repository
{
    public class AnswerRepository : IAnswerRepository
    {
        Logger logger;
        public AnswerRepository()
        {
            logger = LogManager.GetCurrentClassLogger();
        }
        public async Task<bool> Add(Answer entity)
        {
            try
            {
                using (BBAcademyDb db = new BBAcademyDb())
                {
                    entity.CreatedAt = DateTime.Now;
                    entity.ModifiedAt = DateTime.Now;
                    db.Answers.Add(entity);
                    await db.SaveChangesAsync();
                }
                return true;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message + ":" + ex.StackTrace);
                return false;
            }
        }

        public async Task<Answer> Get(long id)
        {
            try
            {
                using (BBAcademyDb db = new BBAcademyDb())
                {
                    Answer Answer = await db.Answers.FirstOrDefaultAsync(b => b.Id == id && !b.Deleted);
                    return Answer;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message + ":" + ex.StackTrace);
                return new Answer();
            }
        }

        public async Task<IList<Answer>> GetAll()
        {
            try
            {
                using (BBAcademyDb db = new BBAcademyDb())
                {
                    IList<Answer> myAnswer = await db.Answers.ToListAsync();
                    return myAnswer;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message + ":" + ex.StackTrace);
                return new List<Answer>();
            }
        }

        public async Task<bool> MarkAsDeleted(Answer entity)
        {
            try
            {
                using (BBAcademyDb db = new BBAcademyDb())
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
                        logger.Error("No such entity to mark");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message + ":" + ex.StackTrace);
                return false;
            }
        }

        public async Task<bool> Update(Answer entity)
        {
            try
            {
                using (BBAcademyDb db = new BBAcademyDb())
                {
                    var result = await db.Answers.FirstOrDefaultAsync(b => b.Id.Equals(entity.Id));
                    if (result != null)
                    {
                        entity.ModifiedAt = DateTime.Now;
                        db.Answers.AddOrUpdate(entity);
                        await db.SaveChangesAsync();
                        return true;
                    }
                    else
                    {
                        logger.Error("No such entity to update");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message + ":" + ex.StackTrace);
                return false;
            }
        }
    }
}
