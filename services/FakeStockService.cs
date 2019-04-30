using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TopSwagCode.SignalR.Models;

namespace TopSwagCode.SignalR.services
{
    public class FakeStockService : IStockService
    {
        private readonly Random _random;

        public FakeStockService()
        {
            _random = new Random();
        }

        public List<Stock> GetChangedStocks()
        {
            var changedStocks = new List<Stock>();

            var numberOfChangedStock = _random.Next(0, 4);

            for (int i = 0; i < numberOfChangedStock; i++)
            {
                changedStocks.Add(new Stock
                {
                    Ask = Math.Round(_random.NextDouble() * (200 - 10) + 10, 2),
                    Bid = Math.Round(_random.NextDouble() * (200 - 10) + 10, 2),
                    Symbol = randomStockNames[_random.Next(0, randomStockNames.Count)]
                });
            }

            return changedStocks;
        }

        public List<Stock> GetAllStocks()
        {
            var stocks = new List<Stock>();


            for (int i = 0; i < randomStockNames.Count; i++)
            {
                stocks.Add(new Stock
                {
                    Ask = Math.Round(_random.NextDouble() * (200 - 10) + 10, 2),
                    Bid = Math.Round(_random.NextDouble() * (200 - 10) + 10, 2),
                    Symbol = randomStockNames[i]
                });
            }

            return stocks;
        }

        List<string> randomStockNames = new List<string>
        {
            "AMZN", "TGT", "VNET", "CARB", "CCIH", "FB", "IAC", "JCOM", "EGOV", "NTES"
        };

    }
}
