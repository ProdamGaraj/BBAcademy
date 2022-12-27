using Backend.Models;
using Backend.Services.Repository.ICRUD;
using Microsoft.EntityFrameworkCore.Update;

namespace Backend.Services.Repository.Interfaces
{
    public interface IUserRepository:IAdd<User>, IGet<User>, IGetAll<User>, IUpdate<User>, IMarkDeleted<User>
    {
    }
}
