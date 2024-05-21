using DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories
{
    public class IncidentRepository : IGenericRepository<Incident>
    {
        private readonly DbContext _context;
        private readonly DbSet<Incident> _dbSet;

        public IncidentRepository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<Incident>();
        }

        public void Delete(Incident? entity)
        {
            if (entity == null)
                return;

            if (_dbSet.Entry(entity).State == EntityState.Detached)
                _dbSet.Attach(entity);

            _dbSet.Remove(entity);
        }

        public void DeleteById(object id)
        {
            Incident incident = _dbSet.Find(Convert.ToInt32(id));

            Delete(incident);
        }

        public IQueryable<Incident> GetAll()
        {
            return _dbSet.Include(i => i.Project).Include(i => i.Project).Include(i => i.User);
        }

        public Incident? GetById(object id)
        {
            if (id == null)
                return null;

            return _dbSet.Include(i => i.Project).Include(i => i.Project).Include(i => i.User).FirstOrDefault(i => i.Id == Convert.ToInt32(id));
        }

        public void Insert(Incident entity)
        {
            _dbSet.Add(entity);
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public void Update(Incident entity)
        {
            _dbSet.Update(entity);
        }
    }
}
