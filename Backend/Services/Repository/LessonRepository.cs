using Backend.Models;
using Backend.Services.Repository.Interfaces;
using NLog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Backend.Services.Repository
{
    public class LessonRepository : ILessonRepository
    {
        private readonly BBAcademyDb db;
        Logger logger;
        public LessonRepository(BBAcademyDb db)
        {
            logger = LogManager.GetCurrentClassLogger();
            this.db = db;
        }
        public async Task<bool> Add(Lesson entity)
        {
            try
            {

                entity.CreatedAt = DateTime.Now;
                entity.ModifiedAt = DateTime.Now;
                db.Lessons.Add(entity);
                await db.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message + ":" + ex.InnerException + ":" + ex.StackTrace);
                return false;
            }
        }

        public async Task<Lesson> Get(long id)
        {
            try
            {

                Lesson Lesson = await db.Lessons.FirstOrDefaultAsync(b => b.Id == id && !b.Deleted);
                return Lesson;

            }
            catch (Exception ex)
            {
                logger.Error(ex.Message + ":" + ex.InnerException + ":" + ex.StackTrace);
                return new Lesson();
            }
        }

        public async Task<IList<Lesson>> GetAll()
        {
            try
            {

                IList<Lesson> myLesson = await db.Lessons.ToListAsync();
                return myLesson;

            }
            catch (Exception ex)
            {
                logger.Error(ex.Message + ":" + ex.InnerException + ":" + ex.StackTrace);
                return new List<Lesson>();
            }
        }

        public async Task<bool> MarkAsDeleted(Lesson entity)
        {
            try
            {

                var result = await db.Lessons.FirstOrDefaultAsync(b => b.Id.Equals(entity.Id));
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

        public async Task<bool> Update(Lesson entity)
        {
            try
            {

                var result = await db.Lessons.FirstOrDefaultAsync(b => b.Id.Equals(entity.Id));
                if (result != null)
                {
                    entity.ModifiedAt = DateTime.Now;
                    db.Lessons.Update(entity);
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
    }
}
