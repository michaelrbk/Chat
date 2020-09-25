using Chat.App.Interfaces;
using Chat.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ChatBotWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private static Settings _settings;
        private readonly IChatBotApp _chatBotApp;

        public Worker(ILogger<Worker> logger, IChatBotApp chatBotApp, IOptions<Settings> settings)
        {
            _logger = logger;
            _settings = settings.Value;
            _chatBotApp = chatBotApp;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await _chatBotApp.ProcessQueue();
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
