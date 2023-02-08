using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Models;

namespace Backend.Services.Repository.ICRUD
{
    public interface IGetAllCustomId<TEntity> where TEntity : class
    {
        public Task<IList<Question>> GetAllCustomId(long id);
    }
}
