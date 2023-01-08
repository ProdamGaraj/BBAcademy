namespace Backend.Models
{
    public class CoursesAllTypes
    {
        public List<Course> AllCourses { get; set; }
        public List<Course> BoughtCourses { get; set; }
        public List<Course> PassedCourses { get; set; }
    }
}
