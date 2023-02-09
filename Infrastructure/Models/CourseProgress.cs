using Infrastructure.Models.Enum;

namespace Infrastructure.Models;

public class CourseProgress
{
    public long UserId { get; set; }

    public virtual User User { get; set; }

    public long CourseId { get; set; }

    public virtual Course Course { get; set; }

    public CourseProgressState State { get; set; }
}