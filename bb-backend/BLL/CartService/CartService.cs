using BLL.Models.GetCoursesForCart;

namespace BLL.CartService;

public class CartService:ICartService
{
    public Task Checkout(long userId)
    {
        throw new NotImplementedException();
    }

    public Task RemoveCourse(long courseId, long userId)
    {
        throw new NotImplementedException();
    }
}