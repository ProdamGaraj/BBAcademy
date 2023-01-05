using Backend.Models;
using Backend.Services.Repository.ICRUD;

namespace Backend.Services.Repository.Interfaces
{
    public interface IQuestionRepository:IAdd<Question>, IGet<Question>, IGetAll<Question>, IUpdate<Question>, IMarkDeleted<Question>, IGetConditionalType<Question>
    {
    }
}
