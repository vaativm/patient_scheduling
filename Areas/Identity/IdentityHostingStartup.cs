using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(WebAppointments.Areas.Identity.IdentityHostingStartup))]
namespace WebAppointments.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
                       {

                       });
        }
    }
}