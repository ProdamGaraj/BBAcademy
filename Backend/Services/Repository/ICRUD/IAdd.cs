namespace Backend.Services.Repository.ICRUD
{
    public interface IAdd<TEntity> where TEntity : class
    {
        void Add(TEntity entity);
    }
}
