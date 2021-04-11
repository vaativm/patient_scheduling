using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppointments.BusinessLogic.Entity;
using WebAppointments.BusinessLogic.IService;
using WebAppointments.Models;

namespace WebAppointments.Controllers
{
    public class ParticipantsController : Controller
    {
        private readonly IParticipantLogic _paticipantLogic;
        private readonly IVisitSettingsLogic _visitSettingLogic;
        private readonly IFacilityLogic _facilityLogic;
        private readonly IVisitLogic _visitLogic;

        public ParticipantsController(IParticipantLogic paticipantLogic, IVisitSettingsLogic visitSettingLogic, IFacilityLogic facilityLogic, IVisitLogic visitLogic)
        {
            _paticipantLogic = paticipantLogic;
            _visitSettingLogic = visitSettingLogic;
            _facilityLogic = facilityLogic;
            _visitLogic = visitLogic;
        }

        public bool HasSchedule(string studyId)
        {
            var schedule = _visitLogic.GetSchedules().Where(s => s.studyId == studyId).FirstOrDefault();
            if (schedule != null)
                return true;
            return false;
        }
        // GET: Participants
        public async Task<IActionResult> Index()
        {
            ViewBag.Message = TempData["Message"];

            var participants = await _paticipantLogic.GetParticipants();

            foreach (var participant in participants)
            {
                if (!HasSchedule(participant.StudyId))
                {
                    if (participant.EnrollmentDate.HasValue)
                    {
                        var schedules = _visitLogic.GenerateParticipantSchedule(participant.EnrollmentDate.Value, participant.StudyId);

                        foreach (var schedule in schedules)
                        {
                            await _visitLogic.AddSchedule(schedule);
                        }
                    }
                    
                }
            }

            var studyIds = _visitLogic.GetExistingPSFParticipants();

            foreach (var studyId in studyIds)
            {
                if (studyId != null)
                {
                    var participantSchedule = _visitLogic.CreatePSFSchedule(studyId);

                    await _visitLogic.AddPSFSchedule(participantSchedule);
                }
            }

            return View(participants);

        }

        // GET: Participants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var participant = await _paticipantLogic.GetParticipants((int)id);
            if (participant == null)
            {
                return NotFound();
            }
            ViewData["Facility"] = new SelectList(_facilityLogic.GetFacilities().Result, "Id", "Name");
            return View(participant);
        }

        // GET: Participants/Create
        public IActionResult Create()
        {
            ViewData["Facility"] = new SelectList(_facilityLogic.GetFacilities().Result, "Id", "Name");
            return View();
        }

        // POST: Participants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudyId,FacilityId,Gender,EnrollmentDate,MobilePhone,AlternateMobilePhone,AlternateContactName,IsPSF")] Participants participant)
        {
            
            if (ModelState.IsValid)
            {
                await _paticipantLogic.AddParticipants(participant);
                TempData["Message"] = "Participant Added Successfully";

                return RedirectToAction(nameof(Index));
            }

            return View(participant);
        }

        // GET: Participants/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var participant = await _paticipantLogic.GetParticipants((int)id);
            if (participant == null)
            {
                return NotFound();
            }
            ViewData["Facility"] = new SelectList(_facilityLogic.GetFacilities().Result, "Id", "Name");
            return View(participant);
        }

        // POST: Participants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StudyId,FacilityId,Gender,EnrollmentDate,MobilePhone,AlternateMobilePhone,AlternateContactName,IsPSF")] Participants participant)
        {
            if (id != participant.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _paticipantLogic.UpdateParticipant(participant);

                    if (participant.IsPSF)
                    {
                        var psfSchedules = _visitLogic.CreatePSFSchedule(participant.StudyId);

                        await _visitLogic.AddPSFSchedule(psfSchedules);
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_paticipantLogic.ParticipantExists(participant.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["Message"] = "Participant Details Updated Successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(participant);
        }

        // GET: Participants/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var participant = await _paticipantLogic.GetParticipants((int)id);
            if (participant == null)
            {
                return NotFound();
            }

            return View(participant);
        }

        // // POST: Participants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var participant = await _paticipantLogic.GetParticipants(id);
            participant.Deleted = 1;
            await _paticipantLogic.UpdateParticipant(participant);
            return RedirectToAction(nameof(Index));
        }

        public ActionResult<List<ParticipantVisitSchedule>> GetPatientSchedule(int participantId)
        {
            var schedule = _visitLogic.GetPatientSchedule(participantId);
            return schedule;
        }

        public ActionResult<List<PSFScheduleViewModel>> GetPSFSchedule(int participantId)
        {
            var psfParticipantSchedule = _visitLogic.GetPSFSchedule(participantId);

            return psfParticipantSchedule;
        }

        [HttpGet]
        public IActionResult LogVisit(int id)
        {
            var visits = _visitLogic.LoadExpectedVisit(id);

            ViewData["Participant"] = new SelectList(_paticipantLogic.GetParticipants().Result, "Id", "StudyId");

            if (User.IsInRole("Super Admin") || User.IsInRole("admin"))
            {
                ViewData["VisitSetting"] = new SelectList(_visitSettingLogic.GetVisitSettings().Result, "Id", "VisitType", visits.VisitSettingId);
            }
            else
            {
                ViewData["VisitSetting"] = new SelectList(_visitSettingLogic.GetVisitSettings().Result.FindAll(v => v.Id <= 9), "Id", "VisitType", visits.VisitSettingId);
            }

            return View(visits);
        }

        [HttpGet]
        public IActionResult LogPSFVisit(int id)
        {
            var visit = new Visits();
            var expectedPSFVisit = _visitLogic.LoadExpectedPSFVisit(id);

            ViewData["IsPSF"] = _visitLogic.IsPSF(id);

            if (expectedPSFVisit != null)
            {
                visit.VisitDate = expectedPSFVisit.ExpectedDate;
                visit.VisitSettingId = expectedPSFVisit.VisitStage;
                visit.ParticipantId = expectedPSFVisit.ParticipantId;
                visit.NextAppointment = _visitLogic.GetNextPSFAppointmentDate(expectedPSFVisit.VisitStage, expectedPSFVisit.StudyId);

                if (User.IsInRole("Super Admin") || User.IsInRole("admin"))
                {
                    ViewData["VisitSetting"] = new SelectList(_visitSettingLogic.GetVisitSettings().Result, "Id", "VisitType", expectedPSFVisit.VisitStage);
                }
                else
                {
                    ViewData["VisitSetting"] = new SelectList(_visitSettingLogic.GetVisitSettings().Result.FindAll(v => v.Id <= 9), "Id", "VisitType", expectedPSFVisit.VisitStage);
                }
            }

            visit.ParticipantId = id;
            ViewData["Participant"] = new SelectList(_paticipantLogic.GetParticipants().Result, "Id", "StudyId");

            return View(visit);
        }
    }
}
