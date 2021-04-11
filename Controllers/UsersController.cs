using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebAppointments.BusinessLogic.IService;

namespace WebAppointments.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private IUserLogic _userLogic;

        public UsersController(IUserLogic userLogic)
        {
            _userLogic = userLogic;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Message = TempData["Message"];
            return View(await _userLogic.GetUsers());
        }
    }
}