using DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories
{
    public class ProjectRepository : IGenericRepository<Project>
    {
        private readonly DbContext _context;
        private readonly DbSet<Project> _dbSet;

        public ProjectRepository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<Project>();
        }

        public void Delete(Project? entity)
        {
            if (entity == null)
                return;

            if (_dbSet.Entry(entity).State == EntityState.Detached)
                _dbSet.Attach(entity);

            _dbSet.Remove(entity);
        }

        public void DeleteById(object id)
        {
            Project? project = _dbSet.Find(Convert.ToInt32(id));

            Delete(project);
        }

        public IQueryable<Project> GetAll()
        {
            return _dbSet.Include(p => p.Incidents).Include(p => p.UsersAndRoles).Include(p => p.Roles).Include(p => p.ActionOnRoleInProject);
        }

        public Project? GetById(object id)
        {
            if (id == null)
                return null;

            return _dbSet.Include(p => p.Incidents).Include(p => p.UsersAndRoles).Include(p => p.Roles).Include(p => p.ActionOnRoleInProject).FirstOrDefault(p => p.Id == Convert.ToInt32(id));
        }

        public void Insert(Project entity)
        {
            _dbSet.Add(entity);
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public void Update(Project entity)
        {
            _dbSet.Update(entity);
        }
    }
}
