using Backend.Models;
using Backend.Services.Repository.ICRUD;

namespace Backend.Services.Repository.Interfaces
{
    public interface IKeyRepository:IAdd<Key>, IGetAll<Key>, IGet<Key>, IUpdate<Key>, IMarkDeleted<Key>
    {
    }
}
