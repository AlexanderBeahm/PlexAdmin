using PlexAdmin.Models;

namespace PlexAdmin.Infrastructure
{
    public interface IPlexAPI
    {
        Task<IList<Playlist>> GetPlaylists();
        Task<IList<PlaylistItem>> GetPlaylistItems(string playlistId);
    }
}
