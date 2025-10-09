using Newtonsoft.Json;

namespace PlexAdmin.Models
{
    public class PlexPlaylistsResponse
    {
        [JsonProperty("MediaContainer")]
        public MediaContainer? MediaContainer { get; set; }
    }

    public class MediaContainer
    {
        [JsonProperty("size")]
        public int Size { get; set; }

        [JsonProperty("Metadata")]
        public List<PlaylistMetadata>? Metadata { get; set; }
    }

    public class PlaylistMetadata
    {
        [JsonProperty("ratingKey")]
        public string? RatingKey { get; set; }

        [JsonProperty("key")]
        public string? Key { get; set; }

        [JsonProperty("guid")]
        public string? Guid { get; set; }

        [JsonProperty("type")]
        public string? Type { get; set; }

        [JsonProperty("title")]
        public string? Title { get; set; }

        [JsonProperty("titleSort")]
        public string? TitleSort { get; set; }

        [JsonProperty("summary")]
        public string? Summary { get; set; }

        [JsonProperty("smart")]
        public string? SmartString { get; set; }

        [JsonProperty("playlistType")]
        public string? PlaylistType { get; set; }

        [JsonProperty("composite")]
        public string? Composite { get; set; }

        [JsonProperty("icon")]
        public string? Icon { get; set; }

        [JsonProperty("viewCount")]
        public int ViewCount { get; set; }

        [JsonProperty("lastViewedAt")]
        public long LastViewedAt { get; set; }

        [JsonProperty("duration")]
        public long Duration { get; set; }

        [JsonProperty("leafCount")]
        public int LeafCount { get; set; }

        [JsonProperty("addedAt")]
        public long AddedAt { get; set; }

        [JsonProperty("updatedAt")]
        public long UpdatedAt { get; set; }

        public Playlist ToPlaylist()
        {
            return new Playlist
            {
                RatingKey = RatingKey ?? string.Empty,
                Key = Key ?? string.Empty,
                Guid = Guid ?? string.Empty,
                Type = Type ?? string.Empty,
                Title = Title ?? string.Empty,
                TitleSort = TitleSort ?? string.Empty,
                Summary = Summary ?? string.Empty,
                Smart = SmartString == "1",
                PlaylistType = PlaylistType ?? string.Empty,
                Composite = Composite ?? string.Empty,
                Icon = Icon ?? string.Empty,
                ViewCount = ViewCount,
                LastViewedAt = LastViewedAt,
                Duration = Duration,
                LeafCount = LeafCount,
                AddedAt = AddedAt,
                UpdatedAt = UpdatedAt
            };
        }
    }
}
