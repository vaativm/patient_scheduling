using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;
using WebAppointments.BusinessLogic.Entity;
using WebAppointments.BusinessLogic.IService;
using WebAppointments.BusinessLogic.Service;

namespace WebAppointments
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("SmokingStudyDbConnection")));
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            // var key = Encoding.ASCII.GetBytes("abcdefgh");
            // services.AddAuthentication(options =>
            // {
            //     options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //     options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            // }
            //     ).AddJwtBearer(options =>
            //     {
            //         options.RequireHttpsMetadata = false;
            //         options.SaveToken = true;
            //         options.TokenValidationParameters = new TokenValidationParameters
            //         {
            //             ValidateIssuerSigningKey = true,
            //             IssuerSigningKey = new SymmetricSecurityKey(key),
            //             ValidateIssuer = false,
            //             ValidateAudience = false
            //         };
            //     });

            services.AddScoped<IParticipantLogic, ParticipantLogic>();
            services.AddScoped<IVisitSettingsLogic, VisitSettingsLogic>();
            services.AddScoped<IUserLogic, UserLogic>();
            services.AddScoped<IVisitLogic, VisitLogic>();
            services.AddScoped<IReportLogic, ReportLogic>();
            services.AddScoped<IFacilityLogic, FacilityLogic>();
            //for development purposes

#if DEBUG
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
#endif
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider service)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllers();
                endpoints.MapRazorPages();
            });

            CreateUserRoles(service).Wait();
        }

        private async Task CreateUserRoles(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            IdentityResult roleResult;
            //TODO create these roles when we have a list of all the roles
            //Supervisor, Human Resource, Procurement
            //Adding Addmin Role   
            var roleCheck = await RoleManager.RoleExistsAsync("admin");
            if (!roleCheck)
            {
                //create the roles and seed them to the database   
                roleResult = await RoleManager.CreateAsync(new IdentityRole("admin"));
            }
            roleCheck = await RoleManager.RoleExistsAsync("Super Admin");
            if (!roleCheck)
            {
                //create the roles and seed them to the database   
                roleResult = await RoleManager.CreateAsync(new IdentityRole("Super Admin"));
            }

            //Assign Admin role to the main User here we have given our newly loregistered login id for Admin management   
            IdentityUser IDuser = await UserManager.FindByEmailAsync("admin@admin.com");
            if (IDuser == null)
            {
                var user = new IdentityUser { Email = "demo@demo.com", UserName = "admin@admin.com" };
                await UserManager.CreateAsync(user, "@dm1n");
                var code = await UserManager.GenerateEmailConfirmationTokenAsync(user);
                await UserManager.ConfirmEmailAsync(user, code);
                await UserManager.AddToRoleAsync(user, "admin");

                // foreach (var permission in context.AspNetPermisions)
                // {
                //     await UserManager.AddClaimAsync(user, new Claim(permission.Permission, permission.Permission));
                // }
            }

            
        }
    }
}
