using DataLayer.Areas.Identity.Data;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.AccessControl;

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
            return _dbSet.Include(u => u.Incidents).Include(u => u.Actions).Include(u => u.RolesInProjects).Include(u => u.ActiveProject);
        }

        public HelpdeskUser? GetById(object id)
        {
            if (id == null)
                return null;

            return _dbSet.Include(u => u.Incidents).Include(u => u.Actions).Include(u => u.RolesInProjects).Include(u => u.ActiveProject).FirstOrDefault(u => u.Id == id.ToString());
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

        public void SetActiveProjectForUser(Project? project, HelpdeskUser? user)
        {
            if (project == null || user == null)
                return;

            user.ActiveProject = project;
            user.ActiveProject_Fk = project.Id;

            Update(user);
        }

        public void RemoveActiveProjecyForUser(HelpdeskUser? user)
        {
            if (user == null)
                return;

            user.ActiveProject_Fk = null;
            user.ActiveProject = null;

            Update(user);
        }
    }
}
