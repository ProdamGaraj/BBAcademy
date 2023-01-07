using Backend.Models;
using Backend.Services.Repository.Interfaces;
using NLog;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace Backend.Services.Repository
{
    public class CourseRepository:ICourseRepository
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
                    Course Course = await db.Courses.Include("CourseToExams").Include("CourseToLessons").FirstOrDefaultAsync(b => b.Id == id && !b.Deleted);
                    return Course;
                }
            }
            catch (Exception ex)
            {
                return new Course();
            }
        }

        public async Task<IList<Course>> GetAll()
        {
            try
            {
                using (BBAcademyDb db = new BBAcademyDb())
                {
                    IList<Course> myCourse = db.Courses.Include("CourseToExams").Include("CourseToLessons").ToList();
                    return myCourse;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message + ":" + ex.StackTrace);
                return new List<Course>();
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
