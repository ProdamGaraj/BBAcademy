using System.Threading.Tasks;

namespace Backend.Services.Repository.ICRUD
{
    public interface IAdd<TEntity> where TEntity : class
    {
        Task<bool> Add(TEntity entity);
    }
}
