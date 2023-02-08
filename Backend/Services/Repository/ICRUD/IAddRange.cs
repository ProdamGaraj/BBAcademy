using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Services.Repository.ICRUD
{
    public interface IAddRange<TEntity> where TEntity : class
    {
        Task<bool> AddRange(ICollection<TEntity> entity);
    }
}
