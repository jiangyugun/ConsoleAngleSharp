using Microsoft.Extensions.Configuration;
using System;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramBotConsole.Entity;

namespace TelegramBotConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            IConfiguration _config = new ConfigurationBuilder()
           .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
           .Build();
            TelegramBotClient bot = new TelegramBotClient(_config["TelegramToken"]!);

            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = new UpdateType[]
                {
                    UpdateType.Message,
                    UpdateType.EditedMessage,
                }
            };

            bot.StartReceiving(UpdateHandler, ErrorHandler, receiverOptions);
            Console.WriteLine("TelegramBot 啟動中!!");
            Console.ReadLine();
        }

        private static Task ErrorHandler(ITelegramBotClient arg1, Exception arg2, CancellationToken arg3)
        {
            throw new NotImplementedException();
        }

        private static Task UpdateHandler(ITelegramBotClient arg1, Update update, CancellationToken arg3)
        {
            if (update.Type == UpdateType.Message)
            {
                if(update.Message!.Type == MessageType.Text)
                {
                    var text = update.Message.Text;
                    var id = update.Message.Chat.Id;
                    string? username = update.Message.From!.FirstName + update.Message.From.LastName;

                    Console.WriteLine($"{username} | {id} | {text}");
                }
            }

            return Task.CompletedTask;
        }
    }
}