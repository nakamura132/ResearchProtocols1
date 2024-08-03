using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using System.Text;

namespace WebApplication1.Controllers
{
    /// <summary>
    /// WebSocket通信を管理するコントローラ。
    /// </summary>
    [ApiController]
    [Route( "api/[controller]" )]
    public class WebSocketController : ControllerBase
    {
        /// <summary>
        /// WebSocketへの接続を開始し、メッセージを送信します。
        /// </summary>
        [HttpGet( "connect" )]
        public async Task Get()
        {
            if ( HttpContext.WebSockets.IsWebSocketRequest )
            {
                using WebSocket webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
                await SendMessages( webSocket );
            }
            else
            {
                HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            }
        }

        /// <summary>
        /// WebSocketを使用してクライアントに周期的にメッセージを送信します。
        /// </summary>
        /// <param name="webSocket">接続されたWebSocketインスタンス</param>
        private async Task SendMessages( WebSocket webSocket )
        {
            while ( webSocket.State == WebSocketState.Open )
            {
                var now = DateTime.UtcNow; // 現在のUTC時間
                var message = $"現在の時間: {now} - こんにちは！";
                var bytes = Encoding.UTF8.GetBytes(message);

                await webSocket.SendAsync( new ArraySegment<byte>( bytes ), WebSocketMessageType.Text, true, CancellationToken.None );
                await Task.Delay( 3000 ); // 3秒間隔で送信
            }
        }
    }
}
