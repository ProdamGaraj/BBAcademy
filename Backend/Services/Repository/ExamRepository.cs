using Backend.Models;
using Backend.Services.Repository.Interfaces;
using NLog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Backend.Services.Repository
{
    public class ExamRepository : IExamRepository
    {
        private readonly BBAcademyDb db;
        Logger logger;
        public ExamRepository(BBAcademyDb db)
        {
            logger = LogManager.GetCurrentClassLogger();
            this.db = db;
        }
        public async Task<bool> Add(Exam entity)
        {
            try
            {

                entity.CreatedAt = DateTime.Now;
                entity.ModifiedAt = DateTime.Now;
                db.Exams.Add(entity);
                QuestionRepository qr = new QuestionRepository(db);
                if (entity.Questions is not null)
                    foreach (Question question in entity.Questions)
                    {
                        if (await qr.Get(question.Id) is not null)
                        {
                            db.Entry(question).State = EntityState.Unchanged;
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

        public async Task<Exam> Get(long id)
        {
            try
            {

                Exam Exam = await db.Exams.Include("Questions").FirstOrDefaultAsync(b => b.Id == id && !b.Deleted);
                return Exam;

            }
            catch (Exception ex)
            {
                logger.Error(ex.Message + ":" + ex.InnerException + ":" + ex.StackTrace);
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
                logger.Error(ex.Message + ":" + ex.InnerException + ":" + ex.StackTrace);
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

        public async Task<bool> Update(Exam entity)
        {
            try
            {

                var result = await db.Exams.FirstOrDefaultAsync(b => b.Id.Equals(entity.Id));
                if (result != null)
                {
                    entity.ModifiedAt = DateTime.Now;
                    db.Exams.Update(entity);

                    QuestionRepository qr = new QuestionRepository(db);
                    if (entity.Questions is not null)
                        foreach (Question question in entity.Questions)
                        {
                            await qr.Update(question);
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