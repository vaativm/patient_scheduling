using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppointments.BusinessLogic.Entity;
using WebAppointments.BusinessLogic.IService;

namespace WebAppointments.BusinessLogic.Service
{
    public class ParticipantLogic : IParticipantLogic
    {
        private readonly SmokingStudyDbContext context = new SmokingStudyDbContext();

        public async Task<List<Participants>> GetParticipants()
        {
            return await context.Participants
                .Include(m => m.Visits)
                .ThenInclude(m => m.VisitSetting)
                .Include(f => f.Facility)
                .Where(m => m.Deleted != 1).ToListAsync();
        }

        public async Task<Participants> GetParticipants(int id)
        {
            return await context.Participants
                .Include(m => m.Visits)
                .ThenInclude(m => m.VisitSetting)
                .Include(f => f.Facility)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<int> AddParticipants(Participants participant)
        {
            context.Add(participant);
            return await context.SaveChangesAsync();
        }

        public async Task<int> UpdateParticipant(Participants participant)
        {
            var existing = context.Participants.FirstOrDefault(m => m.Id == participant.Id);
            context.Entry(existing).CurrentValues.SetValues(participant);
            //context.Update(participant);
            return await context.SaveChangesAsync();
        }

        public bool ParticipantExists(int id)
        {
            return context.Participants.Any(e => e.Id == id);
        }
    }
}