namespace BLL.ClickService;

public interface IClickService
{
    Task<int> CreateInvoice(float amount, string phone, long orderId);
}