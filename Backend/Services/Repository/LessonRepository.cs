using Backend.Models;
using Backend.Services.Repository.Interfaces;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Data.Entity.Migrations;

namespace Backend.Services.Repository
{
    public class LessonRepository:ILessonRepository
    {
        public void Add(Lesson entity)
        {
            using (BBAcademyDb db = new BBAcademyDb())
            {
                entity.CreatedAt = DateTime.Now;
                entity.ModifiedAt = DateTime.Now;
                db.Lessons.Add(entity);
                db.SaveChanges();
            }
        }

        public Lesson Get(long id)
        {
            using (BBAcademyDb db = new BBAcademyDb())
            {
                Lesson Lesson = db.Lessons.FirstOrDefault(b => b.Id == id && !b.Deleted);
                return Lesson;
            }
        }

        public IList<Lesson> GetAll()
        {
            using (BBAcademyDb db = new BBAcademyDb())
            {
                IList<Lesson> myLesson = db.Lessons.ToList();
                return myLesson;
            }
        }

        public void MarkAsDeleted(Lesson entity)
        {
            using (BBAcademyDb db = new BBAcademyDb())
            {
                var result = db.Lessons.SingleOrDefault(b => b.Id.Equals(entity.Id));
                if (result != null)
                {
                    result.Deleted = true;
                    result.ModifiedAt = DateTime.Now;
                    db.SaveChanges();
                }
            }
        }

        public void Update(Lesson entity)
        {
            using (BBAcademyDb db = new BBAcademyDb())
            {
                var result = db.Lessons.SingleOrDefault(b => b.Id.Equals(entity.Id));
                if (result != null)
                {
                    entity.ModifiedAt = DateTime.Now;
                    db.Lessons.AddOrUpdate(entity);
                    db.SaveChanges();
                }
            }
        }
    }
}
