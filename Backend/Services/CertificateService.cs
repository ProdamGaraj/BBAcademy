using Backend.Models;
using Backend.Models.Enum;
using Backend.Models.Interfaces;
using Backend.Models.Responce;
using Backend.Services.AccountService.Interfaces;
using Backend.Services.Interfaces;
using Backend.Services.Repository;
using Backend.Services.Repository.Interfaces;
using Backend.ViewModels;
using NLog;
using System.Reflection;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Backend.Services
{
    public class CertificateService:ICertificateService
    {
        Logger logger = LogManager.GetCurrentClassLogger();
        private IAccountService accountService;
        private ICertificateRepository certificateRepository;
        private IUserRepository userRepository;
        private ICourseRepository courseRepository;
        private ICertificateTemplateRepository certificateTemplateRepository;

        public CertificateService(IAccountService _accountService, ICertificateRepository _cr, IUserRepository _ur, ICourseRepository _cor)
        {
            accountService = _accountService;
            certificateRepository = _cr;
            userRepository = _ur;
            courseRepository = _cor;
        }
        public async Task<IBaseResponce<Certificate>> CreateCertificate(CourseViewModel vm)
        {

            User user = await userRepository.Get(vm.User.Id);
            Course course = await courseRepository.Get(vm.IdCourse);
            CertificateTemplate certificateTemplate = await certificateTemplateRepository.Get(course.CertificateTemplate.Id);
            if (user != null && course != null)
            {
                try
                {
                    var certificate = new Certificate()
                    {
                        UserId = user.Id,
                        Name = "Certificate for " + user.Name + " " + user.LastName + " " + user.SurName + " passed course " + course.Name,
                        CertificateTemplate= certificateTemplate
                    };
                    user.Certificates.Add(certificate);
                    await certificateRepository.Add(certificate);
                    File.Copy(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Files\\Certificates\\certificate.pdf", Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + $"\\Files\\Certificates\\{certificate.Id}.pdf");
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
            User user = await userRepository.Get(vm.User.Id);
            List<Certificate> certificates = (await certificateRepository.GetAll()).Where(x=>x.UserId==user.Id).ToList();
            if (user is not null&&certificates is not null)
            {
                return new BaseResponse<List<Certificate>>()
                {
                    Data = certificates,
                    Description = "Can reach certificate.",
                    StatusCode = StatusCode.OK
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
