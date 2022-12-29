using Backend.Models;
using Backend.Services.Repository.Interfaces;
using System.Data.Entity.Migrations;

namespace Backend.Services.Repository
{
    public class ExamRepository:IExamRepository
    {
        public void Add(Exam entity)
        {
            using (BBAcademyDb db = new BBAcademyDb())
            {
                entity.CreatedAt = DateTime.Now;
                entity.ModifiedAt = DateTime.Now;
                db.Exams.Add(entity);
                db.SaveChanges();
            }
        }

        public Exam Get(long id)
        {
            using (BBAcademyDb db = new BBAcademyDb())
            {
                Exam Exam = db.Exams.SingleOrDefault(b => b.Id == id && !b.Deleted);
                return Exam;
            }
        }

        public IList<Exam> GetAll()
        {
            using (BBAcademyDb db = new BBAcademyDb())
            {
                IList<Exam> myExam = db.Exams.ToList();
                return myExam;
            }
        }

        public void MarkAsDeleted(Exam entity)
        {
            using (BBAcademyDb db = new BBAcademyDb())
            {
                var result = db.Exams.SingleOrDefault(b => b.Id.Equals(entity.Id));
                if (result != null)
                {
                    result.Deleted = true;
                    result.ModifiedAt = DateTime.Now;
                    db.SaveChanges();
                }
            }
        }

        public void Update(Exam entity)
        {
            using (BBAcademyDb db = new BBAcademyDb())
            {
                var result = db.Exams.SingleOrDefault(b => b.Id.Equals(entity.Id));
                if (result != null)
                {
                    entity.ModifiedAt = DateTime.Now;
                    db.Exams.AddOrUpdate(entity);
                    db.SaveChanges();
                }
            }
        }
    }
}
