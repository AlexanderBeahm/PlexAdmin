using PlexAdmin.Models;

namespace PlexAdmin.Infrastructure
{
    public interface IPlexAPI
    {
        public Task<IList<Playlist>> GetPlaylists();

    }
}
