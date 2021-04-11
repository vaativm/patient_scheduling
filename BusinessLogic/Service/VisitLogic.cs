using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using WebAppointments.BusinessLogic.Entity;
using WebAppointments.BusinessLogic.IService;
using WebAppointments.Models;

namespace WebAppointments.BusinessLogic.Service
{
    public class VisitLogic : IVisitLogic
    {
        private readonly SmokingStudyDbContext context = new SmokingStudyDbContext();

        public async Task<List<Visits>> GetVisits()
        {
            return await context.Visits.Where(m => m.Deleted != 1)
            .Include(v => v.Participant)
            .Include(v => v.VisitSetting).ToListAsync();
        }
        public async Task<Visits> GetVisits(int id)
        {
            return await context.Visits
                .Include(v => v.Participant)
                .Include(v => v.VisitSetting)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<int> AddVisits(Visits visit)
        {
            context.Add(visit);
            return await context.SaveChangesAsync();
        }

        public async Task<int> UpdateVisit(Visits visit)
        {
            var existing = context.Visits.FirstOrDefault(m => m.Id == visit.Id);
            visit.CreateDate = existing.CreateDate;
            context.Entry(existing).CurrentValues.SetValues(visit);
            //context.Update(visit);
            return await context.SaveChangesAsync();
        }

        public bool VisitExists(int id)
        {
            return context.Visits.Any(e => e.Id == id);
        }

        public int CountVisit()
        {
            return context.Visits.Count(m => m.Deleted != 1);
        }
        public static int GetIso8601WeekOfYear(DateTime date)
        {
            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(date);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                date = date.AddDays(3);
            }

            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }
        public int CountSchedulledVisits()
        {
            return GetSchedulledVisits().Count();
        }

        public int CountOverDueAppointment()
        {
            return context.Visits.Count(m => m.NextAppointment > DateTime.Today && m.Deleted != 1 && m.ParticipantCame != 1);
        }

        public int ParticipantCame(int participantId, int visitStage)
        {
            var visit = context.Visits.Where(v => v.ParticipantId == participantId && v.VisitSettingId == visitStage).FirstOrDefault();
            var participantCame = 2;

            if (visit != null)
                participantCame = visit.ParticipantCame;

            return participantCame;
        }

        public DateTime? DateParticipantCame(int participantId, int visitStage)
        {
            var visit = context.Visits.Where(v => v.ParticipantId == participantId && v.VisitSettingId == visitStage).FirstOrDefault();
            var participantCame = 2;

            if (visit != null)
                participantCame = visit.ParticipantCame;

            if (participantCame == 1)
                return visit.DateParticipantCame;

            return null;
        }
        public List<ScheduledVisitsViewModel> GetSchedulledVisits()
        {
            var curentWeek = GetIso8601WeekOfYear(DateTime.Now.Date);

            if (DateTime.Now.Date.DayOfWeek == DayOfWeek.Sunday)
            {
                curentWeek += 1;
            }

            var scheduledVisits = context.Schedules;
            var scheduledVisitsVM = new List<ScheduledVisitsViewModel>();
            var schedules = new List<Schedule>();

            foreach (var scheduledVisit in scheduledVisits)
            {
                var visitdateWeek = GetIso8601WeekOfYear(scheduledVisit.WindowStart.Value.Date);
                if (visitdateWeek == curentWeek)
                {
                    var nextVisitStage = scheduledVisit.VisitStage + 1;
                    var participantId = context.Participants.Where(p => p.StudyId == scheduledVisit.studyId).FirstOrDefault().Id;
                    var participantCame = ParticipantCame(participantId, scheduledVisit.VisitStage);

                    var scheduledVisitVM = new ScheduledVisitsViewModel();

                    scheduledVisitVM.StudyId = scheduledVisit.studyId;
                    scheduledVisitVM.ParticipantId = context.Participants.Where(p => p.StudyId == scheduledVisit.studyId).FirstOrDefault().Id;
                    scheduledVisitVM.VisitDate = scheduledVisit.WindowStart.Value;
                    scheduledVisitVM.VisitStage = context.VisitSettings.Where(v => v.VisitStage == scheduledVisit.VisitStage).FirstOrDefault().VisitType;

                    scheduledVisitsVM.Add(scheduledVisitVM);
                }
            }

            var scheduled_Visits = context.Visits
                .Where(v => v.NextAppointment >= DateTime.Now.Date && v.Deleted != 1);

            foreach (var visit in scheduled_Visits)
            {
                var visitdateWeek = GetIso8601WeekOfYear(visit.NextAppointment.Value.Date);
                var visitSettingId = visit.VisitSettingId;

                if (visitdateWeek == curentWeek)
                {
                    if (visitSettingId <= 9)
                    {
                        var nextVisitStage = visitSettingId + 1;

                        if (nextVisitStage > 9)
                            continue;

                        var studyId = GetStudyId(visit.ParticipantId);

                        var participantCame = ParticipantCame(visit.ParticipantId, nextVisitStage);

                        var scheduledVisitVM = new ScheduledVisitsViewModel();

                        scheduledVisitVM.StudyId = studyId;
                        scheduledVisitVM.ParticipantId = visit.ParticipantId;

                        if (nextVisitStage != 9)
                            scheduledVisitVM.VisitDate = visit.NextAppointment.Value;

                        scheduledVisitVM.VisitStage = context.VisitSettings.Where(v => v.VisitStage == nextVisitStage).FirstOrDefault().VisitType;

                        scheduledVisitsVM.Add(scheduledVisitVM);
                    }
                    else
                    {
                        var nextVisitStage = visitSettingId + 1;

                        if (nextVisitStage > 17)
                            continue;

                        var studyId = GetStudyId(visit.ParticipantId);

                        var participantCame = ParticipantCame(visit.ParticipantId, nextVisitStage);

                        var scheduledVisitVM = new ScheduledVisitsViewModel();

                        scheduledVisitVM.StudyId = studyId;
                        scheduledVisitVM.ParticipantId = visit.ParticipantId;

                        if (nextVisitStage != 17)
                            scheduledVisitVM.VisitDate = visit.NextAppointment.Value;

                        scheduledVisitVM.VisitStage = context.VisitSettings.Where(v => v.VisitStage == nextVisitStage).FirstOrDefault().VisitType;

                        scheduledVisitsVM.Add(scheduledVisitVM);
                    }
                }
            }

            return scheduledVisitsVM;
        }

        public async Task<int> AddSchedule(Schedule schedule)
        {
            context.Add(schedule);
            return await context.SaveChangesAsync();
        }

        public List<Schedule> GenerateParticipantSchedule(DateTime evaluationDate, string studyId)
        {
            List<Schedule> participantSchedules = new List<Schedule>();

            //Initialize schedule
            participantSchedules = context.VisitSettings.Where(s => s.Id <= 9 && s.Id > 2).Select(s =>
                  new Schedule()
                  {
                      studyId = studyId,
                      VisitStage = s.Id,
                      WindowStart = null,
                      WindowEnd = null
                  }
               ).OrderBy(s => s.VisitStage).ToList();

            participantSchedules = GetWindowDates(participantSchedules, evaluationDate);

            return participantSchedules;
        }

        public async Task CreateScheduleForExistingVisits()
        {
            var evaluationVisits = context.Visits.Where(v => v.VisitSettingId == 2 && v.Deleted == 0 && v.VisitOutcome == 1)
                .Select(v => new { v.ParticipantId, v.VisitDate })
                .ToList()
                .GroupBy(v => v.ParticipantId)
                .ToList();
            
            foreach(var visitGroup in evaluationVisits)
            {
                var visit = visitGroup.FirstOrDefault();
                var studyId = GetStudyId(visit.ParticipantId);

                if (visit != null && studyId != null)
                {
                    var schedules = GenerateParticipantSchedule(visit.VisitDate, studyId);

                    foreach (var schedule in schedules)
                    {
                        if (!ScheduleExists(studyId, schedule.VisitStage))
                            await AddSchedule(schedule);
                    }
                }
            }
        }

        public bool IsASutarday(DateTime date)
        {
            return date.DayOfWeek == DayOfWeek.Saturday;
        }

        public bool IsASunday(DateTime date)
        {
            return date.DayOfWeek == DayOfWeek.Sunday;
        }
        public bool IsAFriday(DateTime date)
        {
            return date.DayOfWeek == DayOfWeek.Friday;
        }

        private DateTime GetEvaluationWindowEndDate(DateTime? evaluationDate)
        {
            return evaluationDate.Value.AddDays(2);
        }
        private List<Schedule> GetWindowDates(List<Schedule> participantSchedules, DateTime? evaluationDate)
        {
            if (evaluationDate.HasValue)
            {
                var evaluationWindowEndDate = GetEvaluationWindowEndDate(evaluationDate.Value);

                foreach (var item in participantSchedules)
                {
                    //if (item.VisitStage == 1)
                    //{
                    //    item.WindowStart = evaluationDate;
                    //    item.WindowEnd = evaluationDate;
                    //}

                    //if (item.VisitStage == 2)
                    //{
                    //    //var evaluationDate = evaluationDate.Value.AddDays(2);

                    //    if (IsASutarday(evaluationDate.Value))
                    //    {
                    //        evaluationDate = evaluationDate.AddDays(2);
                    //    }

                    //    if (IsASunday(evaluationDate.Value))
                    //    {
                    //        evaluationDate = evaluationDate.AddDays(1);
                    //    }

                    //    item.WindowStart = evaluationDate;
                    //    item.WindowEnd = evaluationDate.AddDays(2);
                    //}

                    if (item.VisitStage == 3)
                    {
                        //var evaluationEndDate = participantSchedules.Find(i => i.VisitStage == 2).WindowEnd;
                        var baselineStartDate = evaluationWindowEndDate.AddDays(2);

                        if (IsASutarday(baselineStartDate))
                            baselineStartDate = baselineStartDate.AddDays(2);

                        if (IsASunday(baselineStartDate))
                            baselineStartDate = baselineStartDate.AddDays(1);

                        item.WindowStart = baselineStartDate;
                        item.WindowEnd = baselineStartDate.AddDays(2);
                    }

                    if (item.VisitStage == 4)
                    {
                        //var evaluationEndDate = participantSchedules.Find(i => i.VisitStage == 2).WindowEnd;
                        var week1StartDate = evaluationWindowEndDate.AddDays(7);

                        if (IsASutarday(week1StartDate))
                            week1StartDate = week1StartDate.AddDays(2);

                        if (IsASunday(week1StartDate))
                            week1StartDate = week1StartDate.AddDays(1);

                        item.WindowStart = week1StartDate;
                        item.WindowEnd = week1StartDate.AddDays(7);
                    }

                    if (item.VisitStage == 5)
                    {
                        //var evaluationEndDate = participantSchedules.Find(i => i.VisitStage == 2).WindowEnd;
                        var week2StartDate = evaluationWindowEndDate.AddDays(14);

                        if (IsASutarday(week2StartDate))
                            week2StartDate = week2StartDate.AddDays(2);

                        if (IsASunday(week2StartDate))
                            week2StartDate = week2StartDate.AddDays(1);

                        item.WindowStart = week2StartDate;
                        item.WindowEnd = week2StartDate.AddDays(7);
                    }

                    if (item.VisitStage == 6)
                    {
                        //var evaluationEndDate = participantSchedules.Find(i => i.VisitStage == 2).WindowEnd;
                        var week4StartDate = evaluationWindowEndDate.AddDays(28);

                        if (IsASutarday(week4StartDate))
                            week4StartDate = week4StartDate.AddDays(2);

                        if (IsASunday(week4StartDate))
                            week4StartDate = week4StartDate.AddDays(1);

                        item.WindowStart = week4StartDate;
                        item.WindowEnd = week4StartDate.AddMonths(1);
                    }

                    if (item.VisitStage == 7)
                    {
                        //var evaluationEndDate = participantSchedules.Find(i => i.VisitStage == 2).WindowEnd;
                        var week8StartDate = evaluationWindowEndDate.AddDays(56);

                        if (IsASutarday(week8StartDate))
                            week8StartDate = week8StartDate.AddDays(2);

                        if (IsASunday(week8StartDate))
                            week8StartDate = week8StartDate.AddDays(1);

                        item.WindowStart = week8StartDate;
                        item.WindowEnd = week8StartDate.AddMonths(1);
                    }

                    if (item.VisitStage == 8)
                    {
                        //var evaluationEndDate = participantSchedules.Find(i => i.VisitStage == 2).WindowEnd;
                        var week12StartDate = evaluationWindowEndDate.AddDays(84);

                        if (IsASutarday(week12StartDate))
                            week12StartDate = week12StartDate.AddDays(2);

                        if (IsASunday(week12StartDate))
                            week12StartDate = week12StartDate.AddDays(1);

                        item.WindowStart = week12StartDate;
                        item.WindowEnd = week12StartDate.AddMonths(1);
                    }

                    if (item.VisitStage == 9)
                    {
                        //var evaluationEndDate = participantSchedules.Find(i => i.VisitStage == 2).WindowEnd;
                        var week36StartDate = evaluationWindowEndDate.AddDays(252);

                        if (IsASutarday(week36StartDate))
                            week36StartDate = week36StartDate.AddDays(2);

                        if (IsASunday(week36StartDate))
                            week36StartDate = week36StartDate.AddDays(1);

                        item.WindowStart = week36StartDate;
                        item.WindowEnd = week36StartDate.AddMonths(1);
                    }
                }
            }
           
            return participantSchedules;
        }
        public ScheduleViewModel GetParticipantSchedule(string studyId, int visitSettingId)
        {
            var vm = new ScheduleViewModel();

            var stage = visitSettingId;
            var schedule = context.Schedules.Where(s => s.studyId == studyId && s.VisitStage == stage).FirstOrDefault();

            if (schedule != null)
            {
                vm.ExpectedVisitDate = schedule.WindowStart;

                if (schedule.VisitStage < 9)
                {
                    stage++;
                    var nextVisit = context.Schedules.Where(s => s.studyId == studyId && s.VisitStage == stage).FirstOrDefault();
                    vm.NextAppointmentDate = nextVisit.WindowStart;
                }
            }

            return vm;
        }

        public List<Schedule> GetSchedules()
        {
            return context.Schedules.ToList();
        }

        private int GetNextVisitStage(int participantId)
        {
            var studyId = GetStudyId(participantId);
            var schedule = context.Schedules.Where(p => p.studyId == studyId);
            var visit = context.Visits.Include(m => m.VisitSetting).Where(m => m.ParticipantId == participantId && m.Deleted == 0)
                .OrderByDescending(m => m.Id).FirstOrDefault();

            var nextStage = 0;

            var nextVisit = schedule.Where(s => s.WindowStart.Value.Date >= DateTime.Now.Date && s.WindowStart.Value.Date <= DateTime.Now.Date)
                    .FirstOrDefault();

            if (nextVisit != null)
            {
                nextStage = nextVisit.VisitStage;
            }
            else
            {
                var upcomingVisit = schedule.Where(s => s.WindowEnd.Value.Date >= DateTime.Now.Date).FirstOrDefault();
                if (upcomingVisit != null)
                    nextStage = upcomingVisit.VisitStage;
                else
                    nextStage = 9;
            }

            return nextStage;
        }
        public Visits LoadExpectedVisit(int participantId)
        {
            //get the participant
            var participant = context.Participants.Where(p => p.Id == participantId).FirstOrDefault();

            //get the participant last visitsetting
            var visit = context.Visits
                .Include(m => m.VisitSetting)
                .Where(m => m.ParticipantId == participantId && m.Deleted == 0 && m.VisitSettingId <= 9)
                .OrderByDescending(m => m.Id)
                .FirstOrDefault();

            var visitSetting = visit?.VisitSetting;
           
            var visits = new Visits();

            //if the visit is null then this is the initial visit ie screening
            if (visit == null)
            {
                var nextStage = 1;
                var visitDate = DateTime.Now.Date;
                

                if (IsASutarday(visitDate))
                    visitDate = visitDate.AddDays(2);

                if (IsASunday(visitDate))
                    visitDate = visitDate.AddDays(1);

                var evaluationDate = visitDate.AddDays(2);

                if (IsASutarday(evaluationDate))
                    evaluationDate = evaluationDate.AddDays(2);

                if (IsASunday(evaluationDate))
                    evaluationDate = evaluationDate.AddDays(1);

                visits.NextAppointment = evaluationDate;
                visits.VisitDate = visitDate;
                visits.ParticipantId = participant.Id;
                visits.VisitSettingId = nextStage;
                visits.DateParticipantCame = DateTime.Now.Date;

                return visits;
            }

            var visitStage = visitSetting == null ? 0 : visitSetting.Id;
            var nextVisitStage = GetNextVisitStage(participantId);
            var participantSchedule = GetParticipantSchedule(participant.StudyId, nextVisitStage);

            if (visitStage <= 8)
            {
                visits.VisitDate = participantSchedule.ExpectedVisitDate.Value;
                visits.ParticipantId = participant.Id;
                visits.VisitSettingId = nextVisitStage;
                visits.NextAppointment = participantSchedule.NextAppointmentDate;
            }
            else
            {
                if (visitStage <= 16)
                {
                    visits.VisitDate = DateTime.Now.Date;
                    visits.ParticipantId = participant.Id;
                    visits.VisitSettingId = visitStage + 1;
                    visits.NextAppointment = null;
                }
                else
                {
                    visits.VisitDate = DateTime.Now.Date;
                    visits.ParticipantId = participant.Id;
                    visits.VisitSettingId = visitStage;
                    visits.NextAppointment = null;
                }
            }

            return visits;
        }
        public string GetStudyId(int participantId)
        {
            var participant = context.Participants.Where(p => p.Id == participantId).FirstOrDefault();

            if (participant != null)
                return participant.StudyId;

            return null;
        }
        private DateTime? GetCompletionDate(int participantId, int visitStage)
        {
            var visit = context.Visits.Where(v => v.ParticipantId == participantId && v.VisitSettingId == visitStage).FirstOrDefault();

            if (visit != null)
                return visit.DateParticipantCame.Value;

            return null;
        }

        private int? GetVisitOutcome(int participantId, int visitStage)
        {
            var visit = context.Visits.Where(v => v.ParticipantId == participantId && v.VisitSettingId == visitStage).FirstOrDefault();

            if (visit != null)
                return visit.VisitOutcome;

            return null;
        }

        private string GetOutcomeComment(int participantId, int visitStage)
        {
            var visit = context.Visits.Where(v => v.ParticipantId == participantId && v.VisitSettingId == visitStage).FirstOrDefault();

            if (visit != null)
                return visit.OutcomeComment;

            return null;
        }
        public List<PSFScheduleViewModel> GetPSFSchedule(int participantId)
        {
            var studyId = GetStudyId(participantId);
            var psfScheduleViewModelList = new List<PSFScheduleViewModel>();

            if (!string.IsNullOrEmpty(studyId))
            {
                var psfParticipantSchedule = context.PSFSchedules.Where(s => s.StudyId == studyId).ToList();
                var psfVisits = context.Visits.Where(v => v.VisitSettingId > 9 && v.ParticipantId == participantId).ToList();

                foreach (var psfSchedule in psfParticipantSchedule)
                {
                    var psfScheduleViewModel = new PSFScheduleViewModel();
                    var visitStage = context.VisitSettings.Where(s => s.VisitStage == psfSchedule.VisitStage).FirstOrDefault();
                    var completiondate = GetCompletionDate(participantId, psfSchedule.VisitStage);
                    var assessmentCompletetion = GetVisitOutcome(participantId, psfSchedule.VisitStage);
                    var outcomeComment = GetOutcomeComment(participantId, psfSchedule.VisitStage);

                    psfScheduleViewModel.VisitSettingId = psfSchedule.VisitStage;
                    psfScheduleViewModel.StudyId = psfSchedule.StudyId;

                    if (visitStage != null)
                        psfScheduleViewModel.VisitStage = visitStage.VisitType;

                    psfScheduleViewModel.ExpectedDate = psfSchedule.ExpectedDate;

                    if (completiondate.HasValue)
                        psfScheduleViewModel.CompletionDate = completiondate.Value.ToString("yyyy-MM-dd");
                    else
                        psfScheduleViewModel.CompletionDate = "";

                    if (assessmentCompletetion.HasValue)
                        psfScheduleViewModel.AssessmentCompletion = assessmentCompletetion.Value == 1 ? "Complete" : "Incomplete";
                    else
                        psfScheduleViewModel.AssessmentCompletion = "";

                    psfScheduleViewModel.Comment = outcomeComment != null ? outcomeComment : "";

                    psfScheduleViewModelList.Add(psfScheduleViewModel);
                }

                return psfScheduleViewModelList;
            }

            return null;
        }
        public List<ParticipantVisitSchedule> GetPatientSchedule(int participantId)
        {
            var participantVisitSchedules = new List<ParticipantVisitSchedule>();
            var studyId = GetStudyId(participantId);
            var schedule = context.Schedules.Where(s => s.studyId == studyId).OrderBy(s => s.VisitStage).ToList();

            foreach (var item in schedule)
            {
                var vm = new ParticipantVisitSchedule();
                var completiondate = GetCompletionDate(participantId, item.VisitStage);
                var assessmentCompletetion = GetVisitOutcome(participantId, item.VisitStage);
                var outcomeComment = GetOutcomeComment(participantId, item.VisitStage);

                vm.ParticipantId = participantId;
                vm.VisitType = context.VisitSettings.Where(s => s.VisitStage == item.VisitStage).FirstOrDefault().VisitType;
                vm.VisitSettingId = item.VisitStage;

                vm.WindowOpens = item.WindowStart.Value.ToString("yyyy-MM-dd");
                vm.WindowCloses = item.WindowEnd.Value.ToString("yyyy-MM-dd");

                if (completiondate.HasValue)
                    vm.CompletionDate = completiondate.Value.ToString("yyyy-MM-dd");
                else
                    vm.CompletionDate = "";

                if (assessmentCompletetion.HasValue)
                    vm.AssessmentCompletion = assessmentCompletetion.Value == 1 ? "Complete" : "Incomplete";
                else
                    vm.AssessmentCompletion = "";

                vm.Comment = outcomeComment != null ? outcomeComment : "";

                participantVisitSchedules.Add(vm);
            }

            return participantVisitSchedules;
        }

        public int? GetParticipantId(string studyId)
        {
            var participant = context.Participants.Where(p => p.StudyId == studyId).FirstOrDefault();

            if (participant != null)
                return participant.Id;

            return null;
        }

        public DateTime GetWindowEndDate(DateTime startDate, int visitSettingId)
        {
            if (visitSettingId == 2)
            {
                return startDate.AddDays(2);
            }

            else if (visitSettingId == 3)
            {
                return startDate.AddDays(2);
            }

            else if (visitSettingId == 4)
            {
                return startDate.AddDays(3);
            }

            else if (visitSettingId == 5)
            {
                return startDate.AddDays(3);
            }

            else if (visitSettingId == 6)
            {
                return startDate.AddMonths(1);
            }

            else if (visitSettingId == 7)
            {
                return startDate.AddMonths(1);
            }

            else if (visitSettingId == 8)
            {
                return startDate.AddMonths(1);
            }

            return startDate.AddMonths(1);
        }

        public List<MissedVisitsViewModel> GetMissedVisits()
        {
            var qaulifiedVisits = context.Schedules.Where(v => v.WindowEnd.Value.Date < DateTime.Now.Date);
            var visits = context.Visits.Where(v => v.Deleted != 1);
            var missedVisits = new List<MissedVisitsViewModel>();

            foreach (var qualifiedVisit in qaulifiedVisits)
            {
                var participantId = GetParticipantId(qualifiedVisit.studyId);

                if (participantId != null)
                {
                    var participantCame = visits.Where(v => v.ParticipantId == participantId && v.VisitSettingId == qualifiedVisit.VisitStage
                    && v.ParticipantCame == 1 && v.VisitOutcome == 1).FirstOrDefault();

                    if (participantCame == null)
                    {
                        var missedVisit = new MissedVisitsViewModel();

                        missedVisit.VisitDate = qualifiedVisit.WindowStart.Value.Date;
                        missedVisit.StudyId = qualifiedVisit.studyId;
                        missedVisit.VisitSettingId = qualifiedVisit.VisitStage;
                        missedVisit.VisitStage = context.VisitSettings.Where(s => s.VisitStage.Value == qualifiedVisit.VisitStage).FirstOrDefault().VisitType;
                        missedVisit.ParticipantId = participantId.Value;

                        if (qualifiedVisit.VisitStage < 9)
                        {
                            var nextVisitStage = qualifiedVisit.VisitStage + 1;
                            var nextVisit = context.Schedules.Where(s => s.studyId == qualifiedVisit.studyId && s.VisitStage == nextVisitStage).FirstOrDefault();

                            if (nextVisit != null)
                                missedVisit.NextAppointment = nextVisit.WindowStart.Value.Date;
                        }

                        missedVisits.Add(missedVisit);
                    }
                }
            }

            foreach (var visit in visits)
            {
                var visitSettingId = visit.VisitSettingId;

                if (visitSettingId <= 9)
                {
                    var nextVisitSettingId = visitSettingId + 1;

                    if (nextVisitSettingId > 9)
                    {
                        continue;
                    }

                    var exists = visits.Where(v => v.ParticipantId == visit.ParticipantId && v.VisitSettingId == nextVisitSettingId
                            && v.ParticipantCame == 1 && v.VisitOutcome == 1).FirstOrDefault();

                    if (exists == null)
                    {
                        if (GetWindowEndDate(visit.NextAppointment.Value, nextVisitSettingId) < DateTime.Now.Date)
                        {
                            var existsInmissedVisits = missedVisits.Where(m => m.ParticipantId == visit.ParticipantId && m.VisitSettingId == nextVisitSettingId)
                                .FirstOrDefault();

                            if (existsInmissedVisits != null)
                                continue;

                            var missedVisit = new MissedVisitsViewModel();

                            missedVisit.VisitDate = visit.NextAppointment.Value;
                            missedVisit.StudyId = GetStudyId(visit.ParticipantId);
                            missedVisit.VisitSettingId = nextVisitSettingId;
                            missedVisit.VisitStage = context.VisitSettings.Where(s => s.VisitStage.Value == nextVisitSettingId).FirstOrDefault().VisitType;
                            missedVisit.ParticipantId = visit.ParticipantId;
                        }
                    }
                }
            }

            return missedVisits;
        }

        public PSFSchedule LoadExpectedPSFVisit(int participantId)
        {
            var psfSchedule = context.PSFSchedules.Where(p => p.ParticipantId == participantId)
                .OrderBy(s => s.VisitStage).ToList();

            //Get the last completed PSF Visit
            var psfVisit = context.Visits.Where(v => v.VisitSettingId > 9 && v.ParticipantId == participantId
                && v.ParticipantCame == 1 && v.VisitOutcome == 1)
                .OrderByDescending(v => v.VisitSettingId).FirstOrDefault();

            int lastCompletedPSFVisitStage = 0;
            PSFSchedule nextPSFVisit = null;

            if (psfVisit != null)
            {
                if (psfVisit.VisitSettingId < 17)
                {
                    lastCompletedPSFVisitStage = psfVisit.VisitSettingId;
                    var nextPSFVisitStage = lastCompletedPSFVisitStage + 1;
                    nextPSFVisit = psfSchedule.Where(s => s.VisitStage == nextPSFVisitStage).FirstOrDefault();
                }
            }
            else
            {
                nextPSFVisit = psfSchedule.FirstOrDefault();
            }

            return nextPSFVisit;
        }

        public DateTime? GetNextPSFAppointmentDate(int currentPSFVisitStage, string studyId)
        {
            var nextPSFAppointmentStage = currentPSFVisitStage + 1;

            if (nextPSFAppointmentStage < 17)
            {
                var schedule = context.PSFSchedules.Where(s => s.VisitStage == nextPSFAppointmentStage && s.StudyId == studyId).FirstOrDefault();
                if (schedule != null)
                    return schedule.ExpectedDate;
            }

            return null;
        }

        public List<PSFSchedule> CreatePSFSchedule(string studyId)
        {
            List<PSFSchedule> psfParticipantSchedules = new List<PSFSchedule>();

            if (studyId != null)
            {
                var regularSchedule = context.Schedules.Where(s => s.studyId == studyId);

                //Initialize schedule
                psfParticipantSchedules = context.VisitSettings.Where(s => s.Id > 9 && s.Id <= 17).Select(s =>
                       new PSFSchedule()
                       {
                           StudyId = studyId,
                           VisitStage = s.Id,
                           ParticipantId = GetParticipantId(studyId).Value
                       }
                   ).OrderBy(s => s.VisitStage).ToList();

                var regularSchedules = regularSchedule.Where(s => s.studyId == studyId);

                if (regularSchedules != null)
                {
                    DateTime? week1ExpectedDate = null;

                    var week1Schedule = regularSchedules.Where(s => s.VisitStage == 4).FirstOrDefault();

                    if (week1Schedule != null)
                        week1ExpectedDate = week1Schedule.WindowStart.Value;

                    var session1 = psfParticipantSchedules.Where(s => s.VisitStage == 10).FirstOrDefault();
                    if (session1 != null)
                        session1.ExpectedDate = week1ExpectedDate.Value;

                    var session2 = psfParticipantSchedules.Where(s => s.VisitStage == 11).FirstOrDefault();
                    if (session2 != null)
                        session2.ExpectedDate = session1.ExpectedDate.AddDays(7);

                    var session3 = psfParticipantSchedules.Where(s => s.VisitStage == 12).FirstOrDefault();
                    if (session3 != null)
                        session3.ExpectedDate = session2.ExpectedDate.AddDays(7);

                    var session4 = psfParticipantSchedules.Where(s => s.VisitStage == 13).FirstOrDefault();
                    if (session4 != null)
                        session4.ExpectedDate = session3.ExpectedDate.AddDays(7);

                    var session5 = psfParticipantSchedules.Where(s => s.VisitStage == 14).FirstOrDefault();
                    if (session5 != null)
                        session5.ExpectedDate = session4.ExpectedDate.AddDays(7);

                    var session6 = psfParticipantSchedules.Where(s => s.VisitStage == 15).FirstOrDefault();
                    if (session6 != null)
                        session6.ExpectedDate = session5.ExpectedDate.AddDays(7);

                    var session7 = psfParticipantSchedules.Where(s => s.VisitStage == 16).FirstOrDefault();
                    if (session7 != null)
                        session7.ExpectedDate = session6.ExpectedDate.AddDays(7);

                    var session8 = psfParticipantSchedules.Where(s => s.VisitStage == 17).FirstOrDefault();
                    if (session8 != null)
                        session8.ExpectedDate = session7.ExpectedDate.AddDays(7);
                }
            }

            return psfParticipantSchedules;
        }
        public List<string> GetExistingPSFParticipants()
        {
            var studyIds = context.Participants.Where(p => p.IsPSF == true)
                .Select(p => p.StudyId).ToList();

            return studyIds;
        }
        public async Task<int> AddPSFSchedule(List<PSFSchedule> psfSchedule)
        {
            var schedules = new List<PSFSchedule>();

            foreach (var schedule in psfSchedule)
            {
                if (!psfScheduleExists(schedule.StudyId, schedule.VisitStage))
                {
                    schedules.Add(schedule);
                }
            }

            context.AddRange(schedules);

            return await context.SaveChangesAsync();
        }

        public bool IsPSF(int participantId)
        {
            var participant = context.Participants.Where(p => p.Id == participantId && p.IsPSF == true).FirstOrDefault();

            if (participant != null)
                return true;

            return false;
        }

        public bool ScheduleExists(string studyId, int visitStage)
        {
            var schedule = context.Schedules.Where(s => s.studyId == studyId && s.VisitStage == visitStage).FirstOrDefault();

            if (schedule != null)
                return true;
            return false;
        }

        public bool psfScheduleExists(string studyId, int visitStage)
        {
            var schedule = context.PSFSchedules.Where(s => s.StudyId == studyId && s.VisitStage == visitStage).FirstOrDefault();

            if (schedule != null)
                return true;
            return false;
        }
    }
}