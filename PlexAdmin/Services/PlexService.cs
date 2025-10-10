using PlexAdmin.Infrastructure;
using PlexAdmin.Models;

namespace PlexAdmin.Services
{
    public class PlexService : IPlexService
    {
        private readonly IPlexAPI _plexApi;

        public PlexService(IPlexAPI plexApi)
        {
            _plexApi = plexApi;
        }


        public async Task<List<PlaylistDto>> GetPlaylistsAsync()
        {
            try
            {
                var playlists = await _plexApi.GetPlaylists();

                return playlists
                    .Select(p => new PlaylistDto
                    {
                        Id = p.RatingKey,
                        Name = p.Title
                    })
                    .ToList();
            }
            catch (Exception)
            {
                // Log exception in production
                return new List<PlaylistDto>();
            }
        }

        public async Task<List<PlaylistItemDto>> GetPlaylistItemsAsync(string playlistId)
        {
            try
            {
                var items = await _plexApi.GetPlaylistItems(playlistId);

                return items
                    .Select(i => new PlaylistItemDto
                    {
                        Id = i.RatingKey,
                        Title = i.Title,
                        Type = i.Type,
                        Year = i.Year,
                        Duration = PlaylistItemDto.FormatDuration(i.Duration),
                        DurationMs = i.Duration,
                        Summary = i.Summary,
                        Rating = i.Rating,
                        FilePath = i.FilePath
                    })
                    .ToList();
            }
            catch (Exception)
            {
                // Log exception in production
                return new List<PlaylistItemDto>();
            }
        }

        public string GenerateM3UContent(List<PlaylistItemDto> items, string playlistName)
        {
            var sb = new System.Text.StringBuilder();

            // Add M3U header
            sb.AppendLine("#EXTM3U");
            sb.AppendLine($"# Playlist: {playlistName}");
            sb.AppendLine();

            // Add each item
            foreach (var item in items)
            {
                if (string.IsNullOrEmpty(item.FilePath))
                {
                    // Skip items without file paths
                    continue;
                }

                // Convert duration from milliseconds to seconds
                var durationSeconds = (int)(item.DurationMs / 1000);

                // Add EXTINF line with duration and title
                sb.AppendLine($"#EXTINF:{durationSeconds},{item.Title}");

                // Add file path
                sb.AppendLine(item.FilePath);
            }

            return sb.ToString();
        }

        public async Task<List<ServerDto>> GetServersAsync()
        {
            try
            {
                var servers = await _plexApi.GetServers();

                return servers
                    .Select(s => new ServerDto
                    {
                        Name = s.Name,
                        Address = s.Address
                    })
                    .ToList();
            }
            catch (Exception)
            {
                // Log exception in production
                return new List<ServerDto>();
            }
        }
    }
}
