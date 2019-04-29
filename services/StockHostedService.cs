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
        private readonly Random _random;

        public StockHostedService(ILogger<StockHostedService> logger, IHubContext<StockHub> stockHubContext)
        {
            // Build some basic UI besides Blog if someone wants to run it locally.
            _logger = logger;
            _stockHubContext = stockHubContext;
            _random = new Random();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Background Service is starting.");
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromMilliseconds(1000));
            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            // Perhaps some better loggings :D
            _logger.LogInformation("Timed Background Service is working.");

            var stocks = FetchFreshStocks();

            var jsonString = JsonConvert.SerializeObject(stocks);
            // TODO: Better method name than LogWork :)
            _stockHubContext.Clients.All.SendAsync("LogWork", jsonString).GetAwaiter().GetResult();
        }


        // This would normally be a SQS queue or something similar to get information from some external process that keep track on stocks.
        private List<Stock> FetchFreshStocks()
        {
            var newStocks = new List<Stock>();

            var numberOfnewStock = _random.Next(0, 4);

            for (int i = 0; i < numberOfnewStock; i++)
            {
                // TODO: Add random stock generator.
                newStocks.Add(new Stock
                {
                    Ask = 1.11,
                    Bid = 1.11,
                    Symbol = "ASD"
                });
            }

            return newStocks;
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
