using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace JmeterTarget1.Controllers
{
    [ApiController]
    [Route( "api/[controller]" )]
    public class UploadController : ControllerBase
    {
        // JSONファイルを受信するエンドポイント
        [HttpPost( "json" )]
        public async Task<IActionResult> UploadJson( IFormFile file )
        {
            // ファイルがアップロードされているか確認
            if ( file == null || file.Length == 0 )
            {
                return BadRequest( "ファイルが選択されていません。" );
            }

            // ファイル名を取得
            var fileName = file.FileName;
            var fileSize = file.Length;
            var receivedDate = DateTime.UtcNow;

            // ファイル名が正しい形式か確認
            if ( !fileName.StartsWith( "client_log_" ) || !fileName.EndsWith( ".json" ) )
            {
                return BadRequest( "不正なファイル名です。" );
            }

            //// ファイルを保存するパス（例: wwwroot/uploads）
            //var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", fileName);

            //// ファイルを保存
            //using ( var stream = new FileStream( filePath, FileMode.Create ) )
            //{
            //    await file.CopyToAsync( stream );
            //}

            // 応答メッセージを作成
            var responseMessage = new
            {
                FileName = fileName,
                FileSize = fileSize,
                ReceivedDate = receivedDate
            };

            return Ok( responseMessage );
        }

        // バイナリーデータを受信するエンドポイント
        [HttpPost( "bin" )]
        public async Task<IActionResult> UploadBin( IFormFile file )
        {
            // ファイルがアップロードされているか確認
            if ( file == null || file.Length == 0 )
            {
                return BadRequest( "ファイルが選択されていません。" );
            }

            // ファイル名を取得
            var fileName = file.FileName;
            var fileSize = file.Length;
            var receivedDate = DateTime.UtcNow;

            // ファイル名が正しい形式か確認
            if ( !fileName.StartsWith( "client_data_" ) || !fileName.EndsWith( ".bin" ) )
            {
                return BadRequest( "不正なファイル名です。" );
            }

            //// ファイルを保存するパス（例: wwwroot/uploads）
            //var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", fileName);

            //// ファイルを保存
            //using ( var stream = new FileStream( filePath, FileMode.Create ) )
            //{
            //    await file.CopyToAsync( stream );
            //}

            // 応答メッセージを作成
            var responseMessage = new
            {
                FileName = fileName,
                FileSize = fileSize,
                ReceivedDate = receivedDate
            };

            return Ok( responseMessage );
        }
    }
}
