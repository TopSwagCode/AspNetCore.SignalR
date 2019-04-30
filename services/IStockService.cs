using System.Collections.Generic;
using TopSwagCode.SignalR.Models;

namespace TopSwagCode.SignalR.services
{
    public interface IStockService
    {
        List<Stock> GetChangedStocks();
        List<Stock> GetAllStocks();
    }
}