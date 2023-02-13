using BLL.Models.GetCoursesForCart;
using BLL.Services.Interfaces;

namespace BLL.CartService;

public class CartService : ICartService
{
    private ICourseProgressService _courseProgressService;

    public CartService(ICourseProgressService courseProgressService)
    {
        _courseProgressService = courseProgressService;
    }

    public Task Checkout(long userId)
    {
        throw new NotImplementedException();
    }

    public async Task AddCourse(long courseId, long userId)
    {
        await _courseProgressService.TransitionToCart(courseId, userId);
    }

    public async Task RemoveCourse(long courseId, long userId)
    {
        await _courseProgressService.TransitionToUnknown(courseId, userId);
    }
}