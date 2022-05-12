using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Telegram.Bot;
using TelegramBot.ApplicationFiles.Interfaces;
using TelegramBot.ConfigurationModels;

namespace TelegramBot.ApplicationFiles
{
    internal class TelegramBotBuilder : ITelegramBotBuilder
    {
        private readonly BotSettings _settings;

        public TelegramBotBuilder(IOptions<BotSettings> options)
        {
            _settings = options.Value;
        }

        public TelegramBotClient GetBot()
        {
            var botClient = new TelegramBotClient(_settings.BotToken);

            return botClient;
        }
    }
}
