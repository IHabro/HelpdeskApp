using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataLayer.DbContexts;
using DataLayer.Models;
using DataLayer.Repositories;

namespace PresentationLayer.Controllers
{
    public class ProjectRolesController : Controller
    {
        private RoleRepository _roleRepository;

        public ProjectRolesController(IdentityDbContext context)
        {
            _roleRepository = new RoleRepository(context);
        }

        // GET: ProjectRoles
        public async Task<IActionResult> Index()
        {
            return View(_roleRepository.GetAll().ToList());
        }

        // GET: ProjectRoles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = _roleRepository.GetById(id);
            if (role == null)
            {
                return NotFound();
            }

            return View(role);
        }

        // GET: ProjectRoles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProjectRoles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description")] ProjectRole projectRole)
        {
            if (ModelState.IsValid)
            {
                _roleRepository.Insert(projectRole);
                _roleRepository.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View(projectRole);
        }

        // GET: ProjectRoles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectRole = _roleRepository.GetById(id);
            if (projectRole == null)
            {
                return NotFound();
            }

            return View(projectRole);
        }

        // POST: ProjectRoles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description")] ProjectRole projectRole)
        {
            if (id != projectRole.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _roleRepository.Update(projectRole);
                    _roleRepository.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectRoleExists(projectRole.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(projectRole);
        }

        // GET: ProjectRoles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectRole = _roleRepository.GetById(id);
            if (projectRole == null)
            {
                return NotFound();
            }

            return View(projectRole);
        }

        // POST: ProjectRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var projectRole = _roleRepository.GetById(id);

            if (projectRole != null)
            {
                _roleRepository.Delete(projectRole);
            }

            _roleRepository.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectRoleExists(int id)
        {
            return _roleRepository.GetById(id) != null;
        }
    }
}
