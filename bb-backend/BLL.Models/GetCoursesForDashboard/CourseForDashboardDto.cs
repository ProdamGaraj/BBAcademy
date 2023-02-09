namespace BLL.Models.GetCoursesForDashboard;

public class CourseForDashboardDto
{
    public string MediaPath { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public int LessonsCount { get; set; }

    public float DurationHours { get; set; }

    public bool IsBought { get; set; }
}