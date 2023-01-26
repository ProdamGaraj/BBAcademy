using Backend.Models;

namespace Backend.ViewModels
{
    public class CartViewModel
    {
        public List<Course> Courses { get; set; }
        public User User { get; set; }
    }
}
