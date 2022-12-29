using Backend.Models;
using Backend.Services.Repository.Interfaces;
using System.Data.Entity.Migrations;

namespace Backend.Services.Repository
{
    public class CourseRepository:ICourseRepository
    {
        public void Add(Course entity)
        {
            using (BBAcademyDb db = new BBAcademyDb())
            {
                entity.CreatedAt = DateTime.Now;
                entity.ModifiedAt = DateTime.Now;
                db.Courses.Add(entity);
                db.SaveChanges();
            }
        }

        public Course Get(long id)
        {
            using (BBAcademyDb db = new BBAcademyDb())
            {
                Course Course = db.Courses.SingleOrDefault(b => b.Id == id && !b.Deleted);
                return Course;
            }
        }

        public IList<Course> GetAll()
        {
            using (BBAcademyDb db = new BBAcademyDb())
            {
                IList<Course> myCourse = db.Courses.ToList();
                return myCourse;
            }
        }

        public void MarkAsDeleted(Course entity)
        {
            using (BBAcademyDb db = new BBAcademyDb())
            {
                var result = db.Courses.SingleOrDefault(b => b.Id.Equals(entity.Id));
                if (result != null)
                {
                    result.Deleted = true;
                    result.ModifiedAt = DateTime.Now;
                    db.SaveChanges();
                }
            }
        }

        public void Update(Course entity)
        {
            using (BBAcademyDb db = new BBAcademyDb())
            {
                var result = db.Courses.SingleOrDefault(b => b.Id.Equals(entity.Id));
                if (result != null)
                {
                    entity.ModifiedAt = DateTime.Now;
                    db.Courses.AddOrUpdate(entity);
                    db.SaveChanges();
                }
            }
        }
    }
}
