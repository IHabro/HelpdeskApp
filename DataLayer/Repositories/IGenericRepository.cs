namespace DataLayer.Repositories
{
    internal interface IGenericRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        T? GetById(object id);
        void Insert(T entity);
        void Update(T entity);
        void DeleteById(object id);
        void Delete(T? entity);
        int SaveChanges();
    }
}
