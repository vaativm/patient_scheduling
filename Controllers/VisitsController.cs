using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppointments.BusinessLogic.Entity;
using WebAppointments.BusinessLogic.IService;
using WebAppointments.Models;

namespace WebAppointments.Controllers
{
    public class VisitsController : Controller
    {
        private readonly IVisitLogic _visitLogic;
        private readonly IParticipantLogic _participantLogic;
        private readonly IVisitSettingsLogic _visitSettingLogic;

        public VisitsController(IVisitLogic visitLogic, IParticipantLogic participantLogic, IVisitSettingsLogic visitSettingLogic)
        {
            _visitLogic = visitLogic;
            _participantLogic = participantLogic;
            _visitSettingLogic = visitSettingLogic;
        }

        // GET: Visits
        public async Task<IActionResult> Index()
        {
            var visits = await _visitLogic.GetVisits();
            if (User.IsInRole("Super Admin") || User.IsInRole("admin"))
                return View(visits.OrderBy(v => v.Participant.StudyId).ToList());

            var regularVisits = visits.Where(v => v.VisitSettingId <= 9).OrderBy(v => v.Participant.StudyId).ToList();

            return View(regularVisits);
        }

        // GET: Visits/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visits = await _visitLogic.GetVisits((int)id);

            if (visits == null)
            {
                return NotFound();
            }

            return View(visits);
        }

        // GET: Visits/Create
        public IActionResult Create()
        {
            ViewData["Participant"] = new SelectList(_participantLogic.GetParticipants().Result, "Id", "StudyId");

            if (User.IsInRole("Super Admin") || User.IsInRole("admin"))
            {
                ViewData["VisitSetting"] = new SelectList(_visitSettingLogic.GetVisitSettings().Result, "Id", "VisitType");
            }
            else
            {
                ViewData["VisitSetting"] = new SelectList(_visitSettingLogic.GetVisitSettings().Result.FindAll(v => v.Id <= 9), "Id", "VisitType");
            }
            return View();
        }
        public IActionResult CreatePSF()
        {
            ViewData["Participant"] = new SelectList(_participantLogic.GetParticipants().Result, "Id", "StudyId");


            if (User.IsInRole("Super Admin") || User.IsInRole("admin"))
            {
                ViewData["VisitSetting"] = new SelectList(_visitSettingLogic.GetVisitSettings().Result, "Id", "VisitType");
            }
            else
            {
                ViewData["VisitSetting"] = new SelectList(_visitSettingLogic.GetVisitSettings().Result.FindAll(v => v.Id <= 9), "Id", "VisitType");
            }
            return View();
        }
        public ActionResult<Visits> LoadExpectedVisit(int participantId)
        {
            var visit = _visitLogic.LoadExpectedVisit(participantId);

            return visit;
        }

        public ActionResult<Visits> LoadExpectedPSFVisit(int participantId)
        {
            var visit = new Visits();
            var expectedPSFVisit = _visitLogic.LoadExpectedPSFVisit(participantId);

            if (expectedPSFVisit != null)
            {
                visit.VisitDate = expectedPSFVisit.ExpectedDate;
                visit.VisitSettingId = expectedPSFVisit.VisitStage;
                visit.ParticipantId = expectedPSFVisit.ParticipantId;
                visit.NextAppointment = _visitLogic.GetNextPSFAppointmentDate(expectedPSFVisit.VisitStage, expectedPSFVisit.StudyId);
            }

            return visit;
        }

        public ActionResult<List<ParticipantVisitSchedule>> GetScheduledVisitToEdit(int participantId)
        {
            var visit = _visitLogic.GetPatientSchedule(participantId);

            return visit;
        }

        // POST: Visits/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VisitDate,ParticipantId,VisitSettingId,NextAppointment,ParticipantCame,VisitOutcome, OutcomeComment, DateParticipantCame")] Visits visits)
        {

            var isPSF = _visitLogic.IsPSF(visits.ParticipantId);
            string studyId = _visitLogic.GetStudyId(visits.ParticipantId);

            if (!string.IsNullOrWhiteSpace(studyId) && visits.VisitSettingId == 2 && visits.ParticipantCame == 1)
            {
                foreach (var schedule in _visitLogic.GenerateParticipantSchedule(visits.DateParticipantCame.Value, studyId))
                {
                    if (!_visitLogic.ScheduleExists(studyId, schedule.VisitStage))
                        await _visitLogic.AddSchedule(schedule);
                }

                if (isPSF)
                {
                    var psfSchedules = _visitLogic.CreatePSFSchedule(studyId);

                    await _visitLogic.AddPSFSchedule(psfSchedules);
                }
            }

            if (visits.VisitSettingId > 9)
            {
                var expectedVisit = _visitLogic.LoadExpectedPSFVisit(visits.ParticipantId);
            }
            else
            {
                var expectedVisit = _visitLogic.LoadExpectedVisit(visits.ParticipantId);
            }

            //if (!isPSF)
            //    ModelState.AddModelError("ParticipantId", "Participant not a PSF");

            if (ModelState.IsValid)
            {
                visits.CreateDate = DateTime.Now;
                await _visitLogic.AddVisits(visits);

                return RedirectToAction(nameof(Index));
            }
            if (User.IsInRole("Super Admin") || User.IsInRole("admin"))
            {
                ViewData["VisitSetting"] = new SelectList(_visitSettingLogic.GetVisitSettings().Result, "Id", "VisitType");
            }
            else
            {
                ViewData["VisitSetting"] = new SelectList(_visitSettingLogic.GetVisitSettings().Result.FindAll(v => v.Id <= 9), "Id", "VisitType");
            }

            ViewData["Participant"] = new SelectList(_participantLogic.GetParticipants().Result, "Id", "StudyId");

            return View(visits);
        }

        // GET: Visits/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visits = await _visitLogic.GetVisits((int)id);
            if (visits == null)
            {
                return NotFound();
            }
            ViewData["Participant"] = new SelectList(_participantLogic.GetParticipants().Result, "Id", "StudyId");

            if (User.IsInRole("Super Admin") || User.IsInRole("admin"))
            {
                ViewData["VisitSetting"] = new SelectList(_visitSettingLogic.GetVisitSettings().Result, "Id", "VisitType");
            }
            else
            {
                ViewData["VisitSetting"] = new SelectList(_visitSettingLogic.GetVisitSettings().Result.FindAll(v => v.Id <= 9), "Id", "VisitType");
            }
            return View(visits);
        }
        public async Task<IActionResult> EditPSF(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewData["IsPSF"] = _visitLogic.IsPSF(id.Value);
            var visits = await _visitLogic.GetVisits((int)id);

            if (visits == null)
            {
                return NotFound();
            }

            ViewData["Participant"] = new SelectList(_participantLogic.GetParticipants().Result, "Id", "StudyId");

            if (User.IsInRole("Super Admin") || User.IsInRole("admin"))
            {
                ViewData["VisitSetting"] = new SelectList(_visitSettingLogic.GetVisitSettings().Result, "Id", "VisitType");
            }
            else
            {
                ViewData["VisitSetting"] = new SelectList(_visitSettingLogic.GetVisitSettings().Result.FindAll(v => v.Id <= 9), "Id", "VisitType");
            }
            return View(visits);
        }
        // POST: Visits/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,VisitDate,ParticipantId,VisitSettingId,NextAppointment,ParticipantCame,VisitOutcome, OutcomeComment,DateParticipantCame")] Visits visits)
        {
            if (id != visits.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _visitLogic.UpdateVisit(visits);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_visitLogic.VisitExists(visits.Id))
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
            ViewData["Participant"] = new SelectList(_participantLogic.GetParticipants().Result, "Id", "StudyId");

            if (User.IsInRole("Super Admin") || User.IsInRole("admin"))
            {
                ViewData["VisitSetting"] = new SelectList(_visitSettingLogic.GetVisitSettings().Result, "Id", "VisitType");
            }
            else
            {
                ViewData["VisitSetting"] = new SelectList(_visitSettingLogic.GetVisitSettings().Result.FindAll(v => v.Id <= 9), "Id", "VisitType");
            }
            return View(visits);
        }

        // GET: Visits/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visits = await _visitLogic.GetVisits((int)id);
            if (visits == null)
            {
                return NotFound();
            }

            return View(visits);
        }

        // POST: Visits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var visits = await _visitLogic.GetVisits(id);
            visits.Deleted = 1;
            await _visitLogic.UpdateVisit(visits);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Schedulled()
        {
            var visits = _visitLogic.GetSchedulledVisits();
            return View(visits);
        }

        public IActionResult Missed()
        {
            var visits = _visitLogic.GetMissedVisits();

            return View(visits);
        }
    }
}