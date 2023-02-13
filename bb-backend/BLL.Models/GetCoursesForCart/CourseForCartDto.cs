namespace BLL.Models.GetCoursesForCart;

public class CourseForCartDto
{
    public long Id { get; set; }
    public string MediaPath { get; set; }

    public float DurationHours { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public decimal Price { get; set; }

    public int LessonsCount { get; set; }
}