using BLL.Models.Click;

namespace BLL.ClickService;

public interface IClickService
{
    Task<int> CreateInvoice(float amount, string phone, long orderId);
    Task<ClickPrepareBllResponseModel> ProcessPrepare(ClickPrepareBllModel model);
    Task<ClickCompleteBllResponseModel> ProcessComplete(ClickCompleteBllModel model);
}