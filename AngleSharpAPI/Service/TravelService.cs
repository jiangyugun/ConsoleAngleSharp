using AngleSharp.Dom;
using AngleSharp;
using AngleSharpAPI.Entity;
using Microsoft.Extensions.Hosting;
using AngleSharpAPI.Interface;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using System.Net.Http.Headers;

namespace AngleSharpAPI.Service
{
    public class TravelService: ITravelInterface
    {
        public async Task<DataResultModel<Info>> GetActivitiesAsync()
        {
            DataResultModel<Info> dataResultModel = new();
            using (HttpClientHandler handler = new())
            {
                using HttpClient client = new();
                try
                {
                    var url = "https://media.taiwan.net.tw/XMLReleaseALL_public/activity_C_f.json";
                    HttpResponseMessage? response = null;
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    response = await client.GetAsync(url);

                    if (response != null)
                    {
                        var strResult = await response.Content.ReadAsStringAsync();
                        var activities = JsonConvert.DeserializeObject<Activity>(strResult);
                        if (response.IsSuccessStatusCode)
                        {
                            dataResultModel.DataList = activities.XML_Head.Infos.Info.ToList();
                            dataResultModel.Result = true;
                            dataResultModel.Msg = "成功取得資料";
                        }
                        else
                        {
                            if (dataResultModel.DataList.ToList().Count <= 0)
                            {
                                dataResultModel.Msg = String.Format("Error Code:{0}, Error Message:{1}", response.StatusCode, response.RequestMessage);
                            }
                        }
                    }
                    else
                    {
                        dataResultModel.Msg = "應用程式呼叫API發生異常";
                    }
                }
                catch (Exception ex)
                {
                    dataResultModel.Msg = ex.Message;
                }
            }
            return dataResultModel;
        }

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

        public async Task<DataResultModel<PoiNearList>> GetViewPoint(string accessToken)
        {
            string url = "https://uds.api.liontravel.com/api/v2/PoiNearList?px=121.518831&py=25.035702&radius=500";
            DataResultModel<PoiNearList> dataResultModel = new();

            using (HttpClientHandler handler = new())
            {
                try
                {
                    using HttpClient client = new();
                    HttpResponseMessage? response = null;
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic ", accessToken);
                    response = await client.GetAsync(url);

                    var strResult = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<PoiNearList>(strResult);
                    var test = result.Data.pois.ToList();
                    //dataResultModel.DataList = 
                    dataResultModel.Result = true;
                    dataResultModel.Msg = "成功取得資料";
                }
                catch(Exception ex)
                {
                    dataResultModel.Msg = ex.ToString();
                }
            }
            return dataResultModel;
        }
    }
}
