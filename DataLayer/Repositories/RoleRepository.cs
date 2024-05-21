using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public class RoleRepository : IGenericRepository<ProjectRole>
    {
        private readonly DbContext _context;
        private readonly DbSet<ProjectRole> _dbSet;

        public RoleRepository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<ProjectRole>();
        }

        public void Delete(ProjectRole? entity)
        {
            if (entity == null)
                return;

            if (_dbSet.Entry(entity).State == EntityState.Detached)
                _dbSet.Attach(entity);

            _dbSet.Remove(entity);
        }

        public void DeleteById(object id)
        {
            ProjectRole? role = _dbSet.Find(Convert.ToInt32(id));

            Delete(role);
        }

        public IQueryable<ProjectRole> GetAll()
        {
            return _dbSet.Include(r => r.Projects).Include(r => r.UsersInProjects).Include(r => r.ActionForProjectAndRole);
        }

        public ProjectRole? GetById(object id)
        {
            if (id == null)
                return null;

            return _dbSet.Include(r => r.Projects).Include(r => r.UsersInProjects).Include(r => r.ActionForProjectAndRole).FirstOrDefault(r => r.Id == Convert.ToInt32(id));
        }

        public void Insert(ProjectRole entity)
        {
            _dbSet.Add(entity);
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public void Update(ProjectRole entity)
        {
            _dbSet.Update(entity);
        }
    }
}
