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
    }
}
