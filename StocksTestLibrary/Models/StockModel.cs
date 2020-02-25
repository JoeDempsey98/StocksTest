using System;
using System.Collections.Generic;
using System.Text;

namespace StocksTestLibrary.Models
{
    public class StockModel
    {
        public decimal Open { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Close { get; set; }
        public long Volume { get; set; }
        public string Date { get; set; }
        public string Ticker { get; set; }
    }
}
