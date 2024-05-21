using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories
{
    // Class for simple data manipulation without any references, can be used for Incidents and then for query of 1:M relations using <class>_Fk
    public class GeneralRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _dbSet;

        public GeneralRepository(DbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<T>();
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet.AsQueryable();
        }

        public T? GetById(object id)
        {
            return _dbSet.Find(id);
        }

        public void Insert(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Update(T entityChanges)
        {
            var entity = _dbSet.Attach(entityChanges);

            entity.State = EntityState.Modified;
        }

        public void DeleteById(object id)
        {
            T? entityToDelete = _dbSet.Find(id);

            Delete(entityToDelete);
        }

        public void Delete(T? entity)
        {
            // Consider entity already removed => log
            if (entity == null)
                return;

            if (_context.Entry(entity).State == EntityState.Detached)
                _dbSet.Attach(entity);

            _dbSet.Remove(entity);
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
