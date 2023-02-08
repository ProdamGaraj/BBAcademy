using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Models;
using Backend.Services.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Backend.Services.Repository
{
    public class ExamRepository : IExamRepository
    {
        private readonly BBAcademyDb db;
        private readonly IQuestionRepository _questionRepository;
        private ILogger<ExamRepository> _logger;
        public ExamRepository(BBAcademyDb db, IQuestionRepository questionRepository, ILogger<ExamRepository> logger)
        {
            this.db = db;
            _questionRepository = questionRepository;
            _logger = logger;
        }
        public async Task<bool> Add(Exam entity)
        {
            try
            {
                entity.CreatedAt = DateTime.Now;
                entity.ModifiedAt = DateTime.Now;
                db.Exams.Add(entity);
                if (entity.Questions is not null)
                    foreach (Question question in entity.Questions)
                    {
                        if (await _questionRepository.Get(question.Id) is not null)
                        {
                            db.Entry(question).State = EntityState.Unchanged;
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

        public async Task<Exam> Get(long id)
        {
            try
            {

                Exam Exam = await db.Exams.Include("Questions").FirstAsync(b => b.Id == id && !b.Deleted);
                return Exam;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ":" + ex.InnerException + ":" + ex.StackTrace);
                return new Exam();
            }
        }

        public async Task<IList<Exam>> GetAll()
        {
            try
            {

                IList<Exam> myExam = await db.Exams.Include("Questions").ToListAsync();
                return myExam;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ":" + ex.InnerException + ":" + ex.StackTrace);
                return new List<Exam>();
            }
        }

        public async Task<bool> MarkAsDeleted(Exam entity)
        {
            try
            {

                var result = await db.Exams.FirstOrDefaultAsync(b => b.Id.Equals(entity.Id));
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

        public async Task<bool> Update(Exam entity)
        {
            try
            {

                var result = await db.Exams.FirstOrDefaultAsync(b => b.Id.Equals(entity.Id));
                if (result != null)
                {
                    entity.ModifiedAt = DateTime.Now;
                    db.Exams.Update(entity);

                    if (entity.Questions is not null)
                        foreach (Question question in entity.Questions)
                        {
                            await _questionRepository.Update(question);
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