using System;
using System.Collections.Generic;
using System.Text;

namespace StocksTestLibrary.Models
{
    public class StockModel
    {
        public int Id { get; set; }
        public decimal Open { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Close { get; set; }
        public long Volume { get; set; }
        public string Date { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public string Ticker { get; set; }
        public decimal LastAveragePrice { get; set; }
        public decimal AveragePrice { get; set; }
        public decimal NextAveragePrice { get; set; }
    }
}
