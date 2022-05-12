using Microsoft.Extensions.Options;
using System.Text.RegularExpressions;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramBot.ApplicationFiles.Interfaces;
using TelegramBot.ConfigurationModels;
using TelegramBot.Entities;
using TelegramBot.Services.Interfaces;

namespace TelegramBot.ApplicationFiles
{
    public class BotApplication : IBotApplication
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly AppSettings _settings;
        private readonly TelegramBotClient _botClient;
        private readonly ReceiverOptions _receiverOptions;

        public BotApplication(
            IOptions<AppSettings> options,
            ITelegramBotBuilder builder,
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _settings = options.Value;
            _botClient = builder.GetBot();

            _receiverOptions = new ReceiverOptions()
            {
                AllowedUpdates = new UpdateType[]
                {
                    UpdateType.Message,
                    UpdateType.EditedMessage
                }
            };
        }

        public void StartBotApplication(CancellationToken ct)
        {
            _botClient.StartReceiving(
                UpdateHandler,
                ErrorHandler,
                _receiverOptions,
                ct);
        }

        private async Task ErrorHandler(ITelegramBotClient client, Exception ext, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        private async Task UpdateHandler(ITelegramBotClient client, Update update, CancellationToken ct)
        {
            if (update == null)
            {
                Console.WriteLine("Неизвестное событие. Не удалось обработать.");
                return;
            }

            switch (update.Type)
            {
                case UpdateType.Message:
                    if (update.Message == null) return;
                    switch (update.Message.Type)
                    {
                        case MessageType.Text:
                            await MessageTypeTextRequest(update);
                            break;

                        case MessageType.Document:
                            await MessageTypeDocumentRequest(update);
                            break;
                    }
                    break;

                case UpdateType.EditedMessage:
                    break;
            }
        }

        private async Task MessageTypeTextRequest(Update update)
        {
            if (update == null || update.Message == null || update.Message.Chat == null)
            {
                return;
            }

            var text = update.Message!.Text ?? string.Empty;
            var chatId = update.Message.Chat.Id;

            string? username = update.Message.Chat.Username;
            string firstname = update.Message.Chat.FirstName ?? "noname";
            string? lastname = update.Message.Chat.LastName;

            UserExistCheck(update);

            Console.WriteLine($"{chatId} | {username} | {firstname} | {lastname} | {text}");
            await _botClient.SendTextMessageAsync(update.Message.Chat!, text);
        }

        private void UserExistCheck(Update update)
        {
            var chat = update.Message!.Chat;
            using (var scope = _serviceProvider.CreateScope())
            {
                var serviceUsers = scope.ServiceProvider.GetRequiredService<IServiceUsers>();

                var allRegisteredUsersByFirstname =
                    serviceUsers.GetUsersByFirstnameAndLastname(
                        chat.FirstName!, chat.LastName!);

                if (allRegisteredUsersByFirstname.Count == 0)
                {
                    serviceUsers.AddUser(new AppUserEntity
                    {
                        Username = chat.Username,
                        Firstname = chat.FirstName,
                        Lastname = chat.LastName,
                        ChatId = chat.Id
                    });
                }
            }
        }

        private async Task MessageTypeDocumentRequest(Update update)
        {
            var fileName = update.Message!.Document!.FileName;
            var fileId = update.Message.Document.FileId;

            if (!Directory.Exists(_settings.DownloadFilesDirectoryPath))
            {
                Directory.CreateDirectory(_settings.DownloadFilesDirectoryPath);
            }

            var file = await _botClient.GetFileAsync(fileId);
            if (file == null) return;

            bool isContain;
            do
            {
                isContain = DetermineSequenceContainsFile(fileName!, _settings.DownloadFilesDirectoryPath);
                if (isContain)
                {
                    fileName = RenameDuplicateFile(fileName!);
                }
            } while (isContain);

            using (var fileStream = new FileStream(Path.Combine(_settings.DownloadFilesDirectoryPath, fileName!), FileMode.Create))
            {
                await _botClient.DownloadFileAsync(file.FilePath!, fileStream);
                await _botClient.SendTextMessageAsync(update.Message.Chat!, "Файл успешно скачан");
            }
        }

        private bool DetermineSequenceContainsFile(string fileName, string directoryPath)
        {
            var allFiles = Directory.GetFiles(Path.GetFullPath(directoryPath))
                .Select(f => Path.GetFileName(f))
                .ToArray();

            if (allFiles.Contains(fileName))
            {
                return true;
            }

            return false;
        }

        private string RenameDuplicateFile(string oldFullFileName)
        {
            var regex = new Regex(@"(\(\d+\))\z");
            var oldName = Path.GetFileNameWithoutExtension(oldFullFileName);
            var extension = Path.GetExtension(oldFullFileName);
            string newFullFileName;

            if (!regex.IsMatch(oldName))
            {
                newFullFileName = string.Format(oldName + "(0)" + extension);
                return newFullFileName;
            }

            var arrayNameFragments = regex.Split(oldName)
                    .Where(s => !(string.IsNullOrWhiteSpace(s) || string.IsNullOrEmpty(s)))
                    .ToArray();

            if (arrayNameFragments.Length > 1)
            {
                var nameWhithoutNumeration = arrayNameFragments[0]; // имя
                var numerationWhithoutName = arrayNameFragments[1]; // нумерация

                var numerationAsNumber = uint.Parse(numerationWhithoutName[1..^1]);
                newFullFileName = string
                    .Format(nameWhithoutNumeration + "(" + (numerationAsNumber + 1).ToString() + ")" + extension);
            }
            else
            {
                throw new ApplicationException(
                    "При переименовании файла что-то пошло не так.\nНе удалось разделить нумерацию и имя");
            }
            return newFullFileName;
        }
    }
}
