using Backend.Models;

namespace Backend.Services.Repository.ICRUD
{
    public interface IAddRange<TEntity> where TEntity : class
    {
        Task<bool> AddRange(ICollection<TEntity> entity);
    }
}
