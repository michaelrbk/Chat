using System;

namespace Chat.Models.Stock
{
    public class Stock
    {
        public string Symbol { get; set; }
        public DateTime DateTime { get; set; }
        public double Open { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public double Close { get; set; }
        public double Volume { get; set; }

    }
}
