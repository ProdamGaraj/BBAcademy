using Backend.Models;
using Backend.Models.Enum;
using Backend.Models.Interfaces;
using Backend.Models.Responce;
using Backend.Services.AccountService.Interfaces;
using Backend.Services.Repository;
using Backend.Services.Repository.Interfaces;
using Backend.ViewModels;
using NLog;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Backend.Services
{
    public class CertificateService
    {
        Logger logger;
        IAccountService accountService;
        public CertificateService()
        {
            logger = LogManager.GetCurrentClassLogger();
        }
        public async Task<IBaseResponce<Certificate>> CreateCertificate(CourseViewModel vm)
        {
            UserRepository ur = new UserRepository();
            CourseRepository cor = new CourseRepository();
            User user = await ur.Get(vm.User.Id);
            Course course = await cor.Get(vm.IdCourse);
            ICertificateRepository cr = new CertificateRepository();
            if (user != null && course != null)
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

        public async Task<IBaseResponce<List<Certificate>>> GetCertificates(CertificateViewModel vm)
        {
            UserRepository ur = new UserRepository();
            CertificateRepository cr = new CertificateRepository();
            User user = await ur.Get(vm.User.Id);
            List<Certificate> certificates = (await cr.GetAll()).Where(x=>x.UserId==user.Id).ToList();
            if (user is not null&&certificates is not null)
            {
                return new BaseResponse<List<Certificate>>()
                {
                    Data = certificates,
                    Description = "Can`t reach certificate.",
                    StatusCode = StatusCode.InternalServerError
                };
            }
            return new BaseResponse<List<Certificate>>()
            {
                Description = "Can`t reach certificate.",
                StatusCode = StatusCode.InternalServerError
            };
        }
    }
}
