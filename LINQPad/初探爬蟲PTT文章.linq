<Query Kind="Program">
  <Output>DataGrids</Output>
  <Reference Relative="..\ConsoleAngleSharp\bin\Debug\net6.0\AngleSharp.dll">E:\Demo Core\ConsoleAngleSharp\ConsoleAngleSharp\bin\Debug\net6.0\AngleSharp.dll</Reference>
  <Namespace>AngleSharp</Namespace>
  <Namespace>AngleSharp.Dom</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
  <RuntimeVersion>6.0</RuntimeVersion>
</Query>

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