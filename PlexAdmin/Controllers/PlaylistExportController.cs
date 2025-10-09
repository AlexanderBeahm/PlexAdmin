using Microsoft.AspNetCore.Mvc;
using PlexAdmin.Services;
using System.Text;

namespace PlexAdmin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlaylistExportController : ControllerBase
    {
        private readonly IPlexService _plexService;
        private readonly ILogger<PlaylistExportController> _logger;

        public PlaylistExportController(IPlexService plexService, ILogger<PlaylistExportController> logger)
        {
            _plexService = plexService;
            _logger = logger;
        }

        [HttpGet("{playlistId}")]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> ExportM3U(string playlistId, [FromQuery] string playlistName)
        {
            try
            {
                _logger.LogInformation("Export M3U requested for playlist {PlaylistId}", playlistId);

                // Get playlist items
                var items = await _plexService.GetPlaylistItemsAsync(playlistId);

                if (items == null || !items.Any())
                {
                    _logger.LogWarning("Playlist {PlaylistId} is empty or not found", playlistId);
                    return NotFound("Playlist is empty or not found");
                }

                // Generate M3U content
                var m3uContent = _plexService.GenerateM3UContent(items, playlistName);

                // Sanitize filename
                var invalidChars = Path.GetInvalidFileNameChars();
                var sanitizedName = string.Join("_", playlistName.Split(invalidChars));
                var filename = $"{sanitizedName}.m3u";

                _logger.LogInformation("Successfully generated M3U file: {Filename} with {Size} bytes", filename, m3uContent.Length);

                // Use MemoryStream for better disposal handling
                var bytes = Encoding.UTF8.GetBytes(m3uContent);
                var stream = new MemoryStream(bytes);

                _logger.LogInformation("Returning file response for {Filename}", filename);

                // Return as FileStreamResult with explicit disposal
                return new FileStreamResult(stream, "application/octet-stream")
                {
                    FileDownloadName = filename,
                    EnableRangeProcessing = false
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error exporting playlist {PlaylistId}", playlistId);
                return StatusCode(500, $"Error exporting playlist: {ex.Message}");
            }
        }
    }
}
