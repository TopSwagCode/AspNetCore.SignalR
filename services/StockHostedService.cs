using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TopSwagCode.SignalR.Hubs;

namespace TopSwagCode.SignalR.services
{
    internal class StockHostedService : IHostedService, IDisposable
    {
        private readonly ILogger _logger;
        private Timer _timer;
        private readonly IHubContext<StockHub> _graphHubContext;

        public StockHostedService(ILogger<TimedHostedService> logger, IHubContext<StockHub> graphHubContext)
        {
            _logger = logger;
            _graphHubContext = graphHubContext;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Background Service is starting.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromMilliseconds(500));
            

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            _logger.LogInformation("Timed Background Service is working.");

            _graphHubContext.Clients.All.SendAsync("LogWork", "Some random message").GetAwaiter().GetResult();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Background Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
