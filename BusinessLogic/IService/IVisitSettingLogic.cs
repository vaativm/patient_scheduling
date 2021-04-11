using System.Collections.Generic;
using System.Threading.Tasks;
using WebAppointments.BusinessLogic.Entity;

namespace WebAppointments.BusinessLogic.IService
{
    public interface IVisitSettingsLogic
    {
        Task<List<VisitSettings>> GetVisitSettings();
        Task<VisitSettings> GetVisitSettings(int id);
        Task<int> AddVisitSettings(VisitSettings participant);
        Task<int> UpdateVisitSettings(VisitSettings participant);
        bool VisitSettingsExists(int id);
        Task<Visits> GetParticipantLastVisitSetting(int participantId);
    }
}