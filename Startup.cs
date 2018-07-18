using Amazon.SQS;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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

            services.AddCors(options => options.AddPolicy("CorsPolicy",
                builder =>
                {
                    builder.AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowAnyOrigin()
                        .AllowCredentials();
                }));

            services.AddSingleton<IAmazonSQS>(o => new FakeSqsService());

            services.AddHostedService<TimedHostedService>();

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddSignalR();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseCors("CorsPolicy");

            app.UseSignalR(route =>
            {
                route.MapHub<ChatHub>("/chathub");
                route.MapHub<ProcessHub>("/processhub");
                route.MapHub<GraphHub>("/graphhub");
            });
        }
    }
}
