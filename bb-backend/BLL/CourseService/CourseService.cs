using AutoMapper;
using BLL.Models.GetCourseForLearning;
using BLL.Models.GetCoursesForCart;
using BLL.Models.GetCoursesForDashboard;
using BLL.Models.SaveCourseEdit;
using Infrastructure.Common;
using Infrastructure.Models;
using Infrastructure.Models.Enum;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BLL.CourseService
{
    public class CourseService : ICourseService
    {
        private readonly IRepository<Course> _courseRepository;
        private readonly ILogger<CourseService> _logger;
        private readonly IMapper _mapper;

        public CourseService(IRepository<Course> courseRepository, ILogger<CourseService> logger, IMapper mapper)
        {
            _courseRepository = courseRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<ICollection<CourseForDashboardDto>> GetCoursesForDashboard(long userId)
        {
            var courses = await _courseRepository.GetAll()
                .Select(
                    c => new CourseForDashboardDto()
                    {
                        Id = c.Id,
                        Title = c.Title,
                        Description = c.Description,
                        DurationHours = c.DurationHours,
                        LessonsCount = c.Lessons.Count(),
                        MediaPath = c.MediaPath,
                        State = c.CourseProgresses.Any(p => p.UserId == userId) ? c.CourseProgresses.FirstOrDefault(p => p.UserId == userId).State : CourseProgressState.Unknown
                    }
                )
                .ToListAsync();

            return courses;
        }

        public async Task<ICollection<CourseForCartDto>> GetCartedCoursesForUser(long userId)
        {
            var courses = await _courseRepository.GetAll()
                .Where(c => c.CourseProgresses.Any(p => p.State == CourseProgressState.Bought && p.UserId == userId))
                .Select(
                    c => new CourseForCartDto()
                    {
                        Title = c.Title, 
                        Description = c.Description,
                        DurationHours = c.DurationHours,
                        LessonsCount = c.Lessons.Count(),
                        MediaPath = c.MediaPath
                    }
                )
                .ToListAsync();

            return courses;
        }

        public async Task<GetCourseForLearningDto> GetForLearning(long courseId)
        {
            var course = await _courseRepository.GetAll()
                .Include(c => c.Exam)
                .ThenInclude(e => e.Questions.OrderBy(q => q.Order))
                .ThenInclude(q => q.AnswerOptions.OrderBy(a => a.Order))
                .Include(c => c.Lessons.OrderBy(l => l.Order))
                .FirstOrDefaultAsync(c => c.Id == courseId);

            var resultDto = _mapper.Map<GetCourseForLearningDto>(course);

            return resultDto;
        }

        public async Task<long> SaveCourseEdit(SaveCourseEditDto editDto)
        {
            using var scope = _logger.BeginScope(editDto);
            _logger.LogInformation("Saving Course");

            var course = _mapper.Map<Course>(editDto);
            _courseRepository.Add(course);
            await _courseRepository.SaveChangesAsync();

            _logger.LogInformation("Finished saving course");

            return course.Id;
        }
    }
}