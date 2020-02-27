using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using StocksTestAPI.Data;
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

        public ActionResult<string> GetStockInfo() 
        {
            //write a function to fetch dbcontext, and pass it the formatted info to update the database with
            string[] tickers =
            {
                "MSFT",
                "TSLA",
                "AAPL",
                "BABA"
            };

            var list = AlphaVantageStock.FormatInfo(tickers, _apiKey.AlphaVantageKey);

            return list.ToString();
        }

        [HttpGet("{symbol}")]
        public ActionResult<string> GetStockInfo(string symbol)
        {
            return AlphaVantageStock.FetchStock(symbol, _apiKey.AlphaVantageKey);
        }
    }
}