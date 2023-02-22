using AngleSharpAPI.Entity;
using AngleSharpAPI.Interface;
using AngleSharpAPI.Service;
using Microsoft.AspNetCore.Mvc;

namespace AngleSharpAPI.Controllers
{
    [ApiController]
    public class TravelController : Controller
    {
        readonly ITravelInterface travelInterface;
        public TravelController(ITravelInterface travelInterface)
        {
            this.travelInterface = travelInterface;
        }

        /// <summary>
        /// 取得所有國家旅遊警示資訊
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("[controller]/GetAllWarning")]
        public async Task<List<TravelWarning>> GetAllTravelWarningAsync()
        {
            return await travelInterface.GetAllTravelWarningAsync();
        }

        /// <summary>
        /// 取得活動 - 觀光資訊資料庫
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("[controller]/GetActivities")]
        public async Task<DataResultModel<Info>> GetActivitiesAsync()
        {
            return await travelInterface.GetActivitiesAsync();
        }
    }
}
