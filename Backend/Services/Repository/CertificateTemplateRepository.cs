using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Models;
using Backend.Services.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Backend.Services.Repository
{
    public class CertificateTemplateRepository : ICertificateTemplateRepository
    {
        private readonly BBAcademyDb db;
        private ILogger<CertificateTemplateRepository> _logger;
        private ICourseRepository _courseRepository;
        public CertificateTemplateRepository(BBAcademyDb db, ILogger<CertificateTemplateRepository> logger, ICourseRepository courseRepository)
        {
            this.db = db;
            _logger = logger;
            _courseRepository = courseRepository;
        }
        public async Task<bool> Add(CertificateTemplate entity)
        {
            try
            {
                entity.CreatedAt = DateTime.Now;
                entity.ModifiedAt = DateTime.Now;
                if (entity.Courses is not null)
                    foreach (Course course in entity.Courses)
                    {
                        if (await _courseRepository.Get(course.Id) is not null)
                        {
                            db.Entry(course).State = EntityState.Unchanged;
                        }
                    }
                db.CertificateTemplates.Add(entity);
                await db.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ":" + ex.InnerException + ":" + ex.StackTrace);
                return false;
            }
        }

        public async Task<CertificateTemplate> Get(long id)
        {
            try
            {

                CertificateTemplate CertificateTemplate = await db.CertificateTemplates.Include("Courses").FirstOrDefaultAsync(b => b.Id == id && !b.Deleted);
                return CertificateTemplate;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ":" + ex.InnerException + ":" + ex.StackTrace);
                return null;
            }
        }

        public async Task<IList<CertificateTemplate>> GetAll()
        {
            try
            {

                IList<CertificateTemplate> CertificateTemplates = await db.CertificateTemplates.Include("Courses").ToListAsync();
                return CertificateTemplates;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ":" + ex.InnerException + ":" + ex.StackTrace);
                return null;
            }
        }

        public async Task<bool> MarkAsDeleted(CertificateTemplate entity)
        {
            try
            {

                var result = await db.CertificateTemplates.FirstOrDefaultAsync(b => b.Id.Equals(entity.Id));
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

        public async Task<bool> Update(CertificateTemplate entity)
        {
            try
            {

                var result = await db.CertificateTemplates.FirstOrDefaultAsync(b => b.Id.Equals(entity.Id));
                if (result != null)
                {
                    entity.ModifiedAt = DateTime.Now;
                    db.CertificateTemplates.Update(entity);
                    if (entity.Courses is not null)
                        foreach (Course course in entity.Courses)
                        {
                            await _courseRepository.Update(course);
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
