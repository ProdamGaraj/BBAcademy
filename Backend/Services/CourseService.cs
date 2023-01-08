using Backend.Models;
using Backend.Models.Responce;
using Backend.Services.Repository;
using Newtonsoft.Json;

namespace Backend.Services
{
    public class CourseService
    {
        public async Task<BaseResponse<object>> GetCourses(User user)
        {
            CourseRepository cr = new CourseRepository();
            List<Course> allCourses= new List<Course>();
            allCourses.AddRange(await cr.GetAll());
            foreach(Course course in user.BoughtCourses)
            {
                allCourses.Remove(course);
            }
            List<long> ids = JsonConvert.DeserializeObject<List<long>>(user.PassedCoursesId);
            List<Course> passedCourses = new List<Course>();
            foreach(long id in ids)
            {
                passedCourses.Add(allCourses.First(x=>x.Id == id));
                allCourses.Remove(passedCourses.Last());
            }
            return new BaseResponse<object>() {Data = new {allCourses,passedCourses,user.BoughtCourses }, Description="Get all courses for a user", StatusCode = Models.Enum.StatusCode.OK };
        }
    }
}
