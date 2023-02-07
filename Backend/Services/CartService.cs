using Backend.Models;
using Backend.Models.Interfaces;
using Backend.Models.Responce;
using Backend.Services.Interfaces;
using Backend.Services.Repository;
using Backend.Services.Repository.Interfaces;
using Backend.ViewModels;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Backend.Services
{
    public class CartService : ICartService
    {
        private readonly ICourseRepository cr;
        public CartService(ICourseRepository cr)
        {
            this.cr = cr;
        }

        public async Task<IBaseResponce<List<Course>>> GetInCartCourses(User user)
        {
            List<Course> inCartCourses = new List<Course>();
            List<Course> allCourses = new List<Course>();
            allCourses.AddRange(await cr.GetAll());
            List<long> courseIds = new List<long>();
            if (user.InCartCourses is not null)
            {
                courseIds.AddRange(JsonConvert.DeserializeObject<List<long>>(user.InCartCourses));
            }
            foreach (long id in courseIds)
            {
                inCartCourses.Add(allCourses.FirstOrDefault(x => x.Id == id));
            }
            return new BaseResponse<List<Course>>()
            {
                Data = inCartCourses,
                Description = "Get all courses for a user",
                StatusCode = Models.Enum.StatusCode.OK
            };
        }
    }
}
