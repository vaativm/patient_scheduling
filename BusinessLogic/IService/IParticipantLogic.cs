using System.Collections.Generic;
using System.Threading.Tasks;
using WebAppointments.BusinessLogic.Entity;

namespace WebAppointments.BusinessLogic.IService
{
    public interface IParticipantLogic
    {
        Task<List<Participants>> GetParticipants();
        Task<Participants> GetParticipants(int id);
        Task<int> AddParticipants(Participants participant);
        Task<int> UpdateParticipant(Participants participant);
        bool ParticipantExists(int id);
    }
}