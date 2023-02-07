using Backend.Models;
using Backend.Models.Enum;
using Backend.Models.Interfaces;
using Backend.Models.Responce;
using Backend.Services.Interfaces;
using Backend.Services.Repository;
using Backend.Services.Repository.Interfaces;
using Backend.ViewModels;
using NLog;

namespace Backend.Services
{
    public class CreationService : ICreationService
    {
        private ICourseRepository courseRepository;

        public CreationService( ICourseRepository courseRepository)
        {
            this.courseRepository = courseRepository;
        }

        public async Task<IBaseResponce<DataViewModel>> CreateFullCourse(DataViewModel dataViewModel)
        {
            Logger logger = LogManager.GetCurrentClassLogger();
            Course Course;
            CertificateTemplate CertificateTemplate;
            try
            {
                if (dataViewModel is not null)
                {
                    Course = dataViewModel.Course;

                    if (Course is not null && Course.Name is not null)
                    {
                        courseRepository.Add(Course);
                    }
                }
                return new BaseResponse<DataViewModel>()
                {
                    Data = dataViewModel,
                    Description = $"Course has been added succsesfully",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message + ":" + ex.InnerException + ":" + ex.StackTrace);
                return new BaseResponse<DataViewModel>()
                {
                    Description = ex.Message + ":" + ex.InnerException + ":" + ex.StackTrace,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

    }
}
