using BLL.Services.Interfaces;
using Infrastructure.Common;
using Infrastructure.Models;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BLL.Services
{
    public class CourseService : ICourseService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Course> _courseRepository;
        private readonly IRepository<Question> _questionRepository;
        private readonly IRepository<Exam> _examRepository;
        private readonly ILogger<CourseService> _logger;

        public CourseService(IRepository<User> userRepository, IRepository<Course> courseRepository, IRepository<Question> questionRepository, IRepository<Exam> examRepository, ILogger<CourseService> logger)
        {
            _userRepository = userRepository;
            _courseRepository = courseRepository;
            _questionRepository = questionRepository;
            _examRepository = examRepository;
            _logger = logger;
        }

        public async Task<ICollection<Course>> GetCourses(long userId)
        {
            try
            {
                var courses = await _courseRepository
                    .GetAll()
                    .ToListAsync();

                // TODO: filtering
                
                return courses;
            }
            catch (Exception ex)
            {
                throw new BusinessException("Failed GetCourses", ex);
            }
        }

        public async Task<Course> GetCourse(long courseId)
        {
            try
            {
                var course = await _courseRepository.GetAll()
                    .FirstOrDefaultAsync(c => c.Id == courseId);

                return course;
            }
            catch (Exception ex)
            {
                throw new BusinessException("Failed GetCourse", ex);
            }
        }
    }
}