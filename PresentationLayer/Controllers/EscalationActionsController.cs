using BusinessLayer.UnitOfWorks;
using DataLayer.Areas.Identity.Data;
using DataLayer.DbContexts;
using DataLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace PresentationLayer.Controllers
{
    public class EscalationActionsController : Controller
    {
        private readonly IdentityDbContext _context;
        private readonly EscalationAction_UoW _actionUoW;
        private readonly UserManager<HelpdeskUser> _userManager;

        public EscalationActionsController(IdentityDbContext context, UserManager<HelpdeskUser> userManager)
        {
            _context = context;
            _actionUoW = new EscalationAction_UoW(context);
            _userManager = userManager;
        }

        // GET: EscalationActions
        public IActionResult Index()
        {
            return View(_actionUoW.GetActions().ToList());
        }

        // GET: EscalationActions/Details/5
        public IActionResult Details(int? id)
        {
            var escalationAction = _actionUoW.GetActionById(id);

            if (escalationAction == null)
                return NotFound();

            return View(escalationAction);
        }

        // GET: EscalationActions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EscalationActions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name,NotificationType")] EscalationAction escalationAction)
        {
            if (ModelState.IsValid)
            {
                _actionUoW.Insert(escalationAction);
                _actionUoW.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View(escalationAction);
        }

        // GET: EscalationActions/Edit/5
        public IActionResult Edit(int? id)
        {
            var escalationAction = _actionUoW.GetActionById(id);

            if (escalationAction == null)
                return NotFound();

            return View(escalationAction);
        }

        // POST: EscalationActions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,NotificationType")] EscalationAction escalationAction)
        {
            if (id != escalationAction.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _actionUoW.UpdateAction(escalationAction);
                    _actionUoW.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EscalationActionExists(escalationAction.Id))
                        return NotFound();
                    else
                        throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(escalationAction);
        }

        // GET: EscalationActions/Delete/5
        public IActionResult Delete(int? id)
        {
            var escalationAction = _actionUoW.GetActionById(id);

            if (escalationAction == null)
                return NotFound();

            return View(escalationAction);
        }

        // POST: EscalationActions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var escalationAction = _actionUoW.GetActionById(id);

            if (escalationAction != null)
                _actionUoW.DeleteActionById(id);

            _actionUoW.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        private bool EscalationActionExists(int id)
        {
            return _context.EscalationActions.Any(e => e.Id == id);
        }

        public IActionResult OpenUsers(int? id)
        {
            var loggedUser = _userManager.GetUserAsync(User).Result;

            if (loggedUser == null || loggedUser.ActiveProject_Fk == null)
                return NotFound();

            var model = _actionUoW.GetOpenUsersModel(id, loggedUser.ActiveProject_Fk);

            if (model == null)
                return NotFound();

            return View(model);
        }

        public IActionResult AddActionToUser(int actionId, string userId)
        {
            var action = _actionUoW.GetActionById(actionId);
            var user = _actionUoW.GetUserById(userId);

            if (action == null || user == null)
                return NotFound();

            action.Users.Add(user);
            user.Actions.Add(action);

            _actionUoW.UpdateAction(action);
            _actionUoW.UpdateUser(user);

            _actionUoW.SaveChanges();

            return RedirectToAction(nameof(OpenUsers), new { id = actionId });
        }

        public IActionResult RemoveActionFromUser(int actionId, string userId)
        {
            var action = _actionUoW.GetActionById(actionId);
            var user = _actionUoW.GetUserById(userId);

            if (action == null || user == null)
                return NotFound();

            action.Users.Remove(user);
            user.Actions.Remove(action);

            _actionUoW.UpdateAction(action);
            _actionUoW.UpdateUser(user);

            _actionUoW.SaveChanges();

            return RedirectToAction(nameof(OpenUsers), new { id = actionId });
        }
    }
}
