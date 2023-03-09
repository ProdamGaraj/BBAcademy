namespace Infrastructure.Models;

public class OrderLine
{
    public long OrderId { get; set; }
    public virtual Order Order { get; set; }

    public long CourseId { get; set; }
    public virtual Course Course { get; set; }
}