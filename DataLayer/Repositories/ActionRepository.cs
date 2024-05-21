using DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories
{
    public class ActionRepository : IGenericRepository<EscalationAction>
    {
        private readonly DbContext _context;
        private readonly DbSet<EscalationAction> _dbSet;

        public ActionRepository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<EscalationAction>();
        }


        public void Delete(EscalationAction? entity)
        {
            if (entity == null)
                return;

            if (_dbSet.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }

            _dbSet.Remove(entity);
        }

        public void DeleteById(object id)
        {
            EscalationAction? action = _dbSet.Find(Convert.ToInt32(id));

            Delete(action);
        }

        public IQueryable<EscalationAction> GetAll()
        {
            return _dbSet.Include(a => a.Levels).Include(a => a.Users).Include(a => a.ActionOnProjectAndRole);
        }

        public EscalationAction? GetById(object id)
        {
            if (id == null)
                return null;

            return _dbSet.Include(a => a.Levels).Include(a => a.Users).Include(a => a.ActionOnProjectAndRole).FirstOrDefault(a => a.Id == Convert.ToInt32(id));
        }

        public void Insert(EscalationAction entity)
        {
            _dbSet.Add(entity);
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public void Update(EscalationAction entity)
        {
            _dbSet.Update(entity);
        }
    }
}
