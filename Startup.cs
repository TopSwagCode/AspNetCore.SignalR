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
        private readonly IHostingEnvironment _environment;

        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            _environment = environment;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options => options.AddPolicy("CorsPolicy",
                builder =>
                {
                    // TODO: Add Prod Cors policy for TopSwagCode and set this as Dev Policy.
                    if (_environment.IsDevelopment())
                    {
                        builder.AllowAnyMethod()
                            .AllowAnyHeader()
                            .WithOrigins("http://localhost:4000")
                            .AllowCredentials();
                    }
                    else
                    {
                        builder.AllowAnyMethod()
                            .AllowAnyHeader()
                            .WithOrigins("http://localhost:4000", "http://127.0.0.1:4000", "https://topswagcode.com")
                            .AllowCredentials();
                    }
                }));

            services.AddTransient<IStockService, FakeStockService>();
            services.AddHostedService<TimedHostedService>();
            services.AddHostedService<StockHostedService>();

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
