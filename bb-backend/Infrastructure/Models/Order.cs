namespace Infrastructure.Models;

public class Order : Entity
{
    public long UserId { get; set; }
    public virtual ICollection<OrderLine> OrderLines { get; set; }
    public DateTime InvoiceCreationTime { get; set; }
    public DateTime CompleatingResponseTime { get; set; }
    //TODO: Orderline
} 