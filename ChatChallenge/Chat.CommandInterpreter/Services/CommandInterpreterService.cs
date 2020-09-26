using Chat.CommandInterpreter.Interfaces;
using Chat.Models;
using Chat.Models.Stock;
using Chat.QueueProducer.Services;
using Chat.WebService.Services;
using Microsoft.Extensions.Options;
using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Chat.CommandInterpreter.Services
{
    public class CommandInterpreterService : ICommandInterpreterService
    {
        private static StockWebService _stockWS;
        private static QueueProducerService _queueProducerServices;
        private static Settings _settings;


        public CommandInterpreterService(IOptions<Settings> settings)
        {
            _settings = settings.Value;
            _queueProducerServices = new QueueProducerService(settings);
            _stockWS = new StockWebService(settings);
        }


        public void InterpretCommand(string message)
        {

            var queueMessage = new QueueMessage
            {
                Queue = "ChatHub"
            };

            //Remove special caracters
            message = message.Replace(" ", "");
            message = Regex.Replace(message, @"[^\w\.@-]", "", RegexOptions.None, TimeSpan.FromSeconds(1.5));

            if (message.StartsWith("stock"))
            {
                var stockRequest = new StockRequest
                {
                    StockCode = message.Replace("stock", "")
                };

                //Get Stock Value from webService
                Stock stock = _stockWS.GetStock(stockRequest).Result;


                if (stock.Open > 0)
                {
                    queueMessage.Message = $"{stock.Symbol} quote is ${stock.Open.ToString("G", CultureInfo.InvariantCulture)} per share";
                }
                else
                {
                    queueMessage.Message = $"{stock.Symbol} stock not found";
                }

            }
            else
            {
                //Return inválid command to chat queue
                queueMessage.Message = "Invalid Command";
            }
            _queueProducerServices.AddToQueue(queueMessage);
            //return Task.CompletedTask;
        }

    }
}
