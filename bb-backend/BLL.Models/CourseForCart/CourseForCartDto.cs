namespace BLL.Models.CourseForCart;

public class CourseForCartDto
{
    public string Name { get; set; }
    public string MediaPath { get; set; }

    public float DurationHours { get; set; }

    public string Description { get; set; }

    public decimal Price { get; set; }

    public bool IsBought { get; set; }

    public int LessonsCount { get; set; }
}