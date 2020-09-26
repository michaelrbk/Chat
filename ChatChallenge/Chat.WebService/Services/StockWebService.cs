using Chat.Models;
using Chat.Models.Stock;
using Chat.WebService.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Chat.WebService.Services
{
    public class StockWebService : IStockWebService
    {
        private static Settings _settings;
        public StockWebService(IOptions<Settings> settings)
        {
            _settings = settings.Value;
        }
        public Task<Stock> GetStock(StockRequest request)
        {
            var stock = new Stock();
            try
            {
                List<string> headers;
                List<string> values;
                (headers, values) = SplitCSV(GetCSV(_settings.StockWebServiceUrl.Replace("{stock_code}", request.StockCode)));
                stock.Symbol = values[headers.IndexOf("Symbol")];

                NumberFormatInfo formatProvider = new NumberFormatInfo
                {
                    NumberDecimalSeparator = "."
                };

                stock.DateTime = DateTime.ParseExact(values[headers.IndexOf("Date")] + " " + values[headers.IndexOf("Time")], "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                stock.Open = Convert.ToDouble(values[headers.IndexOf("Open")], formatProvider);
                stock.Close = Convert.ToDouble(values[headers.IndexOf("Close")], formatProvider);
                stock.High = Convert.ToDouble(values[headers.IndexOf("High")], formatProvider);
                stock.Low = Convert.ToDouble(values[headers.IndexOf("Low")], formatProvider);
                stock.Volume = Convert.ToDouble(values[headers.IndexOf("Volume")], formatProvider);
            }
            catch (Exception) { }

            return Task.FromResult(stock);
        }

        private string GetCSV(string url)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

            StreamReader sr = new StreamReader(resp.GetResponseStream());
            string results = sr.ReadToEnd();
            sr.Close();

            return results;
        }

        private static (List<string>, List<string>) SplitCSV(string file)
        {
            string[] tempStringLines;
            List<string> headers = new List<string>();
            List<string> values = new List<string>();

            tempStringLines = file.Split("\r\n");

            foreach (string item in tempStringLines[0].Split(","))
            {
                headers.Add(item);
            };

            foreach (string item in tempStringLines[1].Split(","))
            {
                values.Add(item.ToUpper());
            };
            return (headers, values);
        }
    }
}
