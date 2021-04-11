using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebAppointments.BusinessLogic.Entity;
using WebAppointments.BusinessLogic.IService;

namespace WebAppointments.Controllers
{
    [Authorize]
    public class VisitSettingsController : Controller
    {
        private readonly IVisitSettingsLogic _visitSettings;

        public VisitSettingsController(IVisitSettingsLogic visitSettings)
        {
            _visitSettings = visitSettings;
        }

        // GET: VisitSettings
        public async Task<IActionResult> Index()
        {
            return View(await _visitSettings.GetVisitSettings());
        }

        // GET: VisitSettings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visitSettings = await _visitSettings.GetVisitSettings((int)id);
            if (visitSettings == null)
            {
                return NotFound();
            }

            return View(visitSettings);
        }

        // GET: VisitSettings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VisitSettings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WindowPeriod,VisitStage, Medication")] VisitSettings visitSettings)
        {
            if (ModelState.IsValid)
            {
                await _visitSettings.AddVisitSettings(visitSettings);
                return RedirectToAction(nameof(Index));
            }
            return View(visitSettings);
        }

        // GET: VisitSettings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visitSettings = await _visitSettings.GetVisitSettings((int)id);
            if (visitSettings == null)
            {
                return NotFound();
            }
            return View(visitSettings);
        }

        // POST: VisitSettings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,WindowPeriod,VisitStage,Medication")] VisitSettings visitSettings)
        {
            if (id != visitSettings.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _visitSettings.UpdateVisitSettings(visitSettings);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_visitSettings.VisitSettingsExists(visitSettings.Id))
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
            return View(visitSettings);
        }

        // GET: VisitSettings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visitSettings = await _visitSettings.GetVisitSettings((int)id);
            if (visitSettings == null)
            {
                return NotFound();
            }

            return View(visitSettings);
        }

        // POST: VisitSettings/Delete/5
        // [HttpPost, ActionName("Delete")]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> DeleteConfirmed(int id)
        // {
        //     var visitSettings = await _context.VisitSettings.FindAsync(id);
        //     _context.VisitSettings.Remove(visitSettings);
        //     await _context.SaveChangesAsync();
        //     return RedirectToAction(nameof(Index));
        // }
    }
}
