using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using TopSwagCode.SignalR.services;

namespace TopSwagCode.SignalR.Hubs
{
    public class StockHub : Hub
    {
        private readonly IStockService _stockService;

        public StockHub(IStockService stockService)
        {
            _stockService = stockService;
        }

        public override async Task OnConnectedAsync()
        {
            var stocks = _stockService.GetAllStocks();

            var jsonString = JsonConvert.SerializeObject(stocks);

            await Clients.Caller.SendAsync("UpdateStocks", jsonString);

            await base.OnConnectedAsync();
        }
    }
}
