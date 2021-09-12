using BaseService;
using DataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace api
{
    public class Startup : AppStartupBase
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env) : base(env, configuration)
        {
        }

        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureApplicationServices(services, new OpenApiInfo
            {
                Version = "v1",
                Title = "Et&T Forecast API",
                Description = "Et&T Forecast API"
            });
            services.AddDbContext<ForecastContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("ForecastContext")));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            ConfigureApplication(app, env);
        }
    }
}