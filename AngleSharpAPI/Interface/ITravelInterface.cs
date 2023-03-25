using AngleSharpAPI.Entity;

namespace AngleSharpAPI.Interface
{
    public interface ITravelInterface
    {
        Task<List<TravelWarning>> GetAllTravelWarningAsync();
        Task<DataResultModel<Info>> GetActivitiesAsync();
        Task<DataResultModel<PoiNearList>> GetViewPoint(string accessToken);
    }
}
