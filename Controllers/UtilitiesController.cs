using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebAppointments.BusinessLogic.IService;

namespace WebAppointments.Controllers
{
    public class UtilitiesController : Controller
    {
        private readonly IVisitLogic _visitLogic;

        public UtilitiesController(IVisitLogic visitLogic)
        {
            _visitLogic = visitLogic;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create()
        {
            await _visitLogic.CreateScheduleForExistingVisits();

            TempData["Message"] = "Schedule added successfully";

            return RedirectToAction(nameof(Index));
        }
    }
}
