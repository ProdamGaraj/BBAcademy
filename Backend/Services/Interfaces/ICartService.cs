using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Models.Interfaces;
using Backend.Models;

namespace Backend.Services.Interfaces
{
    public interface ICartService
    {
        Task<IBaseResponce<List<Course>>> GetInCartCourses(User user);
    }
}
