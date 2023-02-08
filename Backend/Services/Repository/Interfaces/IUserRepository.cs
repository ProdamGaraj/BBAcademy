using Backend.Models;
using Backend.Services.Repository.ICRUD;

namespace Backend.Services.Repository.Interfaces
{
    public interface IUserRepository: IAdd<User>, IGet<User>, IGetAll<User>, IUpdate<User>, IMarkDeleted<User>
    {
    }
}
