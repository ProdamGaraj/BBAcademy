using Backend.Models;
using Backend.Services.Repository.Interfaces;
using NLog;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace Backend.Services.Repository
{
    public class CourseRepository : ICourseRepository
    {
        Logger logger;
        public CourseRepository()
        {
            logger = LogManager.GetCurrentClassLogger();
        }
        public async Task<bool> Add(Course entity)
        {
            try
            {
                using (BBAcademyDb db = new BBAcademyDb())
                {
                    entity.CreatedAt = DateTime.Now;
                    entity.ModifiedAt = DateTime.Now;
                    db.Courses.Add(entity);
                    LessonRepository lr = new LessonRepository();
                    if (entity.Lessons is not null)
                        foreach (Lesson lesson in entity.Lessons)
                        {
                            if (await lr.Get(lesson.Id) is not null)
                            {
                                db.Entry(lesson).State = EntityState.Unchanged;
                            }
                        }
                    ExamRepository er = new ExamRepository();
                    if (entity.Exam is not null)
                        if (await er.Get(entity.Exam.Id) is not null)
                        {
                            db.Entry(entity.Exam).State = EntityState.Unchanged;
                        }
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

        public async Task<Course> Get(long id)
        {
            try
            {
                using (BBAcademyDb db = new BBAcademyDb())
                {
                    Course Course = await db.Courses.Include("Lessons").Include("Exam").FirstOrDefaultAsync(b => b.Id == id && !b.Deleted);
                    return Course;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<IList<Course>> GetAll()
        {
            try
            {
                using (BBAcademyDb db = new BBAcademyDb())
                {
                    IList<Course> myCourse = db.Courses.Include("Lessons").Include("Exam").ToList();
                    return myCourse;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message + ":" + ex.StackTrace);
                return null;
            }
        }

        public async Task<bool> MarkAsDeleted(Course entity)
        {
            try
            {
                using (BBAcademyDb db = new BBAcademyDb())
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
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message + ":" + ex.StackTrace);
                return false;
            }
        }

        public async Task<bool> Update(Course entity)
        {
            try
            {
                using (BBAcademyDb db = new BBAcademyDb())
                {
                    var result = await db.Courses.FirstOrDefaultAsync(b => b.Id.Equals(entity.Id));
                    if (result != null)
                    {
                        entity.ModifiedAt = DateTime.Now;
                        db.Courses.AddOrUpdate(entity);
                        LessonRepository lr = new LessonRepository();
                        if (entity.Lessons is not null)
                            foreach (Lesson lesson in entity.Lessons)
                            {
                                await lr.Update(lesson);
                            }
                        ExamRepository er = new ExamRepository();

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
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message + ":" + ex.StackTrace);
                return false;
            }
        }
    }
}
