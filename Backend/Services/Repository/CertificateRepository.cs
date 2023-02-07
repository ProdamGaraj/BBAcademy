using Backend.Models;
using Backend.Services.Repository.Interfaces;
using NLog;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services.Repository
{
    public class CertificateRepository : ICertificateRepository
    {
        private readonly BBAcademyDb db;
        Logger logger;
        public CertificateRepository(BBAcademyDb db)
        {
            logger = LogManager.GetCurrentClassLogger();
            this.db = db;
        }
        public async Task<bool> Add(Certificate entity)
        {
            try
            {

                entity.CreatedAt = DateTime.Now;
                entity.ModifiedAt = DateTime.Now;
                db.Certificates.Add(entity);
                CourseRepository cr = new CourseRepository(db);
                if (entity.CertificateTemplate.Courses is not null)
                    foreach (Course course in entity.CertificateTemplate.Courses)
                    {
                        if (await cr.Get(course.Id) is not null)
                        {
                            db.Entry(course).State = EntityState.Unchanged;
                        }
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

        public async Task<Certificate> Get(long id)
        {
            try
            {

                Certificate Certificate = await db.Certificates.FirstOrDefaultAsync(b => b.Id == id && !b.Deleted);
                return Certificate;

            }
            catch (Exception ex)
            {
                logger.Error(ex.Message + ":" + ex.InnerException + ":" + ex.StackTrace);
                return null;
            }
        }

        public async Task<IList<Certificate>> GetAll()
        {
            try
            {

                IList<Certificate> myCertificate = await db.Certificates.ToListAsync();
                return myCertificate;

            }
            catch (Exception ex)
            {
                logger.Error(ex.Message + ":" + ex.InnerException + ":" + ex.StackTrace);
                return null;
            }
        }
        public async Task<bool> MarkAsDeleted(Certificate entity)
        {
            try
            {

                var result = await db.Certificates.FirstOrDefaultAsync(b => b.Id.Equals(entity.Id));
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

        public async Task<bool> Update(Certificate entity)
        {
            try
            {

                var result = await db.Certificates.FirstOrDefaultAsync(b => b.Id.Equals(entity.Id));
                if (result != null)
                {
                    entity.ModifiedAt = DateTime.Now;
                    db.Certificates.Update(entity);
                    CourseRepository cr = new CourseRepository(db);
                    if (entity.CertificateTemplate.Courses is not null)
                        foreach (Course course in entity.CertificateTemplate.Courses)
                        {
                            await cr.Update(course);
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