using Chat.App.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ChatBotWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IChatBotApp _chatBotApp;

        public Worker(ILogger<Worker> logger, IChatBotApp chatBotApp)
        {
            _logger = logger;
            _chatBotApp = chatBotApp;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _chatBotApp.ProcessQueue();
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(500, stoppingToken);
            }
        }
    }
}
