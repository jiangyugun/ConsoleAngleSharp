using AngleSharpAPI.Entity;
using AngleSharpAPI.Service;
using Microsoft.AspNetCore.Mvc;

namespace AngleSharpAPI.Controllers
{
    [ApiController]
    public class TravelController : Controller
    {
        /// <summary>
        /// 取得所有國家旅遊警示資訊
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("[controller]/GetAllWarning")]
        public async Task<List<TravelWarning>> GetAllTravelWarningAsync()
        {
            TravelService travelService = new();
            return await travelService.GetAllTravelWarningAsync();
        }
    }
}
