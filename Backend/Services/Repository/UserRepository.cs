using Backend.Models;
using Backend.Services.Repository.Interfaces;
using System.Data.Entity.Migrations;

namespace Backend.Services.Repository
{
    public class UserRepository:IUserRepository
    {
        public void Add(User entity)
        {
            using (BBAcademyDb db = new BBAcademyDb())
            {
                entity.CreatedAt = DateTime.Now;
                entity.ModifiedAt = DateTime.Now;
                db.Users.Add(entity);
                db.SaveChanges();
            }
        }
        public User Get(long id)
        {
            using (BBAcademyDb db = new BBAcademyDb())
            {
                User User = db.Users.SingleOrDefault(b => b.Id==id && !b.Deleted);
                return User;
            }
        }


        public User Get(string id)
        {
            using (BBAcademyDb db = new BBAcademyDb())
            {
                User User = db.Users.SingleOrDefault(b => b.Id.Equals(id) && !b.Deleted);
                return User;
            }
        }

        public IList<User> GetAll()
        {
            using (BBAcademyDb db = new BBAcademyDb())
            {
                IList<User> myUser = db.Users.ToList();
                return myUser;
            }
        }

        public void MarkAsDeleted(User entity)
        {
            using (BBAcademyDb db = new BBAcademyDb())
            {
                var result = db.Users.SingleOrDefault(b => b.Id.Equals(entity.Id));
                if (result != null)
                {
                    result.Deleted = true;
                    result.ModifiedAt = DateTime.Now;
                    db.SaveChanges();
                }
            }
        }

        public void Update(User entity)
        {
            using (BBAcademyDb db = new BBAcademyDb())
            {
                var result = db.Users.SingleOrDefault(b => b.Id.Equals(entity.Id));
                if (result != null)
                {
                    entity.ModifiedAt = DateTime.Now;
                    db.Users.AddOrUpdate(entity);
                    db.SaveChanges();
                }
            }
        }
    }
}
