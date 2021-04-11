using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WebAppointments.BusinessLogic.Entity;
using WebAppointments.BusinessLogic.IService;
using WebAppointments.Models;

namespace WebAppointments.BusinessLogic.Service
{
    public class ReportLogic : IReportLogic
    {
        private readonly SmokingStudyDbContext _smokingStudyDbContext = new SmokingStudyDbContext();
        public List<AttendanceRegisterViewModel> RetentionRates { get; set; }

        public ReportViewModel GetReportData(DateTime? fromVisitDate, DateTime? toVisitDate, int? visitSettingId, int? participantId)
        {
            IList<Visits> visits = new List<Visits>();


            visits = _smokingStudyDbContext.Visits.Where(v =>
                (!fromVisitDate.HasValue || v.VisitDate.Date >= fromVisitDate) &&
                (!toVisitDate.HasValue || v.VisitDate.Date <= toVisitDate) &&
                (!visitSettingId.HasValue || v.VisitSetting.Id == visitSettingId) &&
                (!participantId.HasValue || v.Participant.Id == participantId))
            .Include(p => p.Participant)
            .Include(s => s.VisitSetting)
            .ToList();


            var reportVM = new ReportViewModel();

            //reportVM.VisitTypes = new SelectList(GetVisitSettingSL(), "VisitSettingId", "VisitType");
            reportVM.Participants = new SelectList(GetParticipantsSL(), "ParticipantId", "StudyId");

            foreach (var visit in visits)
            {
                var vm = new VisitsViewModel()
                {
                    StudyId = visit.Participant.StudyId,
                    VisitDate = visit.VisitDate,
                    VisitType = visit.VisitSetting.VisitType,
                    VisitId = visit.Id,
                    VisitSettingId = visit.VisitSettingId
                };

                reportVM.Visits.Add(vm);
            }

            return reportVM;
        }
        private IList<ParticipantsSLViewModel> GetParticipantsSL()
        {
            return _smokingStudyDbContext.Participants.Select(s =>
            new ParticipantsSLViewModel()
            {
                ParticipantId = s.Id,
                StudyId = s.StudyId
            }).ToList();
        }

        public List<VisitsViewModel> LoadScheduledVisits()
        {
            var scheduledVisits = _smokingStudyDbContext.Schedules
                .Where(s => s.WindowStart.Value.Date >= DateTime.Now.Date);

            var scheduled_Visits = _smokingStudyDbContext.Visits
                .Where(v => v.NextAppointment >= DateTime.Now.Date && v.Deleted != 1);

            var scheduledVisitsVM = new List<VisitsViewModel>();

            foreach (var scheduledVisit in scheduledVisits)
            {
                var nextVisitStage = scheduledVisit.VisitStage + 1;
                var participantId = _smokingStudyDbContext.Participants.Where(p => p.StudyId == scheduledVisit.studyId).FirstOrDefault().Id;

                var scheduledVisitVM = new VisitsViewModel();

                scheduledVisitVM.StudyId = scheduledVisit.studyId;
                scheduledVisitVM.Medication = _smokingStudyDbContext.VisitSettings.Where(v => v.VisitStage == scheduledVisit.VisitStage).FirstOrDefault().Medication;
                scheduledVisitVM.VisitType = _smokingStudyDbContext.VisitSettings.Where(v => v.VisitStage == scheduledVisit.VisitStage).FirstOrDefault().VisitType;
                scheduledVisitVM.VisitDate = scheduledVisit.WindowStart.Value.Date;
                scheduledVisitVM.VisitId = scheduledVisit.VisitStage;

                scheduledVisitsVM.Add(scheduledVisitVM);
            }

            foreach (var visit in scheduled_Visits)
            {
                var sturdyId = GetParticipantStudyId(visit.ParticipantId);
                var visitSettingId = visit.VisitSettingId;
                int nextVisitSettingId = 0;

                if (visitSettingId <= 9)
                {
                    nextVisitSettingId = visitSettingId + 1;

                    if (nextVisitSettingId > 9)
                        continue;

                    var exists = scheduledVisitsVM.Where(v => v.StudyId == sturdyId && v.VisitId == nextVisitSettingId).FirstOrDefault();

                    if (exists != null)
                    {
                        continue;
                    }

                    var scheduledVisitVM = new VisitsViewModel();

                    scheduledVisitVM.StudyId = sturdyId;
                    scheduledVisitVM.Medication = _smokingStudyDbContext.VisitSettings.Where(v => v.VisitStage == nextVisitSettingId).FirstOrDefault().Medication;
                    scheduledVisitVM.VisitType = _smokingStudyDbContext.VisitSettings.Where(v => v.VisitStage == nextVisitSettingId).FirstOrDefault().VisitType;
                    scheduledVisitVM.VisitDate = visit.NextAppointment.Value;
                    scheduledVisitVM.VisitId = nextVisitSettingId;

                    scheduledVisitsVM.Add(scheduledVisitVM);
                }
                else
                {
                    nextVisitSettingId = visitSettingId + 1;

                    if (nextVisitSettingId > 17)
                        continue;

                    var exists = scheduledVisitsVM.Where(v => v.StudyId == sturdyId && v.VisitId == nextVisitSettingId).FirstOrDefault();

                    if (exists != null)
                        continue;

                    var scheduledVisitVM = new VisitsViewModel();

                    scheduledVisitVM.StudyId = sturdyId;
                    scheduledVisitVM.Medication = _smokingStudyDbContext.VisitSettings.Where(v => v.VisitStage == nextVisitSettingId).FirstOrDefault().Medication;
                    scheduledVisitVM.VisitType = _smokingStudyDbContext.VisitSettings.Where(v => v.VisitStage == nextVisitSettingId).FirstOrDefault().VisitType;
                    scheduledVisitVM.VisitDate = visit.NextAppointment.Value;
                    scheduledVisitVM.VisitId = nextVisitSettingId;

                    scheduledVisitsVM.Add(scheduledVisitVM);
                }
            }

            return scheduledVisitsVM;
        }
        public ReportViewModel GetScheduledVisits(DateTime? fromVisitDate, DateTime? toVisitDate, int? visitSettingId)
        {
            var scheduledVisits = new List<VisitsViewModel>();

            if (fromVisitDate.HasValue)
            {
                fromVisitDate = fromVisitDate < DateTime.Now.Date ? DateTime.Now.Date : fromVisitDate;
            }
            else
            {
                fromVisitDate = DateTime.Now.Date;
            }

            if (toVisitDate.HasValue)
            {
                toVisitDate = toVisitDate < DateTime.Now.Date ? DateTime.Now.Date : toVisitDate;
            }

            //var scheduledVisitsQueryable = LoadScheduledVisits().AsQueryable();

            //if(fromVisitDate.HasValue)
            //    scheduledVisitsQueryable = scheduledVisitsQueryable.Where(v=>v.VisitDate >= fromVisitDate.Value.Date)
            scheduledVisits = LoadScheduledVisits().Where(v =>
                (!fromVisitDate.HasValue || v.VisitDate.Date >= fromVisitDate.Value.Date) &&
                (!toVisitDate.HasValue || v.VisitDate.Date <= toVisitDate.Value.Date) &&
                (!visitSettingId.HasValue || v.VisitId == visitSettingId)).ToList();

            var reportVM = new ReportViewModel();

            reportVM.Visits = scheduledVisits;

            return reportVM;
        }

        public int? GetParticipantId(string studyId)
        {
            var participant = _smokingStudyDbContext.Participants.Where(p => p.StudyId == studyId).FirstOrDefault();

            if (participant != null)
                return participant.Id;

            return null;
        }

        public string GetParticipantStudyId(int participantId)
        {
            var participant = _smokingStudyDbContext.Participants.Where(p => p.Id == participantId).FirstOrDefault();

            if (participant != null)
                return participant.StudyId;

            return string.Empty;
        }
        public List<AttendanceRegisterViewModel> GetRetentionRate()
        {
            RetentionRates = new List<AttendanceRegisterViewModel>();

            var qualifiedVisits = _smokingStudyDbContext.Schedules.Where(v => v.WindowEnd.Value.Date < DateTime.Now.Date);
            var visits = _smokingStudyDbContext.Visits;
            var visitSettings = _smokingStudyDbContext.VisitSettings.Select(m => new { m.VisitType, m.VisitStage }).OrderBy(m => m.VisitStage).
                Where(s => s.VisitStage <= 9).ToList();

            var visitSettingsGroups = qualifiedVisits.ToList().GroupBy(q => q.VisitStage);

            foreach (var visitSetting in visitSettings)
            {
                RetentionRates.Add(new AttendanceRegisterViewModel
                {
                    VisitSettingId = visitSetting.VisitStage.Value,
                    VisitStage = visitSetting.VisitType
                });
            }

            foreach (var item in RetentionRates)
            {
                var group = visitSettingsGroups.Where(g => g.Key == item.VisitSettingId).FirstOrDefault();

                if (group != null)
                {
                    item.Expected = group.Count();
                }
            }

            var attendance = new List<AttendanceVisitRegister>();

            foreach (var qualifiedVisit in qualifiedVisits)
            {
                var particpantId = GetParticipantId(qualifiedVisit.studyId);

                if (particpantId != null)
                {
                    var visit = visits.Where(v => v.ParticipantId == particpantId && v.VisitSettingId == qualifiedVisit.VisitStage).FirstOrDefault();

                    if (visit != null)
                    {
                        if (visit.ParticipantCame == 1 && visit.VisitOutcome == 1)
                        {
                            attendance.Add(new AttendanceVisitRegister
                            {
                                VisitSetting = qualifiedVisit.VisitStage
                            });
                        }
                    }
                }
            }

            var attendanceGroups = attendance.GroupBy(g => g.VisitSetting);

            foreach (var item in RetentionRates)
            {
                var group = attendanceGroups.Where(g => g.Key == item.VisitSettingId).FirstOrDefault();

                if (group != null)
                {
                    item.Atttended = group.Count();
                }
            }

            foreach (var item in RetentionRates)
            {
                if (item.Expected > 0)
                    item.RetentionRate = (item.Atttended / Convert.ToDecimal(item.Expected)) * 100;
            }

            return RetentionRates;
        }

        private int? GetVisitOutcome(int participantId, int visitStage)
        {
            var visit = _smokingStudyDbContext.Visits.Where(v => v.ParticipantId == participantId && v.VisitSettingId == visitStage).FirstOrDefault();

            if (visit != null)
                return visit.VisitOutcome;

            return null;
        }

        private PSFVisitsViewModel GetCompletedPSFVisitsSummary(DateTime? fromVisitDate, DateTime? toVisitDate)
        {
            var psfVisitStatuses = new PSFVisitsViewModel();
            var psfVisits = _smokingStudyDbContext.Visits
                .Where(
                v =>
                (!fromVisitDate.HasValue || v.VisitDate.Date >= fromVisitDate) && (!toVisitDate.HasValue || v.VisitDate.Date <= toVisitDate)
                && v.VisitSettingId >= 10 && v.VisitSettingId <= 17 && v.ParticipantCame == 1 && v.Deleted == 0)
                .GroupBy(v => v.VisitSettingId)
                .Select(v => new { visitSettingId = v.Key, count = v.Count() });

            var visitStages = _smokingStudyDbContext.VisitSettings.Where(s => s.VisitStage.Value >= 10 && s.VisitStage.Value <= 17)
                .OrderBy(s => s.VisitStage).ToList();

            foreach (var visitstage in visitStages)
            {
                int count = 0;
                var group = psfVisits.Where(g => g.visitSettingId == visitstage.VisitStage.Value).FirstOrDefault();

                if (group != null)
                    count = group.count;

                var psfVisit = new PSFVisitStatusViewModel
                {
                    VisitSettingId = visitstage.VisitStage.Value,
                    VisitStage = visitstage.VisitType,
                    Count = count
                };

                psfVisitStatuses.PSFVisits.Add(psfVisit);
            }

            return psfVisitStatuses;
        }

        private bool IsCompleted(int participantId, int visitSetting)
        {
            var exists = _smokingStudyDbContext.Visits
                .Where(v => v.ParticipantId == participantId && v.VisitSettingId == visitSetting && v.ParticipantCame == 1 && v.VisitOutcome == 1 && v.Deleted == 0).FirstOrDefault();

            if (exists == null)
                return false;
            else
                return true;
        }
        private PSFVisitsViewModel GetUnCompletedPSFVisits(DateTime? fromVisitDate, DateTime? toVisitDate)
        {
            var psfVisits = new PSFVisitsViewModel();
            var psfVisitStatuses = new List<PSFVisitStatusViewModel>();
            var psfSchedules = _smokingStudyDbContext.PSFSchedules
                .Where(v =>
                (!fromVisitDate.HasValue || v.ExpectedDate.Date >= fromVisitDate) && (!toVisitDate.HasValue || v.ExpectedDate.Date <= toVisitDate));

            foreach (var visit in psfSchedules)
            {
                if (!IsCompleted(visit.ParticipantId, visit.VisitStage))
                {
                    var inCompleteVisit = new PSFVisitStatusViewModel
                    {
                        VisitSettingId = visit.VisitStage
                    };
                    psfVisitStatuses.Add(inCompleteVisit);
                }
            }

            var unCompletedPSFVisits = psfVisitStatuses.GroupBy(v => v.VisitSettingId)
                .Select(v => new { visitSettingId = v.Key, count = v.Count() });

            var visitStages = _smokingStudyDbContext.VisitSettings.Where(s => s.VisitStage.Value >= 10 && s.VisitStage.Value <= 17)
                .OrderBy(s => s.VisitStage).ToList();

            foreach (var visitstage in visitStages)
            {
                int count = 0;
                var group = unCompletedPSFVisits.ToList().Where(g => g.visitSettingId == visitstage.VisitStage.Value).FirstOrDefault();
                if (group != null)
                    count = group.count;

                var psfVisit = new PSFVisitStatusViewModel
                {
                    VisitSettingId = visitstage.VisitStage.Value,
                    VisitStage = visitstage.VisitType,
                    Count = count
                };

                psfVisits.PSFVisits.Add(psfVisit);
            }

            return psfVisits;
        }

        public PSFVisitsViewModel GetMissedPSFVisitsSummary(DateTime? fromVisitDate, DateTime? toVisitDate)
        {
            var missedPSFVisits = GetMissedPSFVisits().Where(v => (!fromVisitDate.HasValue || v.VisitDate.Date >= fromVisitDate.Value) &&
            (!toVisitDate.HasValue || v.VisitDate.Date <= toVisitDate.Value));

            var missedSessionsSummary = missedPSFVisits.GroupBy(v => v.VisitSettingId).Select(g => new { visitSettingId = g.Key, count = g.Count() }).ToList();
            var visitStages = _smokingStudyDbContext.VisitSettings.Where(s => s.VisitStage.Value >= 10 && s.VisitStage.Value <= 17)
                .OrderBy(s => s.VisitStage).ToList();
            var psfVisits = new PSFVisitsViewModel();

            foreach (var visitstage in visitStages)
            {
                int count = 0;
                var group = missedSessionsSummary.ToList().Where(g => g.visitSettingId == visitstage.VisitStage.Value).FirstOrDefault();
                if (group != null)
                    count = group.count;

                var psfVisit = new PSFVisitStatusViewModel
                {
                    VisitSettingId = visitstage.VisitStage.Value,
                    VisitStage = visitstage.VisitType,
                    Count = count
                };

                psfVisits.PSFVisits.Add(psfVisit);
            }

            return psfVisits;
        }

        public PSFVisitsViewModel GetPendingPSFVisitsSummary(DateTime? fromVisitDate, DateTime? toVisitDate)
        {
            var missedPSFVisits = GetPendingPSFVisits().Where(v => (!fromVisitDate.HasValue || v.VisitDate.Date >= fromVisitDate.Value) &&
            (!toVisitDate.HasValue || v.VisitDate.Date <= toVisitDate.Value));

            var pendingSessionsSummary = missedPSFVisits.GroupBy(v => v.VisitSettingId).Select(g => new { visitSettingId = g.Key, count = g.Count() }).ToList();
            var visitStages = _smokingStudyDbContext.VisitSettings.Where(s => s.VisitStage.Value >= 10 && s.VisitStage.Value <= 17)
                .OrderBy(s => s.VisitStage).ToList();
            var pendingPSFVisits = new PSFVisitsViewModel();

            foreach (var visitstage in visitStages)
            {
                int count = 0;
                var group = pendingSessionsSummary.ToList().Where(g => g.visitSettingId == visitstage.VisitStage.Value).FirstOrDefault();
                if (group != null)
                    count = group.count;

                var psfVisit = new PSFVisitStatusViewModel
                {
                    VisitSettingId = visitstage.VisitStage.Value,
                    VisitStage = visitstage.VisitType,
                    Count = count
                };

                pendingPSFVisits.PSFVisits.Add(psfVisit);
            }

            return pendingPSFVisits;
        }
        private List<ExpectedPSFVisit> GetPendingPSFVisits()
        {
            var pendingPSFVisits = new List<ExpectedPSFVisit>();

            var visitsExpectedToHaveBeenCompleted = GetScheduledPSFVisits().Where(v => DateTime.Now.Date <= v.VisitDate.Date.AddDays(7));

            foreach (var visit in visitsExpectedToHaveBeenCompleted)
            {
                if (!IsCompleted(visit.ParticipantId, visit.VisitSettingId))
                    pendingPSFVisits.Add(visit);
            }

            return pendingPSFVisits;
        }
        private List<ExpectedPSFVisit> GetMissedPSFVisits()
        {
            var missedPSFVisits = new List<ExpectedPSFVisit>();
            //var visitsExpectedToHaveBeenCompleted = GetExpectedPSFVisits().Where(v => v.VisitDate.Date < DateTime.Now.Date && v.VisitDate.Date.AddDays(7) < DateTime.Now.Date);

            //foreach (var visit in visitsExpectedToHaveBeenCompleted)
            //{
            //    if (!IsCompleted(visit.ParticipantId, visit.VisitSettingId))
            //        missedPSFVisits.Add(visit);
            //}
            var visitsScheduledToHaveBeenCompleted = GetScheduledPSFVisits().Where(v => v.VisitDate.Date < DateTime.Now.Date && v.VisitDate.Date.AddDays(7) < DateTime.Now.Date);
            
            foreach(var visit in visitsScheduledToHaveBeenCompleted)
            {
                if (!IsCompleted(visit.ParticipantId, visit.VisitSettingId))
                    missedPSFVisits.Add(visit);
            }

            return missedPSFVisits;
        }
        private int GetExpectedSession(int visitSettingId)
        {
            visitSettingId += 1;

            if (visitSettingId > 17)
                return 17;

            return visitSettingId;
        }

        private List<ExpectedPSFVisit> GetScheduledPSFVisits()
        {
            var psfVisits = _smokingStudyDbContext.PSFSchedules;

            var expectedPSFVisits = new List<ExpectedPSFVisit>();

            foreach (var psfVisit in psfVisits)
            {
                var expectedPSFVisit = new ExpectedPSFVisit
                {
                    VisitSettingId = psfVisit.VisitStage,
                    VisitDate = psfVisit.ExpectedDate,
                    ParticipantId = psfVisit.ParticipantId
                };

                expectedPSFVisits.Add(expectedPSFVisit);

            }

            return expectedPSFVisits;
        }
        private List<ExpectedPSFVisit> GetExpectedPSFVisits()
        {
            var psfVisits = _smokingStudyDbContext.Visits
                .Where(v => v.VisitSettingId >= 10 && v.VisitSettingId <= 17 && v.ParticipantCame == 1 && v.Deleted == 0);
            var expectedPSFVisits = new List<ExpectedPSFVisit>();

            foreach (var psfVisit in psfVisits)
            {
                var sessionId = GetExpectedSession(psfVisit.VisitSettingId);

                if (sessionId != 17)
                {
                    var expectedPSFVisit = new ExpectedPSFVisit
                    {
                        VisitSettingId = sessionId,
                        VisitDate = psfVisit.NextAppointment.Value,
                        ParticipantId = psfVisit.ParticipantId
                    };

                    expectedPSFVisits.Add(expectedPSFVisit);
                }
            }

            return expectedPSFVisits;
        }

        public PSFVisitsViewModel GetPSFReport(int status, DateTime? fromVisitDate, DateTime? toVisitDate)
        {
            if (status == 1)
                return GetCompletedPSFVisitsSummary(fromVisitDate, toVisitDate);
            else if (status == 2)
                return GetPendingPSFVisitsSummary(fromVisitDate, toVisitDate);
            else
                return GetMissedPSFVisitsSummary(fromVisitDate, toVisitDate);
        }
    }
}