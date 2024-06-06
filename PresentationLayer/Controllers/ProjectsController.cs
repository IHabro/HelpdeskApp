using DataLayer.Areas.Identity.Data;
using DataLayer.DbContexts;
using DataLayer.UnitOfWorks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly UserManager<HelpdeskUser> _userManager;
        private readonly Project_ProjectRole_UoW _projectProjectRoleUoW;

        public ProjectsController(IdentityDbContext context, UserManager<HelpdeskUser> userManager)
        {
            _userManager = userManager;
            _projectProjectRoleUoW = new Project_ProjectRole_UoW(context);
        }

        public IActionResult Index()
        {
            var model = _projectProjectRoleUoW.GetProjectsIndexModel(_userManager.GetUserAsync(User).Result);
            return View(model);
        }

        public IActionResult Include(int? roleId)
        {
            _projectProjectRoleUoW.IncludeRoleIntoProject(_userManager.GetUserAsync(User).Result, roleId);
            _projectProjectRoleUoW.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Exclude(int? roleId)
        {
            _projectProjectRoleUoW.ExcludeRoleFromProject(_userManager.GetUserAsync(User).Result, roleId);
            _projectProjectRoleUoW.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
