using DataLayer.DbContexts;
using DataLayer.Models;
using DataLayer.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace PresentationLayer.Controllers
{
    public class IncidentsController : Controller
    {
        private readonly IdentityDbContext _context;
        private readonly IncidentRepository _incidentRepository;

        public IncidentsController(IdentityDbContext context)
        {
            _context = context;
            _incidentRepository = new IncidentRepository(context);
        }

        // GET: Incidents
        public IActionResult Index()
        {
            return View(_incidentRepository.GetAll().ToList());
        }

        // GET: Incidents/Details/5
        public IActionResult Details(int? id)
        {
            var incident = _incidentRepository.GetById(id);

            if (incident == null)
            {
                return NotFound();
            }

            return View(incident);
        }

        // GET: Incidents/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Incidents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,CodeName,Description,TargettedSystem,DateOfOcurence,User_Fk,Project_Fk,Level_Fk")] Incident incident)
        {
            if (ModelState.IsValid)
            {
                _incidentRepository.Insert(incident);
                _incidentRepository.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View(incident);
        }

        // GET: Incidents/Edit/5
        public IActionResult Edit(int? id)
        {
            var incident = _incidentRepository.GetById(id);

            if (incident == null)
                return NotFound();

            return View(incident);
        }

        // POST: Incidents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,CodeName,Description,TargettedSystem,DateOfOcurence,User_Fk,Project_Fk,Level_Fk")] Incident incident)
        {
            if (id != incident.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _incidentRepository.Update(incident);
                    _incidentRepository.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IncidentExists(incident.Id))
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

            return View(incident);
        }

        // GET: Incidents/Delete/5
        public IActionResult Delete(int? id)
        {
            var incident = _incidentRepository.GetById(id);

            if (incident == null)
                return NotFound();

            return View(incident);
        }

        // POST: Incidents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var incident = _incidentRepository.GetById(id);

            if (incident != null)
                _incidentRepository.Delete(incident);

            _incidentRepository.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        private bool IncidentExists(int id)
        {
            return _incidentRepository.GetById(id) != null;
        }
    }
}
