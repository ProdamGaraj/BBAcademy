namespace Backend.Models
{
    public class Profession:Entity
    {
        public List<Course> Courses { get; set; }
        public Profession(List<Course> courses)
        {
            Courses = courses;
        }
    }
}