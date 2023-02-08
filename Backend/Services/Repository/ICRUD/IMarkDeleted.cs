using System.Threading.Tasks;

namespace Backend.Services.Repository.ICRUD
{
    public interface IMarkDeleted<TEntity> where TEntity : class
    {
        public Task<bool> MarkAsDeleted(TEntity entity);
    }
}
