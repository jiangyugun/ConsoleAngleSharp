using AngleSharp;

//建立Browser的配置
var config = AngleSharp.Configuration.Default.WithDefaultLoader();

//根據配置建立出Browser
var browser = BrowsingContext.New(config);

Console.WriteLine("Hello, World!");
