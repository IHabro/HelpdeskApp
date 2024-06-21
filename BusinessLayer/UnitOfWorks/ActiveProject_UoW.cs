using DataLayer.Areas.Identity.Data;
using DataLayer.DbContexts;
using DataLayer.Models;
using DataLayer.Repositories;

namespace BusinessLayer.UnitOfWorks
{
    // ToDo: Should probably be inside Business Layer since these classes will contain all logic + are middle man between DataLayer and PresentationLayer
    public class ActiveProject_UoW : IUnitOfWork
    {
        private IdentityDbContext _context;
        private ProjectRepository _projectRepository;
        private UserRepository _userRepository;

        public ActiveProject_UoW(IdentityDbContext context)
        {
            _context = context;
            _projectRepository = new ProjectRepository(context);
            _userRepository = new UserRepository(context);
        }

        public IQueryable<Project> GetProjects()
        {
            return _projectRepository.GetAll();
        }

        public HelpdeskUser? GetUser(string? id)
        {
            if (id == null)
                return null;

            return _userRepository.GetById(id);
        }

        public void SetActiveProject(int? projectId, string? userId)
        {
            if (projectId == null || userId == null)
                return;

            var project = _projectRepository.GetById(projectId);
            var user = _userRepository.GetById(userId);

            _userRepository.SetActiveProjectForUser(project, user);
        }

        public void RemoveActiveProject(string? userId)
        {
            if (userId == null)
                return;

            var user = _userRepository.GetById(userId);

            _userRepository.RemoveActiveProjecyForUser(user);
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
