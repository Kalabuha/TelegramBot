using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using TelegramBot.ConfigurationModels;

namespace TelegramBot.ApplicationFiles.Interfaces
{
    public interface ITelegramBotBuilder
    {
        TelegramBotClient GetBot();
    }
}
