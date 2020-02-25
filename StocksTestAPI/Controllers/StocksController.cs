using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using StocksTestAPI.Models;
using StocksTestLibrary.Data;

namespace StocksTestAPI.Controllers
{
    [Route("api/Stocks")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        //put a method to push data to the db

        private readonly APIKey _apiKey;
        public StocksController(IOptions<APIKey> apiKey)
        {
            _apiKey = apiKey.Value ?? throw new ArgumentException(nameof(apiKey));
        }

        [HttpGet("{symbol}")]
        public ActionResult<string> GetStockInfo(string symbol)
        {
            return AlphaVantageStock.FetchStock(symbol, _apiKey.AlphaVantageKey);
        }
    }
}