using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using WebAppointments.BusinessLogic.IService;
using WebAppointments.Models;

namespace WebAppointments.Controllers
{
    public class ReportsController : Controller
    {
        private readonly IReportLogic _reportLogic;
        private readonly IVisitSettingsLogic _visitSettingLogic;
        public ReportsController(IReportLogic reportLogic, IVisitSettingsLogic visitSettingLogic)
        {
            _reportLogic = reportLogic;
            _visitSettingLogic = visitSettingLogic;
        }
        public IList<ReportViewModel> ParticipantVisits { get; set; } = new List<ReportViewModel>();
        public IActionResult Index(DateTime? fromVisitDate, DateTime? toVisitDate, int? visitSettingId, int? participantId)
        {
            if (User.IsInRole("Super Admin") || User.IsInRole("admin"))
            {
                ViewData["VisitSetting"] = new SelectList(_visitSettingLogic.GetVisitSettings().Result, "Id", "VisitType");
            }
            else
            {
                ViewData["VisitSetting"] = new SelectList(_visitSettingLogic.GetVisitSettings().Result.FindAll(v => v.Id <= 9), "Id", "VisitType");
            }

            if (User.IsInRole("Super Admin") || User.IsInRole("admin"))
                return View(_reportLogic.GetReportData(fromVisitDate, toVisitDate, visitSettingId, participantId));

            var reportVM = _reportLogic.GetReportData(fromVisitDate, toVisitDate, visitSettingId, participantId);
            var visits = reportVM.Visits.Where(v => v.VisitSettingId <= 9).ToList();

            reportVM.Visits = visits;

            return View(reportVM);
        }

        public IActionResult ScheduledVisits(DateTime? fromVisitDate, DateTime? toVisitDate, int? visitSettingId)
        {
            if (User.IsInRole("Super Admin") || User.IsInRole("admin"))
            {
                ViewData["VisitSetting"] = new SelectList(_visitSettingLogic.GetVisitSettings().Result, "Id", "VisitType");
            }
            else
            {
                ViewData["VisitSetting"] = new SelectList(_visitSettingLogic.GetVisitSettings().Result.FindAll(v => v.Id <= 9), "Id", "VisitType");
            }

            if (User.IsInRole("Super Admin") || User.IsInRole("admin"))
                return View(_reportLogic.GetScheduledVisits(fromVisitDate, toVisitDate, visitSettingId));

            var reportVM = _reportLogic.GetScheduledVisits(fromVisitDate, toVisitDate, visitSettingId);
            var visits = reportVM.Visits.Where(v => v.VisitSettingId <= 9).ToList();

            reportVM.Visits = visits;

            return View(reportVM);
        }

        public IActionResult RetentionRate()
        {
            if (User.IsInRole("Super Admin") || User.IsInRole("admin"))
            {
                ViewData["VisitSetting"] = new SelectList(_visitSettingLogic.GetVisitSettings().Result, "Id", "VisitType");
            }
            else
            {
                ViewData["VisitSetting"] = new SelectList(_visitSettingLogic.GetVisitSettings().Result.FindAll(v => v.Id <= 9), "Id", "VisitType");
            }

            return View(_reportLogic.GetRetentionRate());
        }

        public ActionResult<PSFVisitsViewModel> PSFReport(DateTime? fromVisitDate, DateTime? toVisitDate, int? visitSettingId, int status = 1)
        {
            if (User.IsInRole("Super Admin") || User.IsInRole("admin"))
                ViewData["VisitSetting"] = new SelectList(_visitSettingLogic.GetVisitSettings().Result.FindAll(v => v.Id > 9), "Id", "VisitType");

            if (User.IsInRole("Super Admin") || User.IsInRole("admin"))
            {
                var psfReport = _reportLogic.GetPSFReport(status, fromVisitDate, toVisitDate);
                psfReport.Status = status;
                psfReport.FromVisitDate = fromVisitDate;
                psfReport.ToVisitDate = toVisitDate;

                if (visitSettingId != null)
                {
                    psfReport.PSFVisits = psfReport.PSFVisits.Where(s => s.VisitSettingId == visitSettingId.Value).ToList();
                }

                return View(psfReport);
            }

            return View(Unauthorized());
        }
    }
}
