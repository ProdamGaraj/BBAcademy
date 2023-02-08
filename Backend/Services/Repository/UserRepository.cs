using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Models;
using Backend.Services.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Backend.Services.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly BBAcademyDb db;
        private ILogger<UserRepository> _logger;
        private ICertificateRepository _certificateRepository;
        private ILessonRepository _lessonRepository;
        public UserRepository(BBAcademyDb db, ILogger<UserRepository> logger, ICertificateRepository certificateRepository, ILessonRepository lessonRepository)
        {
            this.db = db;
            _logger = logger;
            _certificateRepository = certificateRepository;
            _lessonRepository = lessonRepository;
        }
        public async Task<bool> Add(User entity)
        {
            try
            {

                entity.CreatedAt = DateTime.Now;
                entity.ModifiedAt = DateTime.Now;
                db.Users.Add(entity);

                if (entity.Certificates is not null)
                    foreach (Certificate certificate in entity.Certificates)
                    {
                        if (_certificateRepository.Get(certificate.Id) is not null)
                        {
                            db.Entry(certificate).State = EntityState.Unchanged;
                        }
                    }
                if (entity.SolvedLessons is not null)
                    foreach (Lesson lesson in entity.SolvedLessons)
                    {
                        if (_lessonRepository.Get(lesson.Id) is not null)
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

        public async Task<User> Get(long id)
        {
            try
            {

                User User = await db.Users.Include("Certificates").Include("SolvedLessons").FirstOrDefaultAsync(b => b.Id == id && !b.Deleted);
                return User;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ":" + ex.InnerException + ":" + ex.StackTrace);
                return null;
            }
        }

        public async Task<IList<User>> GetAll()
        {
            try
            {

                IList<User> myUser = await db.Users.Include("Certificates").Include("SolvedLessons").ToListAsync();
                return myUser;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ":" + ex.InnerException + ":" + ex.StackTrace);
                return null;
            }
        }
        public async Task<bool> MarkAsDeleted(User entity)
        {
            try
            {

                var result = await db.Users.FirstOrDefaultAsync(b => b.Id.Equals(entity.Id));
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

        public async Task<bool> Update(User entity)
        {
            try
            {

                var result = await db.Users.FirstOrDefaultAsync(b => b.Id.Equals(entity.Id));
                if (result != null)
                {
                    entity.ModifiedAt = DateTime.Now;
                    db.Users.Update(entity);

                    if (entity.Certificates is not null)
                        foreach (Certificate certificate in entity.Certificates)
                        {
                            await _certificateRepository.Update(certificate);
                        }


                    if (entity.SolvedLessons is not null)
                        foreach (Lesson lesson in entity.SolvedLessons)
                        {
                            await _lessonRepository.Update(lesson);
                        }
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
    }
}