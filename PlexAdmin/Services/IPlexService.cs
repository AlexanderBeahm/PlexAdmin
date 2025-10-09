using PlexAdmin.Models;

namespace PlexAdmin.Services
{
    public interface IPlexService
    {
        Task<List<PlaylistDto>> GetPlaylistsAsync();
    }
}
