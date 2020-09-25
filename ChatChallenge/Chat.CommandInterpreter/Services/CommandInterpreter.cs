using Chat.Models;
using Chat.Models.Stock;
using Chat.QueueManager.Interfaces;
using Chat.WebService.Services;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace Chat.QueueManager.Services
{
    public class CommandInterpreter : ICommandInterpreter
    {
        private static StockWebService _stockWS;
        private static QueueManagerService _queueManager;
        private static Settings _settings;


        public CommandInterpreter(IOptions<Settings> settings)
        {
            _settings = settings.Value;
            _queueManager = new QueueManagerService(settings);
            _stockWS = new StockWebService(settings);
        }


        public Task InterpretCommand(string message)
        {

            var queueMessage = new QueueMessage
            {
                Queue = "ChatHub"
            };

            message = message.Replace(" ", "").ToLower();
            if (message.ToLower().StartsWith("/stock="))
            {
                var stockRequest = new StockRequest
                {
                    StockCode = message.Replace("/stock=", "")
                };

                //Get Stock Value from webService
                Stock stock = _stockWS.GetStock(stockRequest).Result;


                if (stock.Close > 0)
                {
                    queueMessage.Message = $"{stock.Symbol} quote is {stock.Close} per share";
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
            _queueManager.AddToQueue(queueMessage);
            return Task.CompletedTask;
        }

    }
}
