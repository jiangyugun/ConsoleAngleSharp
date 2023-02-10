<Query Kind="Program">
  <Output>DataGrids</Output>
  <Reference Relative="..\ConsoleAngleSharp\bin\Debug\net6.0\AngleSharp.dll">E:\Demo Core\ConsoleAngleSharp\ConsoleAngleSharp\bin\Debug\net6.0\AngleSharp.dll</Reference>
  <Namespace>AngleSharp</Namespace>
  <Namespace>AngleSharp.Dom</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
  <RuntimeVersion>6.0</RuntimeVersion>
</Query>

public class Post
{
	public string Title { get; set; }
	public int Push { get; set; }
	public string Link { get; set; }
}

async Task Main()
{
	var config = AngleSharp.Configuration.Default
	.WithDefaultLoader()
	.WithDefaultCookies(); // 加上 `WithDefaultCookies()` 來加上預設的 Cookie
	
	var browser = BrowsingContext.New(config);
	
	var url = new Url("https://www.ptt.cc/bbs/Beauty/index.html");	// 這邊用的型別是 AngleSharp 提供的 AngleSharp.Dom.Url
	browser.SetCookie(url, "over18=1'"); //開啟網頁之前，使用 SetCookie 對目標指定要用的 Cookie
	
    // 使用 OpenAsync 來打開網頁抓回內容
	var document = await browser.OpenAsync(url);
	//document.Body.InnerHtml.Dump();
	
	// 取出所有文章標題
	var postSource = document.QuerySelectorAll("div.r-ent");
	
	//	document
	//		.QuerySelectorAll("div.r-ent")// 指定 class 為 r-ent 的 div
	//		.Select(node => node.InnerHtml) // 直接抓內容出來看看
	//    	.Dump();
	var posts = postSource.Select(post =>{
		var titleElement = post.QuerySelector("div.title > a");
	    var title = titleElement?.InnerHtml;
	    var link = titleElement?.GetAttribute("href");
	    
	    var pushString = post.QuerySelector("div.nrec > span")?.InnerHtml;
	    var pushCount = 
	        pushString == "爆" ? 100 : 
	        Int16.TryParse(pushString, out var push) ? push : 0;
	        
	    return new Post
	    {
	        Title = title,
	        Link = link,
	        Push = pushCount
	    };
	})
	.Where(post => post.Title != null)
	.Dump();
}