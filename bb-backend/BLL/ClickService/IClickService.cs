using BLL.Models.Click;

namespace BLL.ClickService;

public interface IClickService
{
    Task<ClickPrepareBllResponseModel> ProcessPrepare(ClickPrepareBllModel model);
    Task<ClickCompleteBllResponseModel> ProcessComplete(ClickCompleteBllModel model);
}