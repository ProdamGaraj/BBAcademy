using Backend.Models;
namespace Backend.ViewModels
{
    public class AccountViewModel
    {
        public User User{ get; set; }
        public List<Course> AllCourses { get; set; }
        public List<Course> BoughtCourses { get; set; }
        public List<Course> EndedCourses { get; set; }
    }
}
