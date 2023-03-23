using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace AngleSharpAPI.Controllers
{
    public class TelegramBotController : Controller
    {
        readonly IConfiguration _configuration;

        public TelegramBotController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("Test")]
        [Route("[controller]/Test_TG")]
        public async Task<string> Test()
        {
            var telegramToken = _configuration["TelegramToken"];
            var botClient = new TelegramBotClient(telegramToken);
            //取得機器人基本資訊
            var me = await botClient.GetMeAsync();
            //發送訊息到指定頻道
            Message message = await botClient.SendTextMessageAsync(
                  chatId: "-1001598450899",
                  text: "哈囉 我是醬油膏的機器人~");
            //回傳取得的機器人基本資訊
            return $"Hello, World! I am user {me.Id} and my name is {me.FirstName}.";
        }
    }
}
