using AngleSharp;
using AngleSharp.Dom;
using Newtonsoft.Json;

void test()
{
    var config = AngleSharp.Configuration.Default.WithDefaultLoader();
    var browser = BrowsingContext.New(config);

    // 這邊用的型別是 AngleSharp 提供的 AngleSharp.Dom.Url
    var url = new Url("https://dannyliu.me/");

    // 使用 OpenAsync 來打開網頁抓回內容
    var document = browser.OpenAsync(url).Result;

    //QuerySelector(".entry-content")找出class="entry-content"的所有元素
    var contents = document.QuerySelectorAll(".entry-content");

    foreach (var c in contents)
    {
        //取得每個元素的TextContent
        Console.WriteLine(c.TextContent);
    }
}

async Task Main()
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

    var post = postSource.Select(post =>
    {
        var countryElement = post.QuerySelector("[data-title=國家] > a");
        var country = countryElement?.InnerHtml;
        var link = countryElement?.GetAttribute("href");
        var cautionElement = post.QuerySelector("[data-title=最新警示提醒]");
        cautionElement?.RemoveChild(cautionElement.QuerySelector("span")!); //移除<span>
        var caution = cautionElement?.InnerHtml;

        return new Post
        {
            Title = country!,
            Link = link!,
            Caution = caution!
        };
    })
    .Where(post => post.Title != null).ToList();
    var jsonTest = JsonConvert.SerializeObject(post);

    Console.WriteLine(jsonTest);
    document.Close(); //把開啟的網頁內容順手清掉
}

await Main();