using Backend.Models;
using Backend.Models.Interfaces;
using Backend.Models.Responce;
using Backend.Services.Repository;
using Backend.ViewModels;
using Newtonsoft.Json;

namespace Backend.Services
{
    public class CartService
    {
        public async Task<IBaseResponce<List<Course>>> GetInKartCourses(User user)
        {
            List<Course> inKartCourses = new List<Course>();
            List<Course> allCourses= new List<Course>();
            CourseRepository cr = new CourseRepository();
            allCourses.AddRange(await cr.GetAll());
            List<long> courseIds = new List<long>();
            courseIds.AddRange(JsonConvert.DeserializeObject<List<long>>(user.InKartCourses));
            foreach(long id in courseIds)
            {
                inKartCourses.Add(allCourses.FirstOrDefault(x=>x.Id==id));
            }
            return new BaseResponse<List<Course>>() { Data = inKartCourses, Description = "Get all courses for a user", StatusCode = Models.Enum.StatusCode.OK };
        }
    }
}
