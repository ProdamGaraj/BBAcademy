using Backend.Models;

namespace Backend.Services
{
    public class CourseService
    {
        public CoursesAllTypes GetCourses()
        {
            return new CoursesAllTypes();
        }
    }
}
