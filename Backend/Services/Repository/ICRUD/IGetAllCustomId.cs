using Backend.Models;

namespace Backend.Services.Repository.ICRUD
{
    public interface IGetAllCustomId<TEntity> where TEntity : class
    {
        public IList<Question> GetAllCustomId(long id);
    }
}
