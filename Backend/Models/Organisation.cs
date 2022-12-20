namespace Backend.Models
{
    public class Organisation : Entity
    {
        public int KeyCount { get; set; }
        public string Description { get; set; }
        public List<User> Workers { get; set; }
    }
}