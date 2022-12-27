namespace Backend.Services.Repository.ICRUD
{
    public interface IMarkDeleted<TEntity> where TEntity : class
    {
        void MarkAsDeleted(TEntity entity);
    }
}
