using BLL.Models.GetCoursesForCart;
using Infrastructure.Models;

namespace BLL.CartService;

public interface ICartService
{
    Task<ICollection<CourseForCartDto>> Checkout(long dto);
    Task<CourseForCartDto> RemoveCourse(long courseId, long userId); 
}