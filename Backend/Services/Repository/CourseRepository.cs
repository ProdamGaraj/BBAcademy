using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models;
using Backend.Services.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Backend.Services.Repository
{
    public class CourseRepository : ICourseRepository
    {
        private readonly BBAcademyDb db;
        private IExamRepository _examRepository;
        private ILessonRepository _lessonRepository;
        private ILogger<CourseRepository> _logger;

        public CourseRepository(BBAcademyDb db, ILogger<CourseRepository> logger, IExamRepository examRepository, ILessonRepository lessonRepository)
        {
            this.db = db;
            _logger = logger;
            _examRepository = examRepository;
            _lessonRepository = lessonRepository;
        }

        public async Task<bool> Add(Course entity)
        {
            try
            {
                entity.CreatedAt = DateTime.Now;
                entity.ModifiedAt = DateTime.Now;
                db.Courses.Add(entity);
                //LessonRepository lr = new LessonRepository(db);
                //if (entity.Lessons is not null)
                //    foreach (Lesson lesson in entity.Lessons)
                //    {
                //        if (lesson.Id != null)
                //            if ((await lr.Get(lesson.Id)).Name is not null)
                //            {
                //                db.Entry(lesson).State = EntityState.Unchanged;
                //            }
                //    }
                //ExamRepository er = new ExamRepository(db);
                //if (entity.Exam is not null)
                //    if (entity.Exam.Id != null && (await er.Get(entity.Exam.Id)).Name is not null)
                //    {
                //        db.Entry(entity.Exam).State = EntityState.Unchanged;
                //    }

                _logger.LogError("Saving changes");
                await db.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ":" + ex.InnerException + ":" + ex.StackTrace);
                return false;
            }
        }

        public async Task<Course> Get(long id)
        {
            try
            {
                Course Course = await db.Courses.Include("Lessons")
                    .Include("Exam")
                    .FirstOrDefaultAsync(b => b.Id == id && !b.Deleted);
                return Course;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ":" + ex.InnerException + ":" + ex.StackTrace);
                return null;
            }
        }

        public async Task<IList<Course>> GetAll()
        {
            try
            {
                IList<Course> myCourse = db.Courses.Include("Lessons")
                    .Include("Exam")
                    .ToList();
                return myCourse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ":" + ex.InnerException + ":" + ex.StackTrace);
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

        public async Task<bool> Update(Course entity)
        {
            try
            {
                var result = await db.Courses.FirstOrDefaultAsync(b => b.Id.Equals(entity.Id));
                if (result != null)
                {
                    entity.ModifiedAt = DateTime.Now;
                    db.Courses.Update(entity);
                    if (entity.Lessons is not null)
                    {
                        foreach (Lesson lesson in entity.Lessons)
                        {
                            await _lessonRepository.Update(lesson);
                        }
                    }

                    if (entity.Exam is not null)
                    {
                        if ((await _examRepository.Get(entity.Exam.Id)) is not null)
                        {
                            await _examRepository.Update(entity.Exam);
                        }
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
    }
}