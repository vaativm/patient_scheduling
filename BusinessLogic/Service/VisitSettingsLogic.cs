using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppointments.BusinessLogic.Entity;
using WebAppointments.BusinessLogic.IService;

namespace WebAppointments.BusinessLogic.Service
{
    public class VisitSettingsLogic : IVisitSettingsLogic
    {
        private readonly SmokingStudyDbContext context = new SmokingStudyDbContext();

        public async Task<List<VisitSettings>> GetVisitSettings()
        {
            return await context.VisitSettings.ToListAsync();
        }

        public async Task<VisitSettings> GetVisitSettings(int id)
        {
            return await context.VisitSettings
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<int> AddVisitSettings(VisitSettings visitSettings)
        {
            context.Add(visitSettings);
            return await context.SaveChangesAsync();
        }

        public async Task<int> UpdateVisitSettings(VisitSettings visitSettings)
        {
            var existing = context.VisitSettings.FirstOrDefault(m => m.Id == visitSettings.Id);
            context.Entry(existing).CurrentValues.SetValues(visitSettings);
            return await context.SaveChangesAsync();
        }

        public bool VisitSettingsExists(int id)
        {
            return context.VisitSettings.Any(e => e.Id == id);
        }

        public async Task<Visits> GetParticipantLastVisitSetting(int participantId)
        {
            return await context.Visits.Include(m => m.VisitSetting).Where(m => m.ParticipantId == participantId && m.Deleted == 0)
            .OrderByDescending(m => m.Id).FirstOrDefaultAsync();
        }
    }
}