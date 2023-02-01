using AngleSharp;
using AngleSharp.Dom;
using LINQPad;

// 這邊為了方便處理也順便把 `Main` 改成非同步的版本
async Task Main()
{
	var config = AngleSharp.Configuration.Default.WithDefaultLoader();
	var browser = BrowsingContext.New(config);

	// 這邊用的型別是 AngleSharp 提供的 AngleSharp.Dom.Url
	var url = new Url("https://www.ptt.cc/bbs/Beauty/index.html");

	// 使用 OpenAsync 來打開網頁抓回內容
	var document = await browser.OpenAsync(url);
	document.Dump();
}

Console.WriteLine("Hello, World!");
