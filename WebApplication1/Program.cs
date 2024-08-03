
using Microsoft.AspNetCore.WebSockets;

var builder = WebApplication.CreateBuilder(args);

// コントローラーサービスを追加
builder.Services.AddControllers();

var app = builder.Build();

// WebSocket サポートを追加
app.UseWebSockets();

// 実際に WebSocket 接続を処理するカスタムミドルウェアを追加します
// WebSocket ミドルウェアは、静的ファイルミドルウェアや認証ミドルウェアの前に設定する必要があります。
//app.UseMiddleware<WebSocketMiddleware>();

// デフォルトファイル (index.html) を使用
app.UseDefaultFiles();
// 静的ファイルを提供
app.UseStaticFiles();

// コントローラーをマッピング
app.MapControllers();

app.Run();
