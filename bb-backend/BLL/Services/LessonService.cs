using BLL.Services.Interfaces;
using Infrastructure.Common;
using Infrastructure.Models;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BLL.Services
{
    public class LessonService : ILessonService
    {
        private readonly IRepository<Lesson> _lessonRepository;
        private readonly ILogger<LessonService> _logger;

        public LessonService(IRepository<Lesson> lessonRepository, ILogger<LessonService> logger)
        {
            _logger = logger;
            _lessonRepository = lessonRepository;
        }

        public async Task<ICollection<Lesson>> GetLessons(long courseId)
        {
            try
            {
                var lessons = await _lessonRepository.GetAll()
                    .Where(l => l.CourseId == courseId)
                    .ToListAsync();

                return lessons;
            }
            catch (Exception ex)
            {
                throw new BusinessException("Failed GetLessons", ex);
            }
        }
    }
}