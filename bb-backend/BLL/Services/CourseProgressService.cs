using BLL.Services.Interfaces;
using Infrastructure.Common;
using Infrastructure.Models;
using Infrastructure.Models.Enum;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BLL.Services;

public class CourseProgressService : ICourseProgressService
{
    private readonly IRepository<CourseProgress> _repository;
    private readonly ILogger<CourseProgressService> _logger;

    public CourseProgressService(IRepository<CourseProgress> repository, ILogger<CourseProgressService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task TransitionToCart(long courseId, long userId)
    {
        await TransitionToState(courseId, userId, CourseProgressState.InCart);
    }

    public async Task TransitionToUnknown(long courseId, long userId)
    {
        await TransitionToState(courseId, userId, CourseProgressState.Unknown);
    }

    public async Task TransitionToBought(long courseId, long userId)
    {
        await TransitionToState(courseId, userId, CourseProgressState.Bought);
    }

    public async Task TransitionToPassed(long courseId, long userId)
    {
        await TransitionToState(courseId, userId, CourseProgressState.Passed);
    }

    private async Task TransitionToState(long courseId, long userId, CourseProgressState state)
    {
        try
        {
            var courseProgress = await _repository.GetAll()
                .FirstOrDefaultAsync(c => c.UserId == userId && c.CourseId == courseId);

            if (courseProgress is null)
            {
                courseProgress = new CourseProgress()
                {
                    UserId = userId,
                    CourseId = courseId
                };
                _repository.Add(courseProgress);
            }

            courseProgress.State = state;

            await _repository.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new BusinessException("Failed TransitionToCart", ex);
        }
    }
}