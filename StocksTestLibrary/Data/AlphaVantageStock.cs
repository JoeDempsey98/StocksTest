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
                    StockModel stock = new StockModel
                    {
                        Open = j["1. open"].ToObject<decimal>(),
                        High = j["2. high"].ToObject<decimal>(),
                        Low = j["3. low"].ToObject<decimal>(),
                        Close = j["4. close"].ToObject<decimal>(),
                        Volume = j["6. volume"].ToObject<long>(),
                        Date = i.ToString().Substring(1, 10)
                    };
                    stock.Year = Int32.Parse(stock.Date.Substring(0, 4));
                    stock.Month = Int32.Parse(stock.Date.Substring(5, 2));
                    stock.Day = Int32.Parse(stock.Date.Substring(8, 2));
                    stock.AveragePrice = (stock.Open + stock.High + stock.Low + stock.Close) / 4;
                    stock.Ticker = ticker;

                    list.Add(stock);
                }
            }

            for (int i = 0; i < list.Count; i++)
            {
                if (i == 0)
                    list[i].LastAveragePrice = list[i].AveragePrice;
                else
                    list[i].LastAveragePrice = list[i - 1].AveragePrice;
                if (i == list.Count - 1)
                    list[i].NextAveragePrice = list[i].AveragePrice;
                else
                    list[i].NextAveragePrice = list[i + 1].AveragePrice;
            }

            return list;
        }
    }
}
