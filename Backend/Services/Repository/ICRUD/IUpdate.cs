using System.Threading.Tasks;

namespace Backend.Services.Repository.ICRUD
{
    public interface IUpdate<TEntity> where TEntity : class
    {
        Task<bool> Update(TEntity entity);
    }
}
