﻿using Backend.Models;
using Backend.Services.Repository.Interfaces;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace Backend.Services.Repository
{
    public class CertificateRepository:ICertificateRepository
    {
        public async Task<bool> Add(Certificate entity)
        {
            try
            {
                using (BBAcademyDb db = new BBAcademyDb())
                {
                    entity.CreatedAt = DateTime.Now;
                    entity.ModifiedAt = DateTime.Now;
                    db.Certificates.Add(entity);
                    await db.SaveChangesAsync();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<Certificate> Get(long id)
        {
            try
            {
                using (BBAcademyDb db = new BBAcademyDb())
                {
                    Certificate Certificate = await db.Certificates.Include("Courses").Include("Users").FirstOrDefaultAsync(b => b.Id == id && !b.Deleted);
                    return Certificate;
                }
            }
            catch (Exception ex)
            {
                return new Certificate();
            }
        }

        public async Task<IList<Certificate>> GetAll()
        {
            try
            {
                using (BBAcademyDb db = new BBAcademyDb())
                {
                    IList<Certificate> myCertificate = db.Certificates.Include("Courses").Include("Users").ToList();
                    return myCertificate;
                }
            }
            catch (Exception ex)
            {
                return new List<Certificate>();
            }
        }

        public async Task<bool> MarkAsDeleted(Certificate entity)
        {
            try
            {
                using (BBAcademyDb db = new BBAcademyDb())
                {
                    var result = await db.Certificates.FirstOrDefaultAsync(b => b.Id.Equals(entity.Id));
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

        public async Task<bool> Update(Certificate entity)
        {
            try
            {
                using (BBAcademyDb db = new BBAcademyDb())
                {
                    var result = await db.Certificates.FirstOrDefaultAsync(b => b.Id.Equals(entity.Id));
                    if (result != null)
                    {
                        entity.ModifiedAt = DateTime.Now;
                        db.Certificates.AddOrUpdate(entity);
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