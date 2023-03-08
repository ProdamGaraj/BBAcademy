using BLL.Models.GetCoursesForCart;
using Infrastructure.Models;

namespace BLL.CartService;

public interface ICartService
{
    Task AddCourse(long courseId, long userId);
    Task RemoveCourse(long courseId, long userId);
}