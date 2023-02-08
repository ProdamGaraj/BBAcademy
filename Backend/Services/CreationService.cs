using System;
using System.Threading.Tasks;
using Backend.Models;
using Backend.Models.Enum;
using Backend.Models.Interfaces;
using Backend.Models.Responce;
using Backend.Services.Interfaces;
using Backend.Services.Repository.Interfaces;
using Backend.ViewModels;
using Microsoft.Extensions.Logging;

namespace Backend.Services
{
    public class CreationService : ICreationService
    {
        private ICourseRepository courseRepository;
        private ILogger<CreationService> _logger;

        public CreationService( ICourseRepository courseRepository, ILogger<CreationService> logger)
        {
            this.courseRepository = courseRepository;
            _logger = logger;
        }

        public async Task<IBaseResponce<DataViewModel>> CreateFullCourse(DataViewModel dataViewModel)
        {
            Course course;
            try
            {
                if (dataViewModel is not null)
                {
                    course = dataViewModel.Course;

                    if (course is not null && course.Name is not null)
                    {
                        _logger.LogError("CreateFullCourse before add");
                        await courseRepository.Add(course);
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
                _logger.LogError(ex.Message + ":" + ex.InnerException + ":" + ex.StackTrace);
                return new BaseResponse<DataViewModel>()
                {
                    Description = ex.Message + ":" + ex.InnerException + ":" + ex.StackTrace,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

    }
}
