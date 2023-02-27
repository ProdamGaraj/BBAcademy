namespace Infrastructure.Models;

public class Order : Entity
{
    public long UserId { get; set; }
    public virtual ICollection<Course> Courses { get; set; }
    public DateTime InvoiceCreationTime { get; set; }
    public DateTime CompleatingResponseTime { get; set; }
    //TODO: Orderline
}