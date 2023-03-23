using Microsoft.Extensions.Configuration;
using System;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBotConsole.Entity;
using static System.Net.Mime.MediaTypeNames;

namespace TelegramBotConsole
{
    class Program
    {
        //取得設定檔
        static readonly IConfiguration _config = new ConfigurationBuilder()
       .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
       .Build();

        //抓取TelegramBot的Token
        static TelegramBotClient bot = new(_config["TelegramToken"]!);

        static bool screaming = false;

        // Pre - assign menu text
        const string firstMenu = "<b>Menu 1</b>\n\nA beautiful menu with a shiny inline button.";
        const string secondMenu = "<b>Menu 2</b>\n\nA better menu with even more shiny inline buttons.";

        // Pre-assign button text
        const string nextButton = "Next";
        const string backButton = "Back";
        const string tutorialButton = "Tutorial";

        // 建立按紐
        static readonly InlineKeyboardMarkup firstMenuMarkup = new(InlineKeyboardButton.WithCallbackData(nextButton));
        static readonly InlineKeyboardMarkup secondMenuMarkup = new(
            new[] {
                    new[] { InlineKeyboardButton.WithCallbackData(backButton) },
                    new[] { InlineKeyboardButton.WithUrl(tutorialButton, "https://core.telegram.org/bots/tutorial") }
            }
        );
        static void Main(string[] args)
        {
            using var cts = new CancellationTokenSource();
            var options = new ReceiverOptions
            {
                AllowedUpdates = new UpdateType[]
                {
                    UpdateType.Message,
                    UpdateType.CallbackQuery,
                    UpdateType.EditedMessage
                }
            };

            bot.StartReceiving(UpdateHandler, ErrorHandler,receiverOptions: options, cancellationToken:cts.Token);
            Console.WriteLine("TelegramBot 啟動中!!");
            Console.ReadLine();

            // Send cancellation request to stop the bot
            cts.Cancel();

        }

        private static Task ErrorHandler(ITelegramBotClient arg1, Exception arg2, CancellationToken arg3)
        {
            throw new NotImplementedException();
        }

        private static async Task UpdateHandler(ITelegramBotClient arg1, Update update, CancellationToken cancellationToken)
        {
            if (update.Type == UpdateType.CallbackQuery)
            {
                await HandleButton(update.CallbackQuery!);
                return;
            }

            var user = update.Message!.From!;
            var text = update.Message!.Text ?? string.Empty;
            if (user is null)
                return;

            Console.WriteLine($"{user.FirstName} | {user.Id} | {text}");

            if (update.Message!.Text!.StartsWith("/"))
            {
                await HandleCommand(user.Id, text);
            }
            else if (screaming && text.Length > 0)
            {
                await bot.SendTextMessageAsync(user.Id, text.ToUpper(), entities: update.Message!.Entities, cancellationToken: cancellationToken);
            }
            else
            {
                await bot.CopyMessageAsync(user.Id, user.Id, update.Message!.MessageId, cancellationToken: cancellationToken);
            }
        }

        /// <summary>
        /// 指令篩選器
        /// </summary>
        /// <param name="id"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private static async Task HandleCommand(long userId, string? command)
        {
            switch (command)
            {
                case "/scream":
                    screaming = true;
                    break;

                case "/whisper":
                    screaming = false;
                    break;

                case "/menu":
                    await SendMenu(userId);
                    break;
            }
            await Task.CompletedTask;
        }

        /// <summary>
        /// 送出選單
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private static async Task SendMenu(long userId)
        {
            await bot.SendTextMessageAsync(
                userId,
                firstMenu,
                ParseMode.Html,
                replyMarkup: firstMenuMarkup
            );
        }

        /// <summary>
        /// 按鈕事件處理
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        private static async Task HandleButton(CallbackQuery query)
        {
            string text = string.Empty;
            InlineKeyboardMarkup markup = new(Array.Empty<InlineKeyboardButton>());

            if (query.Data == nextButton)
            {
                text = secondMenu;
                markup = secondMenuMarkup;
            }
            else if (query.Data == backButton)
            {
                text = firstMenu;
                markup = firstMenuMarkup;
            }

            // Close the query to end the client-side loading animation
            await bot.AnswerCallbackQueryAsync(query.Id);

            // Replace menu text and keyboard
            await bot.EditMessageTextAsync(
                query.Message!.Chat.Id,
                query.Message.MessageId,
                text,
                ParseMode.Html,
                replyMarkup: markup
            );

        }
    }
}