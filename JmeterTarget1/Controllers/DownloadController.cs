using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Threading.Tasks;

namespace JmeterTarget1.Controllers
{
    [ApiController]
    [Route( "api/[controller]" )]
    public class DownloadController : ControllerBase
    {
        private readonly string _fileDirectory;

        public DownloadController( IWebHostEnvironment env )
        {
            // wwwroot/files フォルダーのパスを設定
            _fileDirectory = Path.Combine( env.WebRootPath, "files" );
        }

        [HttpGet]
        public async Task<IActionResult> GetFileAsync( [FromQuery] int filesize )
        {
            // ファイルサイズのバリデーション
            if ( filesize < 1 || filesize > 1000000 )
            {
                return BadRequest( "無効なファイルサイズパラメータです。1 から 1,000,000 KB の範囲で指定してください。" );
            }

            // ファイル名を生成
            string fileName = $"file_{filesize.ToString("D7")}.json";
            string filePath = Path.Combine(_fileDirectory, fileName);

            // ファイルが存在するか確認
            if ( !System.IO.File.Exists( filePath ) )
            {
                return NotFound( "指定されたファイルは存在しません。" );
            }

            // ファイルの内容を読み込み
            string fileContent;
            using ( var reader = new StreamReader( filePath ) )
            {
                fileContent = await reader.ReadToEndAsync();
            }

            // JSON レスポンスを作成
            var response = new
            {
                Filename = fileName,
                Filesize = filesize,
                Content = fileContent
            };

            return Ok( response );
        }
    }
}
