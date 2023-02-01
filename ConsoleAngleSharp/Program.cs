using AngleSharp;
using AngleSharp.Dom;
using LINQPad;

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
