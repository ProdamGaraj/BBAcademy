using Backend.Models;
using Backend.Services.Repository.Interfaces;
using System.Data.Entity.Migrations;

namespace Backend.Services.Repository
{
    public class CertificateRepository:ICertificateRepository
    {
        public void Add(Certificate entity)
        {
            using (BBAcademyDb db = new BBAcademyDb())
            {
                entity.CreatedAt = DateTime.Now;
                entity.ModifiedAt = DateTime.Now;
                db.Certificates.Add(entity);
                db.SaveChanges();
            }
        }

        public Certificate Get(long id)
        {
            using (BBAcademyDb db = new BBAcademyDb())
            {
                Certificate Certificate = db.Certificates.SingleOrDefault(b => b.Id == id && !b.Deleted);
                return Certificate;
            }
        }

        public IList<Certificate> GetAll()
        {
            using (BBAcademyDb db = new BBAcademyDb())
            {
                IList<Certificate> myCertificate = db.Certificates.ToList();
                return myCertificate;
            }
        }

        public void MarkAsDeleted(Certificate entity)
        {
            using (BBAcademyDb db = new BBAcademyDb())
            {
                var result = db.Certificates.SingleOrDefault(b => b.Id.Equals(entity.Id));
                if (result != null)
                {
                    result.Deleted = true;
                    result.ModifiedAt = DateTime.Now;
                    db.SaveChanges();
                }
            }
        }

        public void Update(Certificate entity)
        {
            using (BBAcademyDb db = new BBAcademyDb())
            {
                var result = db.Certificates.SingleOrDefault(b => b.Id.Equals(entity.Id));
                if (result != null)
                {
                    entity.ModifiedAt = DateTime.Now;
                    db.Certificates.AddOrUpdate(entity);
                    db.SaveChanges();
                }
            }
        }
    }
}
