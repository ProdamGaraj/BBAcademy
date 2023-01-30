using Backend.Models;
using Backend.Services.Repository.Interfaces;
using NLog;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services.Repository
{
    public class CourseRepository : ICourseRepository
    {
        private readonly BBAcademyDb db;
        Logger logger;
        public CourseRepository(BBAcademyDb db)
        {
            logger = LogManager.GetCurrentClassLogger();
            this.db = db;
        }
        public async Task<bool> Add(Course entity)
        {
            try
            {

                entity.CreatedAt = DateTime.Now;
                entity.ModifiedAt = DateTime.Now;
                db.Courses.Add(entity);
                LessonRepository lr = new LessonRepository(db);
                if (entity.Lessons is not null)
                    foreach (Lesson lesson in entity.Lessons)
                    {
                        if (await lr.Get(lesson.Id) is not null)
                        {
                            db.Entry(lesson).State = EntityState.Unchanged;
                        }
                    }
                ExamRepository er = new ExamRepository(db);
                if (entity.Exam is not null)
                    if (await er.Get(entity.Exam.Id) is not null)
                    {
                        db.Entry(entity.Exam).State = EntityState.Unchanged;
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

        public async Task<Course> Get(long id)
        {
            try
            {

                Course Course = await db.Courses.Include("Lessons").Include("Exam").FirstOrDefaultAsync(b => b.Id == id && !b.Deleted);
                return Course;

            }
            catch (Exception ex)
            {
                logger.Error(ex.Message + ":" + ex.InnerException + ":" + ex.StackTrace);
                return null;
            }
        }

        public async Task<IList<Course>> GetAll()
        {
            try
            {

                IList<Course> myCourse = db.Courses.Include("Lessons").Include("Exam").ToList();
                return myCourse;

            }
            catch (Exception ex)
            {
                logger.Error(ex.Message + ":" + ex.InnerException + ":" + ex.StackTrace);
                return null;
            }
        }

        public async Task<bool> MarkAsDeleted(Course entity)
        {
            try
            {

                var result = await db.Courses.FirstOrDefaultAsync(b => b.Id.Equals(entity.Id));
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

        public async Task<bool> Update(Course entity)
        {
            try
            {

                var result = await db.Courses.FirstOrDefaultAsync(b => b.Id.Equals(entity.Id));
                if (result != null)
                {
                    entity.ModifiedAt = DateTime.Now;
                    db.Courses.Update(entity);
                    LessonRepository lr = new LessonRepository(db);
                    if (entity.Lessons is not null)
                        foreach (Lesson lesson in entity.Lessons)
                        {
                            await lr.Update(lesson);
                        }
                    ExamRepository er = new ExamRepository(db);

                    if (entity.Exam is not null)
                        if (await er.Get(entity.Exam.Id) is not null)
                        {
                            await er.Update(entity.Exam);
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
    }
}
