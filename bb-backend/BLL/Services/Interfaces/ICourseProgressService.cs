namespace BLL.Services.Interfaces;

public interface ICourseProgressService
{
    Task TransitionToCart(long courseId, long userId);
    Task TransitionToBought(long courseId, long userId);
    Task TransitionToPassed(long courseId, long userId);
    Task TransitionToUnknown(long courseId, long userId);
}