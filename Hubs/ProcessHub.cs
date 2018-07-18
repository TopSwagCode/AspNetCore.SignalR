using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace TopSwagCode.SignalR.Hubs
{
    public class ProcessHub : Hub
    {
        public async Task StartProcessing()
        {
            for (int i = 0; i <= 100; i += 5)
            {
                await Clients.Caller.SendAsync("process", i);
                await Task.Delay(75);
            }

            await Task.Delay(2000);

            await Clients.Caller.SendAsync("processDone", "/assets/topswagcode.png");
        }
    }
}
