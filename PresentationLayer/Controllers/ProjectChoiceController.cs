using DataLayer.Areas.Identity.Data;
using DataLayer.DbContexts;
using DataLayer.Migrations;
using DataLayer.Models;
using DataLayer.Repositories;
using DataLayer.UnitOfWorks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    public class ProjectChoiceController : Controller
    {
        private ActiveProjectUoW _activeProjectUoW;
        private UserManager<HelpdeskUser> _userManager;

        public ProjectChoiceController(IdentityDbContext context, UserManager<HelpdeskUser> userManager)
        {
            _activeProjectUoW = new ActiveProjectUoW(context);
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View(_activeProjectUoW.GetProjects().ToList());
        }

        public IActionResult RemoveActive()
        {
            var user = _userManager.GetUserAsync(User).Result;

            if (user == null)
                return NotFound();

            _activeProjectUoW.RemoveActiveProject(user.Id);
            _activeProjectUoW.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult AddActive(int? id)
        {
            var user = _userManager.GetUserAsync(User).Result;

            if (user == null)
                return NotFound();

            _activeProjectUoW.SetActiveProject(id, user.Id);
            _activeProjectUoW.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
