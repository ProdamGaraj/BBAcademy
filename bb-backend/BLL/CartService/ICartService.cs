using BLL.Models.GetCoursesForCart;
using Infrastructure.Models;

namespace BLL.CartService;

public interface ICartService
{
    Task Checkout(long dto);
    Task RemoveCourse(long courseId, long userId); 
}