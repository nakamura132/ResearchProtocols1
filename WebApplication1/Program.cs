var builder = WebApplication.CreateBuilder(args);

// コントローラーサービスを追加
builder.Services.AddControllers();

var app = builder.Build();

// デフォルトファイル (index.html) を使用
app.UseDefaultFiles();
// 静的ファイルを提供
app.UseStaticFiles();
// コントローラーをマッピング
app.MapControllers();

app.Run();
