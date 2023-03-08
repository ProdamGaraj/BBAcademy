namespace Infrastructure.Models;

public class Order : Entity
{
    public long UserId { get; set; }
    public virtual User User { get; set; }

    /// <summary>
    /// Invoice Id
    /// </summary>
    public string ExternalPaymentId { get; set; }
    public virtual ICollection<OrderLine> OrderLines { get; set; }
    public virtual ICollection<Course> Courses { get; set; }
} 