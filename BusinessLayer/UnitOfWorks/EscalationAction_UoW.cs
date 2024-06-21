using DataLayer.Areas.Identity.Data;
using DataLayer.DbContexts;
using DataLayer.Models;
using DataLayer.Repositories;
using DataLayer.ViewModels;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BusinessLayer.UnitOfWorks
{
    public class EscalationAction_UoW : IUnitOfWork
    {
        private readonly IdentityDbContext _context;
        private readonly ActionRepository _actionRepository;
        private readonly UserRepository _userRepository;
        private readonly ProjectRepository _projectRepository;
        private readonly LevelRepository _levelRepository;

        public EscalationAction_UoW(IdentityDbContext context)
        {
            _context = context;
            _actionRepository = new ActionRepository(context);
            _userRepository = new UserRepository(context);
            _projectRepository = new ProjectRepository(context);
            _levelRepository = new LevelRepository(context);
        }

        // Model
        public ActionUsersViewModel? GetOpenUsersModel(int? actionId, int? projectId)
        {
            var model = new ActionUsersViewModel();

            var action = _actionRepository.GetById(actionId);
            var project = _projectRepository.GetById(projectId);

            if (action == null || project == null)
                return null;

            var projectUsers = project.UsersAndRoles.Select(ur => ur.User_Fk).ToList();
            var users = new List<HelpdeskUser>();

            foreach (var user in projectUsers)
            {
                var tmp = _userRepository.GetById(user);

                if (tmp != null)
                    users.Add(tmp);
            }

            model.Action = action;
            model.UsersToNotify = action.Users;
            model.NotificationCandidates = users.Except(action.Users).ToList();

            return model;
        }

        // User CRUD
        public HelpdeskUser? GetUserById(object id)
        {
            return _userRepository.GetById(id);
        }

        public void UpdateUser(HelpdeskUser user)
        {
            _userRepository.Update(user);
        }

        // Actions CRUD
        public IQueryable<EscalationAction> GetActions()
        {
            return _actionRepository.GetAll();
        }

        public EscalationAction? GetActionById(object id)
        {
            return _actionRepository.GetById(id);
        }

        public void DeleteActionById(object id)
        {
            _actionRepository.DeleteById(id);
        }

        public void Insert(EscalationAction entity)
        {
            _actionRepository.Insert(entity);
        }

        public void UpdateAction(EscalationAction entity)
        {
            _actionRepository.Update(entity);
        }

        // IDisposable + IUnitOfWork
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
