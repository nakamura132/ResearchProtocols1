using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [Route( "api/[controller]" )]
    [ApiController]
    public class SseController : ControllerBase
    {
        // GET: api/sse/stream
        [HttpGet( "stream" )]
        public async Task Stream()
        {
            Response.Headers.Append( "Content-Type", "text/event-stream" );

            while ( !HttpContext.RequestAborted.IsCancellationRequested )
            {
                var data = new
                {
                    Message = "Server-sent event",
                    Timestamp = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ")
                };

                // データを送信
                await Response.WriteAsync( $"data: {System.Text.Json.JsonSerializer.Serialize( data )}\n\n" );
                await Response.Body.FlushAsync();

                // 5秒間隔で送信
                await Task.Delay( 5000 );
            }
        }
    }
}
