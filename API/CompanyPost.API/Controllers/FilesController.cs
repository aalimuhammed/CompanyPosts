using Microsoft.AspNetCore.StaticFiles;

namespace CompanyPost.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        public FilesController(IWebHostEnvironment env)
        {
            _env = env;
        }

        [HttpGet("{folder}/{fileName}")]
        public IActionResult GetFile(string folder, string fileName)
        {
            // ── Prevent path traversal (../, absolute paths, etc.) ──
            if (string.IsNullOrWhiteSpace(folder) || string.IsNullOrWhiteSpace(fileName)
                || folder.Contains("..") || fileName.Contains("..")
                || Path.IsPathRooted(folder) || Path.IsPathRooted(fileName))
            {
                return BadRequest("Invalid path.");
            }

            var wwwrootPath = _env.WebRootPath; // typically {ContentRoot}/wwwroot
            var folderPath = Path.Combine(wwwrootPath, folder);
            var fullPath = Path.Combine(folderPath, fileName);

            // ── Make sure the resolved path is still inside wwwroot/{folder} ──
            var normalizedFolder = Path.GetFullPath(folderPath);
            var normalizedFile = Path.GetFullPath(fullPath);
            if (!normalizedFile.StartsWith(normalizedFolder, StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest("Invalid path.");
            }

            if (!System.IO.File.Exists(normalizedFile))
            {
                return NotFound();
            }

            // ── Resolve a proper content type (falls back to octet-stream) ──
            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(normalizedFile, out var contentType))
            {
                contentType = "application/octet-stream";
            }

            var bytes = System.IO.File.ReadAllBytes(normalizedFile);
            return File(bytes, contentType, fileName);
        }
    }
}
