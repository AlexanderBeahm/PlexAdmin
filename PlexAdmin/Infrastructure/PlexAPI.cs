using PlexAdmin.Models;
using Newtonsoft.Json;

namespace PlexAdmin.Infrastructure
{
    public class PlexAPI : IPlexAPI
    {
        private PlexHTTPClient Client { get; }
        public PlexAPI(PlexHTTPClient client)
        {
            Client = client;
        }

        public async Task<IList<Playlist>> GetPlaylists()
        {
            var response = await Client.GetAsync("/playlists");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var plexResponse = JsonConvert.DeserializeObject<PlexPlaylistsResponse>(content);

            if (plexResponse?.MediaContainer?.Metadata == null)
            {
                return new List<Playlist>();
            }

            return plexResponse.MediaContainer.Metadata
                .Select(m => m.ToPlaylist())
                .ToList();
        }

        public async Task<IList<PlaylistItem>> GetPlaylistItems(string playlistId)
        {
            var response = await Client.GetAsync($"/playlists/{playlistId}/items");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var plexResponse = JsonConvert.DeserializeObject<PlexPlaylistItemsResponse>(content);

            if (plexResponse?.MediaContainer?.Metadata == null)
            {
                return new List<PlaylistItem>();
            }

            return plexResponse.MediaContainer.Metadata
                .Select(m => m.ToPlaylistItem())
                .ToList();
        }

        public async Task<IList<Server>> GetServers()
        {
            var response = await Client.GetAsync("/servers");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var plexResponse = JsonConvert.DeserializeObject<PlexServersResponse>(content);

            if (plexResponse?.MediaContainer?.Server == null)
            {
                return new List<Server>();
            }

            return plexResponse.MediaContainer.Server
                .Select(s => s.ToServer())
                .ToList();
        }
    }
}
