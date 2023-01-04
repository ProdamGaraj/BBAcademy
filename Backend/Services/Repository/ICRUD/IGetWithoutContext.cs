namespace Backend.Services.Repository.ICRUD
{
        public interface IGetWithoutContext<TEntity> where TEntity : class
        {
            Task<TEntity> GetWithoutContext(long id);
        }
}
