using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TopSwagCode.SignalR.Models
{
    public class Stock
    {
        [JsonProperty("symbol")]
        public string Symbol { get; set; }
        [JsonProperty("bid")]
        public double Bid { get; set; }
        [JsonProperty("ask")]
        public double Ask { get; set; }
    }
}
