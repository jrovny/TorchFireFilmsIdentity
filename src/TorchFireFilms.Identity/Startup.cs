using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using TorchFireFilms.Identity.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using IdentityServer4.Services;
using Microsoft.AspNetCore.HttpOverrides;

namespace TorchFireFilms.Identity
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureNonBreakingSameSiteCookies();
            services.AddDbContext<ApplicationDbContext>();
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders =
                    ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
                options.KnownNetworks.Clear();
                options.KnownProxies.Clear();
            });
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            var config = new IdentityServerConfiguration();
            Configuration.Bind("IdentityServerConfiguration", config);

            var builder = services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;

                // see https://identityserver4.readthedocs.io/en/latest/topics/resources.html
                options.EmitStaticAudienceClaim = true;
            });
            builder.AddInMemoryIdentityResources(config.IdentityResources);
            builder.AddInMemoryApiScopes(config.ApiScopes);
            builder.AddInMemoryClients(config.Clients);
            builder.AddAspNetIdentity<ApplicationUser>();
            builder.AddProfileService<CustomProfileService>();

            // TODO: Remove from production
            if (_env.IsDevelopment())
                builder.AddDeveloperSigningCredential();
            else
                builder.AddSigningCredential(X509CertificateManager.GetX509Certificate2(config.X509CertificatePath));

            services.AddSingleton<IConnectionService, ConnectionService>();
            services.AddTransient<IProfileService, CustomProfileService>();

            services.AddRazorPages();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseCookiePolicy();
            app.UseForwardedHeaders();
            if (_env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseIdentityServer();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
