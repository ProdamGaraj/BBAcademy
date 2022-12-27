using Backend.Models;
using Backend.Services.Repository.ICRUD;

namespace Backend.Services.Repository.Interfaces
{
    public interface IAnswerRepository: IAdd<Answer>, IGetAll<Answer>, IGet<Answer>, IMarkDeleted<Answer>, IUpdate<Answer>
    {
    }
}
