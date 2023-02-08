using System.Threading.Tasks;

namespace Backend.Services.Repository.ICRUD
{
    public interface IDelete<TEntity> where TEntity : class
    {
        Task<bool> Delete(TEntity entity);
    }
}
