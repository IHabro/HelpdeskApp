using DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories
{
    public class LevelRepository : IGenericRepository<EscalationLevel>
    {
        private readonly DbContext _context;
        private readonly DbSet<EscalationLevel> _dbSet;

        public LevelRepository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<EscalationLevel>();
        }

        public void Delete(EscalationLevel? entity)
        {
            if (entity == null)
                return;

            if (_dbSet.Entry(entity).State == EntityState.Detached)
                _dbSet.Attach(entity);

            _dbSet.Remove(entity);
        }

        public void DeleteById(object id)
        {
            EscalationLevel? level = _dbSet.Find(Convert.ToInt32(id));

            Delete(level);
        }

        public IQueryable<EscalationLevel> GetAll()
        {
            return _dbSet.Include(l => l.Incidents).Include(l => l.Actions);
        }

        public EscalationLevel? GetById(object id)
        {
            if (id == null)
                return null;

            return _dbSet.Include(l => l.Incidents).Include(l => l.Actions).FirstOrDefault(l => l.Id == Convert.ToInt32(id));
        }

        public void Insert(EscalationLevel entity)
        {
            _dbSet.Add(entity);
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public void Update(EscalationLevel entity)
        {
            _dbSet.Update(entity);
        }
    }
}
