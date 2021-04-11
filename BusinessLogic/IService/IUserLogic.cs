using System.Collections.Generic;
using System.Threading.Tasks;
using WebAppointments.BusinessLogic.Entity;

namespace WebAppointments.BusinessLogic.IService
{
    public interface IUserLogic
    {
        Task<List<AspNetUsers>> GetUsers();

    }
}