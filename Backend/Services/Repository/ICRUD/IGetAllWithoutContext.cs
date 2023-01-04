namespace Backend.Services.Repository.ICRUD
{
        public interface IGetAllWithoutContext<TEntity> where TEntity : class
        {
            Task<IList<TEntity>> GetAllWithoutContext();
        }
}
