using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebAppointments.BusinessLogic.Entity;
using WebAppointments.BusinessLogic.IService;

namespace WebAppointments.Controllers
{
    public class FacilitiesController : Controller
    {
        private readonly IFacilityLogic _facilityLogic;


        public FacilitiesController(IFacilityLogic facilityLogic)
        {
            _facilityLogic = facilityLogic;
        }

        // GET: Facilities
        public async Task<IActionResult> Index()
        {
            ViewBag.Message = TempData["Message"];

            return View(await _facilityLogic.GetFacilities());

        }

        // GET: Facilities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facilities = await _facilityLogic.GetFacilities((int)id);

            if (facilities == null)
            {
                return NotFound();
            }

            return View(facilities);
        }

        // GET: Facilities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Facilities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Mflcode,Name")] Facilities facilities)
        {
            if (ModelState.IsValid)
            {
                await _facilityLogic.AddFacilities(facilities);
                TempData["Message"] = "Facility Added Successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(facilities);
        }

        // GET: Facilities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var facilities = await _facilityLogic.GetFacilities((int)id);
            if (facilities == null)
            {
                return NotFound();
            }
            //ViewData["Facility"] = new SelectList(_facilityLogic.GetFacilities().Result, "Id", "Name");
            return View(facilities);
        }

        // POST: Facilities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Mflcode,Name")] Facilities facilities)
        {
            if (id != facilities.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _facilityLogic.UpdateFacilities(facilities);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_facilityLogic.FacilityExists(facilities.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["Message"] = "Facility Details Updated Successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(facilities);
        }

        // GET: Facilities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facilities = await _facilityLogic.GetFacilities((int)id);

            if (facilities == null)
            {
                return NotFound();
            }

            return View(facilities);
        }

        // POST: Facilities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var facilities = await _facilityLogic.GetFacilities(id);
            //modify the Database and model to add this column
            facilities.Deleted = 1;
            await _facilityLogic.UpdateFacilities(facilities);

            return RedirectToAction(nameof(Index));
        }

    }
}
