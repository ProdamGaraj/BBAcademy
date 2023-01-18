using Backend.Models;
using Backend.Services.Repository.Interfaces;
using NLog;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace Backend.Services.Repository
{
    public class UserRepository : IUserRepository
    {

        Logger logger;
        public UserRepository()
        {
            logger = LogManager.GetCurrentClassLogger();
        }
        public async Task<bool> Add(User entity)
        {
            try
            {
                using (BBAcademyDb db = new BBAcademyDb())
                {
                    entity.CreatedAt = DateTime.Now;
                    entity.ModifiedAt = DateTime.Now;
                    db.Users.Add(entity);

                    CertificateRepository cr = new CertificateRepository();
                    if (entity.Certificates is not null)
                        foreach (Certificate certificate in entity.Certificates)
                        {
                            if (cr.Get(certificate.Id) is not null)
                            {
                                db.Entry(certificate).State = EntityState.Unchanged;
                            }
                        }
                    LessonRepository lr = new LessonRepository();
                    if (entity.SolvedLessons is not null)
                        foreach (Lesson lesson in entity.SolvedLessons)
                        {
                            if (lr.Get(lesson.Id) is not null)
                            {
                                db.Entry(lesson).State = EntityState.Unchanged;
                            }
                        }
                    await db.SaveChangesAsync();
                }
                return true;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message + ":" + ex.InnerException + ":" + ex.StackTrace);
                return false;
            }
        }

        public async Task<User> Get(long id)
        {
            try
            {
                using (BBAcademyDb db = new BBAcademyDb())
                {
                    User User = await db.Users.Include("Certificates").Include("SolvedLessons").FirstOrDefaultAsync(b => b.Id == id && !b.Deleted);
                    return User;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message + ":" + ex.InnerException + ":" + ex.StackTrace);
                return null;
            }
        }

        public async Task<IList<User>> GetAll()
        {
            try
            {
                using (BBAcademyDb db = new BBAcademyDb())
                {
                    IList<User> myUser = await db.Users.Include("Certificates").Include("SolvedLessons").ToListAsync();
                    return myUser;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message + ":" + ex.InnerException + ":" + ex.StackTrace);
                return null;
            }
        }
        public async Task<bool> MarkAsDeleted(User entity)
        {
            try
            {
                using (BBAcademyDb db = new BBAcademyDb())
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
                        logger.Error("No such entity to mark");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message + ":" + ex.InnerException + ":" + ex.StackTrace);
                return false;
            }
        }

        public async Task<bool> Update(User entity)
        {
            try
            {
                using (BBAcademyDb db = new BBAcademyDb())
                {
                    var result = await db.Users.FirstOrDefaultAsync(b => b.Id.Equals(entity.Id));
                    if (result != null)
                    {
                        entity.ModifiedAt = DateTime.Now;
                        db.Users.AddOrUpdate(entity);

                        CertificateRepository cr = new CertificateRepository();
                        if (entity.Certificates is not null)
                            foreach (Certificate certificate in entity.Certificates)
                            {
                                await cr.Update(certificate);
                            }


                        LessonRepository lr = new LessonRepository();
                        if (entity.SolvedLessons is not null)
                            foreach (Lesson lesson in entity.SolvedLessons)
                            {
                                await lr.Update(lesson);
                            }
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
                logger.Error(ex.Message + ":" + ex.InnerException + ":" + ex.StackTrace);
                return false;
            }
        }
    }
}