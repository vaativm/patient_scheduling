using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAppointments.BusinessLogic.Entity;
using WebAppointments.Models;

namespace WebAppointments.BusinessLogic.IService
{
    public interface IVisitLogic
    {
        Task<List<Visits>> GetVisits();
        Task<Visits> GetVisits(int id);
        Task<int> AddVisits(Visits visit);
        List<Schedule> GenerateParticipantSchedule(DateTime expectedDate, string studyId);
        List<PSFSchedule> CreatePSFSchedule(string studyId);
        Task<int> AddSchedule(Schedule schedule);
        Task<int> AddPSFSchedule(List<PSFSchedule> schedule);
        List<string> GetExistingPSFParticipants();
        List<Schedule> GetSchedules();
        ScheduleViewModel GetParticipantSchedule(string studyId, int visitSettingId);
        List<ParticipantVisitSchedule> GetPatientSchedule(int participantId);
        List<PSFScheduleViewModel> GetPSFSchedule(int participantId);
        Task<int> UpdateVisit(Visits visit);
        bool VisitExists(int id);
        int CountVisit();
        int CountSchedulledVisits();
        int CountOverDueAppointment();
        Visits LoadExpectedVisit(int participantId);
        PSFSchedule LoadExpectedPSFVisit(int participantId);
        DateTime? GetNextPSFAppointmentDate(int currentPSFVisitStage, string studyId);
        List<ScheduledVisitsViewModel> GetSchedulledVisits();
        List<MissedVisitsViewModel> GetMissedVisits();
        bool IsPSF(int participantId);
        string GetStudyId(int participantId);
        bool ScheduleExists(string studyId, int visitStage);
        bool psfScheduleExists(string studyId, int visitStage);
        Task CreateScheduleForExistingVisits();
    }
}