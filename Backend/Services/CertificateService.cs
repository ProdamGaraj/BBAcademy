using Backend.Models;
using Backend.Models.Enum;
using Backend.Models.Interfaces;
using Backend.Models.Responce;
using Backend.Services.Repository;
using Backend.Services.Repository.Interfaces;
using NLog;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Backend.Services
{
    public class CertificateService
    {
         Logger logger;
        public CertificateService()
        {
            logger = LogManager.GetCurrentClassLogger();
        }
        public async Task<IBaseResponce<Certificate>> CreateCertificate(User user, Course course, bool examPassed)
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

                    return new BaseResponse<Certificate>() {
                        Data = certificate,
                        Description = certificate.Name + "Got at" + DateTime.Now.ToString(),
                        StatusCode = StatusCode.OK
                    };
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message + ":" + ex.InnerException + ":" + ex.StackTrace);
                    return new BaseResponse<Certificate>()
                    {
                        Description = ex.Message + ":" + ex.InnerException + ":" + ex.StackTrace,
                        StatusCode = StatusCode.OK
                    };
                }
            }
            return new BaseResponse<Certificate>()
            {
                        Description = "Conditions are not passed. Either uaer or course is null, or exam is not passed",
                        StatusCode = StatusCode.InternalServerError
                    }; 
        }
    }
}
