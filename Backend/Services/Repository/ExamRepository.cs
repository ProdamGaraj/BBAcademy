using Backend.Models;
using Backend.Services.Repository.ICRUD;
using Backend.Services.Repository.Interfaces;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace Backend.Services.Repository
{
    public class ExamRepository:IExamRepository
    {

        public async Task<bool> Add(Exam entity)
        {
            try
            {
                using (BBAcademyDb db = new BBAcademyDb())
                {
                    entity.CreatedAt = DateTime.Now;
                    entity.ModifiedAt = DateTime.Now;
                    db.Exams.Add(entity);
                    await db.SaveChangesAsync();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<Exam> Get(long id)
        {
            try
            {
                using (BBAcademyDb db = new BBAcademyDb())
                {
                    Exam Exam = await db.Exams.Include("ExamToQuestions").FirstOrDefaultAsync(b => b.Id == id && !b.Deleted);
                    return Exam;
                }
            }
            catch (Exception ex)
            {
                return new Exam();
            }
        }

        public async Task<IList<Exam>> GetAll()
        {
            try
            {
                using (BBAcademyDb db = new BBAcademyDb())
                {
                    IList<Exam> myExam = await db.Exams.Include("ExamToQuestions").ToListAsync();
                    return myExam;
                }
            }
            catch (Exception ex)
            {
                return new List<Exam>();
            }
        }

        public async Task<bool> MarkAsDeleted(Exam entity)
        {
            try
            {
                using (BBAcademyDb db = new BBAcademyDb())
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
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> Update(Exam entity)
        {
            try
            {
                using (BBAcademyDb db = new BBAcademyDb())
                {
                    var result = await db.Exams.FirstOrDefaultAsync(b => b.Id.Equals(entity.Id));
                    if (result != null)
                    {
                        entity.ModifiedAt = DateTime.Now;
                        db.Exams.Update(entity);
                        await db.SaveChangesAsync();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
