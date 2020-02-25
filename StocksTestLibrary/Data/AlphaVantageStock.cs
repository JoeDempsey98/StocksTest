using Newtonsoft.Json.Linq;
using StocksTestLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace StocksTestLibrary.Data
{
    public static class AlphaVantageStock
    {
        //fetch stock info, format it, and pass it along to be used

        public static string FetchStock(string ticker, string apikey)
        {
            string url = $"https://www.alphavantage.co/query?function=TIME_SERIES_DAILY_ADJUSTED&symbol={ticker}&apikey={apikey}";
            string json;

            using (var web = new WebClient())
            {
                json = web.DownloadString(url);
            }

            return json;
        }

        public static IList<StockModel> FormatInfo(string ticker, string apikey)
        {
            string json = FetchStock(ticker, apikey);

            JObject o = JObject.Parse(json);

            IList<JToken> js = o["Time Series (Daily)"].ToList();

            IList<StockModel> list = new List<StockModel>();
            foreach (var i in js)
            {
                foreach (var j in i)
                {
                    StockModel stock = new StockModel();
                    stock.Open = j["1. open"].ToObject<decimal>();
                    stock.High = j["2. high"].ToObject<decimal>();
                    stock.Low = j["3. low"].ToObject<decimal>();
                    stock.Close = j["4. close"].ToObject<decimal>();
                    stock.Volume = j["6. volume"].ToObject<long>();
                    stock.Date = i.ToString().Substring(1, 10);
                    stock.Ticker = ticker;

                    list.Add(stock);
                }
            }


            return list;
        }
    }
}
