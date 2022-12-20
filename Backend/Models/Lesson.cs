namespace Backend.Models
{
    public class Lesson:Entity
    {
        public string Description { get; set; }
        public string VideoPath { get; set; }//?
        public Course Course { get; set; }
    }
}
