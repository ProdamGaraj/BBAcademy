namespace WebApi.ViewModels
{
    public class AccountViewModel
    {
        public User User{ get; set; }
        public List<Course> AllCourses { get; set; }
        public List<Course> InCartCourses { get; set; } 
        public List<Course> BoughtCourses { get; set; }
        public List<Course> EndedCourses { get; set; }
        public string filter { get; set; }
        public long ClickedCourseId { get; set; }
    }
}
