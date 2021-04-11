using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAppointments.BusinessLogic.Entity;
using WebAppointments.BusinessLogic.IService;

namespace WebAppointments.BusinessLogic.Service
{
    public class UserLogic : IUserLogic
    {

        private readonly SmokingStudyDbContext context = new SmokingStudyDbContext();

        public async Task<List<AspNetUsers>> GetUsers()
        {
            return await context.AspNetUsers.ToListAsync();
        }
    }
}