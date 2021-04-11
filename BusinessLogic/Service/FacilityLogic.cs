using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppointments.BusinessLogic.Entity;
using WebAppointments.BusinessLogic.IService;

namespace WebAppointments.BusinessLogic.Service
{

    public class FacilityLogic : IFacilityLogic
    {
        private readonly SmokingStudyDbContext context = new SmokingStudyDbContext();
        public async Task<int> AddFacilities(Facilities facility)
        {
            context.Add(facility);
            return await context.SaveChangesAsync();
        }

        public async Task<List<Facilities>> GetFacilities()
        {
            return await context.Facilities
                .Where(m => m.Deleted != 1).ToListAsync();
        }

        public async Task<Facilities> GetFacilities(int id)
        {
            return await context.Facilities
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<int> UpdateFacilities(Facilities facility)
        {
            var existing = context.Facilities.FirstOrDefault(m => m.Id == facility.Id);
            context.Entry(existing).CurrentValues.SetValues(facility);

            return await context.SaveChangesAsync();
        }

        public bool FacilityExists(int id)
        {
            return context.Facilities.Any(e => e.Id == id);
        }


    }
}
