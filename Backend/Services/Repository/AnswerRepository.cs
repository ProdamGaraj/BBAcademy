﻿using Backend.Models;
using Backend.Services.Repository.Interfaces;
using System.Data.Entity.Migrations;

namespace Backend.Services.Repository
{
    public class AnswerRepository : IAnswerRepository
    {
        public void Add(Answer entity)
        {
            using (BBAcademyDb db = new BBAcademyDb())
            {
                entity.CreatedAt = DateTime.Now;
                entity.ModifiedAt = DateTime.Now;
                db.Answers.Add(entity);
                db.SaveChanges();
            }
        }

        public Answer Get(long id)
        {
            using (BBAcademyDb db = new BBAcademyDb())
            {
                Answer Answer = db.Answers.SingleOrDefault(b => b.Id==id && !b.Deleted);
                return Answer;
            }
        }

        public IList<Answer> GetAll()
        {
            using (BBAcademyDb db = new BBAcademyDb())
            {
                IList<Answer> myAnswer = db.Answers.ToList();
                return myAnswer;
            }
        }

        public void MarkAsDeleted(Answer entity)
        {
            using (BBAcademyDb db = new BBAcademyDb())
            {
                var result = db.Answers.SingleOrDefault(b => b.Id.Equals(entity.Id));
                if (result != null)
                {
                    result.Deleted = true;
                    result.ModifiedAt = DateTime.Now;
                    db.SaveChanges();
                }
            }
        }

        public void Update(Answer entity)
        {
            using (BBAcademyDb db = new BBAcademyDb())
            {
                var result = db.Answers.SingleOrDefault(b => b.Id.Equals(entity.Id));
                if (result != null)
                {
                    entity.ModifiedAt = DateTime.Now;
                    db.Answers.AddOrUpdate(entity);
                    db.SaveChanges();
                }
            }
        }
    }
}
