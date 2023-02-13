using Infrastructure.Models.Enum;

namespace BLL.Models.GetCoursesForDashboard;

public class CourseForDashboardDto
{
    public long Id { get; set; }
    public string MediaPath { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public int LessonsCount { get; set; }

    public float DurationHours { get; set; }

    public CourseProgressState? State { get; set; }
}