using BLL.Models.GetCoursesForCart;

namespace BLL.CartService;

public class CartService:ICartService
{
    public Task<ICollection<CourseForCartDto>> Checkout(long userId)
    {
        throw new NotImplementedException();
    }

    public Task<CourseForCartDto> RemoveCourse(long courseId, long userId)
    {
        throw new NotImplementedException();
    }
}