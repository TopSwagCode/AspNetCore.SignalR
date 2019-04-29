using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TopSwagCode.SignalR.Hubs;
using TopSwagCode.SignalR.services;

namespace TopSwagCode.SignalR
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Could add a WebAPI to get Current Stock from cache / inmemory, so we don't just see random stocks as price changes.

            // TODO: Add Unit test project.

            // TODO: Add Mediatr.
            services.AddCors(options => options.AddPolicy("CorsPolicy",
                builder =>
                {
                    // TODO: Add Prod Cors policy for TopSwagCode and set this as Dev Policy.
                    builder.AllowAnyMethod()
                        .AllowAnyHeader()
                        .WithOrigins("http://localhost:4000")
                        .AllowCredentials();
                }));
            
            services.AddHostedService<TimedHostedService>();
            
            services.AddSignalR();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors("CorsPolicy");

            app.UseSignalR(route =>
            {
                route.MapHub<ChatHub>("/chathub");
                route.MapHub<ProcessHub>("/processhub");
                route.MapHub<GraphHub>("/graphhub");
                route.MapHub<StockHub>("/stockhub");
            });
        }
    }
}
