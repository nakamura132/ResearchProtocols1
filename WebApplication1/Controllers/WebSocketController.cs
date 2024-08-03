using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;

namespace WebApplication1.Controllers
{
    /// <summary>
    /// WebSocket用のAPIコントローラー
    /// </summary>
    [ApiController]
    [Route( "api/[controller]" )]
    public class WebSocketController : ControllerBase
    {
        /// <summary>
        /// WebSocket接続エンドポイント
        /// </summary>
        /// <param name="context">HTTPコンテキスト</param>
        /// <returns>非同期タスク</returns>
        [HttpGet( "connect" )]
        public async Task Connect()
        {
            // WebSocket以外の要求には400ステータスコードを返す
            if ( !HttpContext.WebSockets.IsWebSocketRequest )
            {
                HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                await HttpContext.Response.WriteAsync( "WebSocket以外の要求は無効です。" );
                return;
            }

            // WebSocket要求の場合はWebSocketを取得
            var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();

            try
            {
                // 3秒ごとにメッセージを送信するタスクを開始
                _ = Task.Run( async () =>
                {
                    var random = new Random();
                    while ( webSocket.State == WebSocketState.Open )
                    {
                        var message = $"{DateTime.UtcNow:yyyy-MM-ddTHH:mm:ss} : 任意のメッセージ {random.Next(1000)}";
                        var buffer = System.Text.Encoding.UTF8.GetBytes(message);
                        var segment = new ArraySegment<byte>(buffer);
                        await webSocket.SendAsync( segment, WebSocketMessageType.Text, true, CancellationToken.None );
                        await Task.Delay( 3000 );
                    }
                } );

                // クライアントからのメッセージを受信して処理する
                var buffer = new byte[1024 * 4];
                WebSocketReceiveResult result;
                do
                {
                    result = await webSocket.ReceiveAsync( new ArraySegment<byte>( buffer ), CancellationToken.None );
                    if ( result.MessageType == WebSocketMessageType.Text )
                    {
                        var receivedMessage = System.Text.Encoding.UTF8.GetString(buffer, 0, result.Count);
                        var responseMessage = $"{DateTime.UtcNow:yyyy-MM-ddTHH:mm:ss} : 次のメッセージを受信しました: {receivedMessage}";
                        var responseBuffer = System.Text.Encoding.UTF8.GetBytes(responseMessage);
                        var responseSegment = new ArraySegment<byte>(responseBuffer);
                        await webSocket.SendAsync( responseSegment, WebSocketMessageType.Text, true, CancellationToken.None );
                    }
                } while ( !result.CloseStatus.HasValue );

                // WebSocketがクローズされた場合、通信を終了する
                await webSocket.CloseAsync( result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None );
            }
            catch ( Exception ex )
            {
                // エラーハンドリング
                Console.WriteLine( $"WebSocketエラー: {ex.Message}" );
            }
            finally
            {
                // WebSocketが閉じられたときの処理
                if ( webSocket.State == WebSocketState.Open )
                {
                    await webSocket.CloseAsync( WebSocketCloseStatus.InternalServerError, "エラーが発生しました", CancellationToken.None );
                }
            }
        }
    }
}
