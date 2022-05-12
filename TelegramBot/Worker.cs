using TelegramBot.ApplicationFiles;
using TelegramBot.ApplicationFiles.Interfaces;
using Microsoft.Extensions.Options;
using TelegramBot.ConfigurationModels;
using Telegram.Bot;

namespace TelegramBot
{
    public class Worker : BackgroundService
    {
        private readonly IBotApplication _botApplication;
        private readonly ILogger<Worker> _logger;

        public Worker(
            IBotApplication botApplication,
            ILogger<Worker> logger)
        {
            _botApplication = botApplication;
            _logger = logger;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Старт бота");
            _botApplication.StartBotApplication(stoppingToken);
            return Task.CompletedTask;  
        }
    }
}