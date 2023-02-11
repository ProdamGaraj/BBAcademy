using AutoMapper;
using BLL.Models.CourseForCart;
using BLL.Models.GetCourseForLearning;
using BLL.Models.GetCoursesForDashboard;
using BLL.Models.Save;
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

        public async Task<ICollection<CourseForDashboardDto>> GetCoursesForDashboard(long userId)
        {
            try
            {
                var courses = await _courseRepository.GetAll()
                    .Select(
                        c => new CourseForDashboardDto()
                        {
                            Id = c.Id,
                            Name = c.Description,
                            Description = c.Description,
                            DurationHours = c.DurationHours,
                            LessonsCount = c.Lessons.Count(),
                            MediaPath = c.MediaPath,
                            IsBought = c.CourseProgresses.Any(p => p.State == CourseProgressState.Bought && p.UserId == userId)
                        }
                    )
                    .ToListAsync();

                return courses;
            }
            catch (Exception ex)
            {
                throw new BusinessException("GetCoursesForDashboard", ex);
            }
        }

        public async Task<ICollection<CourseForCartDto>> GetCartedCoursesForUser(long userId)
        {
            
            try
            {
                var courses = await _courseRepository.GetAll()
                    .Where(c => c.CourseProgresses.Any(p => p.State == CourseProgressState.Bought && p.UserId == userId))
                    .Select(
                        c => new CourseForCartDto()
                        {
                            Name = c.Description, //TODO mapping
                            Description = c.Description,
                            DurationHours = c.DurationHours,
                            LessonsCount = c.Lessons.Count(),
                            MediaPath = c.MediaPath
                        }
                    )
                    .ToListAsync();

                return courses;
            }
            catch (Exception ex)
            {
                throw new BusinessException("GetCoursesForDashboard", ex);
            }
        }

        public async Task<GetCourseForLearningDto> GetForLearning(long courseId)
        {
            try
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
            catch (Exception ex)
            {
                throw new BusinessException("Failed GetForLearning", ex);
            }
        }

        public async Task<long> SaveCourse(SaveCourseDto dto)
        {
            using var scope = _logger.BeginScope(dto);
            _logger.LogInformation("Saving Course");

            var course = _mapper.Map<Course>(dto);
            _courseRepository.Add(course);
            await _courseRepository.SaveChangesAsync();

            _logger.LogInformation("Finished saving course");

            return course.Id;
        }
    }
}