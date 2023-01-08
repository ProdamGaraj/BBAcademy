using Backend.Models;
using Backend.Services.Repository;
using Backend.Services.Repository.Interfaces;
using NLog;

namespace Backend.Services
{
    public class CertificateService
    {
         Logger logger;
        public CertificateService()
        {
            logger = LogManager.GetCurrentClassLogger();
        }
        public async Task<Certificate> CreateCertificate(User user, Course course, bool examPassed)
        {
           
            
            ICertificateRepository cr = new CertificateRepository();

            if (user != null && examPassed && course != null)
            {
                try
                {
                    var certificate = new Certificate()
                    {
                        UserId = user.Id,
                        CourseId = course.Id,
                        Name = "Certificate for " + user.Name + " " + user.LastName + " " + user.SurName + " passed course " + course.Name
                    };
                    user.Certificates.Add(certificate);
                    await cr.Add(certificate);
                    return await cr.Get(certificate.Id);
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message + ":" + ex.InnerException + ":" + ex.StackTrace);
                    return null;
                }
            }
            return new Certificate() { };
        }
    }
}
