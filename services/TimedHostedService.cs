using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Amazon.SQS;
using Amazon.SQS.Model;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TopSwagCode.SignalR.Hubs;

namespace TopSwagCode.SignalR.services
{
    internal class TimedHostedService : IHostedService, IDisposable
    {
        private readonly ILogger _logger;
        private Timer _timer;
        private readonly IHubContext<GraphHub> _graphHubContext;
        private IAmazonSQS _amazonSqs;
        private readonly string _queueUrl;

        public TimedHostedService(ILogger<TimedHostedService> logger, IHubContext<GraphHub> graphHubContext, IAmazonSQS amazonSqs)
        {
            _logger = logger;
            _graphHubContext = graphHubContext;
            _amazonSqs = amazonSqs;
            _queueUrl = "https://sqs.eu-west-1.amazonaws.com/455373423635/TestQueue";
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
            bool anyMessages = true;
            while (anyMessages)
            {
                var result = _amazonSqs.ReceiveMessageAsync(_queueUrl).GetAwaiter().GetResult();
                if (result.Messages.Any())
                {
                    foreach (var message in result.Messages)
                    {
                        _logger.LogInformation(message.Body);
                        dynamic sqsMessage = JsonConvert.DeserializeObject(message.Body);

                        _graphHubContext.Clients.All.SendCoreAsync("LogWork", new[] { sqsMessage.Message }).GetAwaiter().GetResult();
                        
                     
                        _amazonSqs.DeleteMessageAsync(new DeleteMessageRequest(_queueUrl, message.ReceiptHandle)).GetAwaiter().GetResult();
                    }

                    if(_amazonSqs.GetType() == typeof(FakeSqsService))
                        anyMessages = false; // Fake Sqs never runs out of messages
                }
                else
                {
                    anyMessages = false;
                    _logger.LogInformation("No more messages waiting for next");
                }
                
            }
            
            
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
