using DataLayer.Areas.Identity.Data;
using DataLayer.DbContexts;
using DataLayer.Models;
using DataLayer.Repositories;
using DataLayer.ViewModels;

namespace DataLayer.UnitOfWorks
{
    public class Project_ProjectRole_UoW : IUnitOfWork
    {
        private readonly IdentityDbContext _context;
        private readonly RoleRepository _roleRepository;
        private readonly ProjectRepository _projectRepository;
        private readonly UserRepository _userRepository;

        public Project_ProjectRole_UoW(IdentityDbContext context)
        {
            _context = context;
            _roleRepository = new RoleRepository(context);
            _projectRepository = new ProjectRepository(context);
            _userRepository = new UserRepository(context);
        }

        public IQueryable<ProjectRole> GetRoles()
        {
            return _roleRepository.GetAll();
        }

        public ProjectRolesViewModel? GetProjectsIndexModel(HelpdeskUser? activeUser)
        {
            if (activeUser == null)
                throw new Exception("Active User cannot be null in Project_ProjectRole_UoW.");

            activeUser = _userRepository.GetById(activeUser.Id);

            if (activeUser == null || activeUser.ActiveProject == null)
                throw new Exception("ActiveProject cannot be null in Project_ProjectRole_UoW for logged user.");

            var roles = _roleRepository.GetAll();

            var model = new ProjectRolesViewModel
            {
                Included = [.. roles.Where(r => r.Projects.Contains(activeUser.ActiveProject))],
                Excluded = [.. roles.Where(r => r.Projects.Contains(activeUser.ActiveProject) == false)]
            };

            return model;
        }

        public void IncludeRoleIntoProject(HelpdeskUser? activeUser, int? roleId)
        {
            if (activeUser == null)
                throw new Exception("Active User cannot be null in Project_ProjectRole_UoW.");

            if (roleId == null)
                throw new Exception("Role cannot be null in Project_ProjectRole_UoW.");

            activeUser = _userRepository.GetById(activeUser.Id);

            if (activeUser == null || activeUser.ActiveProject == null)
                throw new Exception("ActiveProject cannot be null in Project_ProjectRole_UoW for logged user.");

            ProjectRole? role = _roleRepository.GetById(roleId) ?? throw new Exception($"Provided roleId {roleId} returned null, or default DB value for ProjectRole.");

            role.Projects.Add(activeUser.ActiveProject);
            activeUser.ActiveProject.Roles.Add(role);

            _projectRepository.Update(activeUser.ActiveProject);
            _roleRepository.Update(role);
        }

        public void ExcludeRoleFromProject(HelpdeskUser? activeUser, int? roleId)
        {
            if (activeUser == null)
                throw new Exception("Active User cannot be null in Project_ProjectRole_UoW.");

            if (roleId == null)
                throw new Exception("Role cannot be null in Project_ProjectRole_UoW.");

            activeUser = _userRepository.GetById(activeUser.Id);

            if (activeUser == null || activeUser.ActiveProject == null)
                throw new Exception("ActiveProject cannot be null in Project_ProjectRole_UoW for logged user.");

            ProjectRole? role = _roleRepository.GetById(roleId) ?? throw new Exception($"Provided roleId {roleId} returned null, or default DB value for ProjectRole.");

            role.Projects.Remove(activeUser.ActiveProject);
            activeUser.ActiveProject.Roles.Remove(role);

            _projectRepository.Update(activeUser.ActiveProject);
            _roleRepository.Update(role);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
