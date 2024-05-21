using DataLayer.Areas.Identity.Data;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories
{
    public class UserRepository : IGenericRepository<HelpdeskUser>
    {
        private readonly DbContext _context;
        private readonly DbSet<HelpdeskUser> _dbSet;

        public UserRepository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<HelpdeskUser>();
        }

        public void Delete(HelpdeskUser? entity)
        {
            if (entity == null)
                return;

            if (_context.Entry(entity).State == EntityState.Detached)
                _dbSet.Attach(entity);

            _dbSet.Remove(entity);
        }

        public void DeleteById(object id)
        {
            HelpdeskUser? user = _dbSet.Find(id.ToString());

            Delete(user);
        }

        public IQueryable<HelpdeskUser> GetAll()
        {
            return _dbSet.Include(u => u.Incidents).Include(u => u.Actions).Include(u => u.RolesInProjects);
        }

        public HelpdeskUser? GetById(object id)
        {
            if (id == null)
                return null;

            return _dbSet.Include(u => u.Incidents).Include(u => u.Actions).Include(u => u.RolesInProjects).FirstOrDefault(u => u.Id == id.ToString());
        }

        public void Insert(HelpdeskUser entity)
        {
            _dbSet.Add(entity);
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public void Update(HelpdeskUser entity)
        {
            _dbSet.Update(entity);
        }
    }
}
