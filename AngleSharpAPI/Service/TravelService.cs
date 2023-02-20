using AngleSharp.Dom;
using AngleSharp;
using AngleSharpAPI.Entity;
using Microsoft.Extensions.Hosting;

namespace AngleSharpAPI.Service
{
    public class TravelService
    {
        public async Task<List<TravelWarning>> GetAllTravelWarningAsync()
        {
            var config = AngleSharp.Configuration.Default
                        .WithDefaultLoader()
                        .WithDefaultCookies(); // 加上 `WithDefaultCookies()` 來加上預設的 Cookie

            var browser = BrowsingContext.New(config);

            var url = new Url("https://www.boca.gov.tw/sp-trwa-list-1.html");   // 這邊用的型別是 AngleSharp 提供的 AngleSharp.Dom.Url

            // 使用 OpenAsync 來打開網頁抓回內容
            var document = await browser.OpenAsync(url);

            var postSource = document.QuerySelectorAll("section.listTb.areaTb table tbody tr");
            //.Select(node=>node.InnerHtml).Dump(); //取出國家警示列表

            var travelWarnings = postSource.Select(post =>
            {
                var countryElement = post.QuerySelector("[data-title=國家] > a");
                var country = countryElement?.InnerHtml;
                var link = "https://www.boca.gov.tw" + countryElement?.GetAttribute("href");
                var cautionElement = post.QuerySelector("[data-title=最新警示提醒]");
                cautionElement?.RemoveChild(cautionElement.QuerySelector("span")!); //移除<span>
                var caution = cautionElement?.InnerHtml;


                return new TravelWarning
                {
                    Country = country!,
                    Link = link,
                    Caution = caution!
                };
            })
            .Where(post => post.Country != null).ToList();

            return travelWarnings;
        }
    }
}
