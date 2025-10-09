using System.Net.Http;
using System.Net.Http.Headers;

namespace PlexAdmin.Infrastructure
{
    // Set up HttpClient for Plex API, see https://github.com/LukeHagar/plexcsharp/issues/10 
    public class PlexHTTPClient : HttpClient, IDisposable
    {
        private readonly HttpClient httpClient = new HttpClient();
        public PlexHTTPClient(Uri plexUrl, string plexToken)
        {
            BaseAddress = plexUrl;
            DefaultRequestHeaders.Add("Accept", "application/json");
            DefaultRequestHeaders.Add("X-Plex-Token", plexToken);
        }

        public new void Dispose() => httpClient.Dispose();
    }
}
