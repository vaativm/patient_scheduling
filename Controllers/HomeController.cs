using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using WebAppointments.BusinessLogic.IService;
using WebAppointments.Models;

namespace WebAppointments.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IParticipantLogic _paticipantLogic;
        private readonly IVisitLogic _visitLogic;
        private readonly IVisitSettingsLogic _visitSettingLogic;
        public HomeController(IParticipantLogic paticipantLogic, IVisitLogic visitLogic, IVisitSettingsLogic visitSettingLogic)
        {
            _paticipantLogic = paticipantLogic;
            _visitLogic = visitLogic;
            _visitSettingLogic = visitSettingLogic;
        }

        public IActionResult Index()
        {
            var dashView = new DashboardView
            {
                Participants = _paticipantLogic.GetParticipants().Result.Count(),
                Visits = _visitLogic.CountVisit(),
                Schedulled = _visitLogic.CountSchedulledVisits(),
                Missed = _visitLogic.GetMissedVisits().Count()
            };
            return View(dashView);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public BarChartData GetChartData()
        {
            var settings = _visitSettingLogic.GetVisitSettings().Result.Select(m => m?.VisitType).ToList();

            var visitsOrdered = _visitLogic.GetVisits().Result
               .GroupBy(m => new { m.VisitSetting }).Select(m => new { Label = m.Key.VisitSetting.VisitType, Data = m.Select(x => x.ParticipantId).Distinct().Count() });

            List<string> labels = new List<string>();
            List<double> data = new List<double>();

            foreach (var item in settings)
            {
                labels.Add(item);
                var d = visitsOrdered.FirstOrDefault(m => m.Label == item);
                data.Add(d != null ? d.Data : 0);
            }

            return new BarChartData
            {
                Labels = labels,
                Data = data
            };
        }
    }
}
