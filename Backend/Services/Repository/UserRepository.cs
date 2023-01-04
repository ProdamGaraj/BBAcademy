﻿using Backend.Models;
using Backend.Services.Repository.Interfaces;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace Backend.Services.Repository
{
    public class UserRepository:IUserRepository
    {
        public async Task<bool> Add(User entity)
        {
            try
            {
                using (BBAcademyDb db = new BBAcademyDb())
                {
                    entity.CreatedAt = DateTime.Now;
                    entity.ModifiedAt = DateTime.Now;
                    db.Users.Add(entity);
                    await db.SaveChangesAsync();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<User> Get(long id)
        {
            try
            {
                using (BBAcademyDb db = new BBAcademyDb())
                {
                    User User = await db.Users.Include("UserToCertificates").Include("UserToCourses").Include("UserToLessons").FirstOrDefaultAsync(b => b.Id == id && !b.Deleted);
                    return User;
                }
            }
            catch (Exception ex)
            {
                return new User();
            }
        }

        public async Task<IList<User>> GetAll()
        {
            try
            {
                using (BBAcademyDb db = new BBAcademyDb())
                {
                    IList<User> myUser = await db.Users.Include("UserToCertificates").Include("UserToCourses").Include("UserToLessons").ToListAsync();
                    return myUser;
                }
            }
            catch (Exception ex)
            {
                return new List<User>();
            }
        }
        public async Task<User> GetWithoutContext(long id)
        {
            try
            {
                using (BBAcademyDb db = new BBAcademyDb())
                {
                    User User = await db.Users.FirstOrDefaultAsync(b => b.Id == id && !b.Deleted);
                    return User;
                }
            }
            catch (Exception ex)
            {
                return new User();
            }
        }

        public async Task<IList<User>> GetAllWithoutContext()
        {
            try
            {
                using (BBAcademyDb db = new BBAcademyDb())
                {
                    IList<User> myUser = await db.Users.ToListAsync();
                    return myUser;
                }
            }
            catch (Exception ex)
            {
                return new List<User>();
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
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
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
                        db.Users.Update(entity);
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
