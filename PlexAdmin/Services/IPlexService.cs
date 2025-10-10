using PlexAdmin.Models;

namespace PlexAdmin.Services
{
    public interface IPlexService
    {
        Task<List<PlaylistDto>> GetPlaylistsAsync();
        Task<List<PlaylistItemDto>> GetPlaylistItemsAsync(string playlistId);
        string GenerateM3UContent(List<PlaylistItemDto> items, string playlistName);
        Task<List<ServerDto>> GetServersAsync();
    }
}
