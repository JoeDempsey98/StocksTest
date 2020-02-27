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
        //add methods for using LINQ to fetch data from the database to display
        //refactor code to abstract away some dependencies and obsolete methods
        //generally clean up code

        private readonly APIKey _apiKey;
        private readonly StockContext _context;
        public StocksController(IOptions<APIKey> apiKey, StockContext context)
        {
            _apiKey = apiKey.Value ?? throw new ArgumentException(nameof(apiKey));
            _context = context;
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
            
            _context.AddRange(list);
            
            _context.SaveChanges();

            return list.ToString();
        }

        [HttpGet("{symbol}")]
        public ActionResult<string> GetStockInfo(string symbol)
        {
            return AlphaVantageStock.FetchStock(symbol, _apiKey.AlphaVantageKey);
        }
    }
}