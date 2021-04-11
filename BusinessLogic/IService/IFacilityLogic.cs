using System.Collections.Generic;
using System.Threading.Tasks;
using WebAppointments.BusinessLogic.Entity;

namespace WebAppointments.BusinessLogic.IService
{
    public interface IFacilityLogic
    {
        Task<List<Facilities>> GetFacilities();
        Task<Facilities> GetFacilities(int id);
        Task<int> AddFacilities(Facilities facility);
        Task<int> UpdateFacilities(Facilities facility);
        bool FacilityExists(int id);
    }
}
