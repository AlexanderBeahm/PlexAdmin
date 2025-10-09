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
    }
}
