using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [Route( "api/[controller]" )]
    [ApiController]
    public class LongPollingController : ControllerBase
    {
        // GET: api/longpolling/longpoll
        [HttpGet( "longpoll" )]
        public async Task<IActionResult> LongPoll()
        {
            // 長時間ポーリングのための遅延
            await Task.Delay( 10000 ); // 10秒待機

            // レスポンスデータを作成
            var data = new
            {
                Message = "Server response after long polling",
                Timestamp = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ")
            };

            return Ok( data ); // 200 OK を返す
        }
    }
}
