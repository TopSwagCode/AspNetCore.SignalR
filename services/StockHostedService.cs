using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TopSwagCode.SignalR.Hubs;
using TopSwagCode.SignalR.Models;

namespace TopSwagCode.SignalR.services
{
    internal class StockHostedService : IHostedService, IDisposable
    {
        private readonly ILogger _logger;
        private Timer _timer;
        private readonly IHubContext<StockHub> _stockHubContext;
        private readonly IStockService _stockService;

        public StockHostedService(ILogger<StockHostedService> logger, IHubContext<StockHub> stockHubContext, IStockService stockService)
        {
            _logger = logger;
            _stockHubContext = stockHubContext;
            _stockService = stockService;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(CheckForChangedStockAndPublishToClients, null, TimeSpan.Zero, TimeSpan.FromMilliseconds(2000));
            return Task.CompletedTask;
        }

        private void CheckForChangedStockAndPublishToClients(object state)
        {
            _logger.LogInformation("Timed Stock Background Service is working.");

            var stocks = _stockService.GetChangedStocks();

            var jsonString = JsonConvert.SerializeObject(stocks);

            _stockHubContext.Clients.All.SendAsync("UpdateStocks", jsonString).GetAwaiter().GetResult();
        }



        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
