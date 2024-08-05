using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JmeterTarget1.Controllers
{
    [Route( "api/[controller]" )]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            // レスポンスデータを作成
            var data = new
            {
                Message = "Server response.",
                Timestamp = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ")
            };

            return Ok( data ); // 200 OK を返す
        }
    }
}
