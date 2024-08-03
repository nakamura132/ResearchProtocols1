var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// デフォルトファイル (index.html) を使用
app.UseDefaultFiles();
// 静的ファイルを提供
app.UseStaticFiles();

app.Run();
