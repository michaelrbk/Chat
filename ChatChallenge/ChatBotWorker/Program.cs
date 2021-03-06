using Chat.App.App;
using Chat.App.Interfaces;
using Chat.Models;
using Chat.QueueConsumer.Interfaces;
using Chat.QueueConsumer.Services;
using Chat.Services.Interfaces;
using Chat.Services.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ChatBotWorker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    IConfiguration configuration = hostContext.Configuration;
                    services.Configure<Settings>(configuration.GetSection("SharedSettings"));

                    services.AddHostedService<Worker>();

                    services.AddTransient<IChatBotApp, ChatBotApp>();
                    services.AddTransient<IChatBotService, ChatBotService>();
                    services.AddTransient<IQueueConsumerService, QueueConsumerService>();
                });
    }
}
