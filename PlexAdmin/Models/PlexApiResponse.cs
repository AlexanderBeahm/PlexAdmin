using Newtonsoft.Json;

namespace PlexAdmin.Models
{
    public class PlexPlaylistsResponse
    {
        [JsonProperty("MediaContainer")]
        public MediaContainer? MediaContainer { get; set; }
    }

    public class PlexPlaylistItemsResponse
    {
        [JsonProperty("MediaContainer")]
        public PlaylistItemsMediaContainer? MediaContainer { get; set; }
    }

    public class MediaContainer
    {
        [JsonProperty("size")]
        public int Size { get; set; }

        [JsonProperty("Metadata")]
        public List<PlaylistMetadata>? Metadata { get; set; }
    }

    public class PlaylistItemsMediaContainer
    {
        [JsonProperty("size")]
        public int Size { get; set; }

        [JsonProperty("composite")]
        public string? Composite { get; set; }

        [JsonProperty("duration")]
        public long Duration { get; set; }

        [JsonProperty("leafCount")]
        public int LeafCount { get; set; }

        [JsonProperty("playlistType")]
        public string? PlaylistType { get; set; }

        [JsonProperty("ratingKey")]
        public string? RatingKey { get; set; }

        [JsonProperty("smart")]
        public string? Smart { get; set; }

        [JsonProperty("title")]
        public string? Title { get; set; }

        [JsonProperty("Metadata")]
        public List<PlaylistItemMetadata>? Metadata { get; set; }
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

    public class PlaylistItemMetadata
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

        [JsonProperty("summary")]
        public string? Summary { get; set; }

        [JsonProperty("year")]
        public int Year { get; set; }

        [JsonProperty("duration")]
        public long Duration { get; set; }

        [JsonProperty("rating")]
        public double Rating { get; set; }

        [JsonProperty("thumb")]
        public string? Thumb { get; set; }

        [JsonProperty("art")]
        public string? Art { get; set; }

        [JsonProperty("addedAt")]
        public long AddedAt { get; set; }

        [JsonProperty("updatedAt")]
        public long UpdatedAt { get; set; }

        [JsonProperty("index")]
        public int Index { get; set; }

        [JsonProperty("Media")]
        public List<MediaInfo>? Media { get; set; }

        public PlaylistItem ToPlaylistItem()
        {
            return new PlaylistItem
            {
                RatingKey = RatingKey ?? string.Empty,
                Key = Key ?? string.Empty,
                Guid = Guid ?? string.Empty,
                Type = Type ?? string.Empty,
                Title = Title ?? string.Empty,
                Summary = Summary ?? string.Empty,
                Year = Year,
                Duration = Duration,
                Rating = Rating,
                Thumb = Thumb ?? string.Empty,
                Art = Art ?? string.Empty,
                AddedAt = AddedAt,
                UpdatedAt = UpdatedAt,
                Index = Index,
                FilePath = Media?.FirstOrDefault()?.Part?.FirstOrDefault()?.File ?? string.Empty
            };
        }
    }

    public class MediaInfo
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("duration")]
        public long Duration { get; set; }

        [JsonProperty("bitrate")]
        public int Bitrate { get; set; }

        [JsonProperty("container")]
        public string? Container { get; set; }

        [JsonProperty("Part")]
        public List<PartInfo>? Part { get; set; }
    }

    public class PartInfo
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("key")]
        public string? Key { get; set; }

        [JsonProperty("duration")]
        public long Duration { get; set; }

        [JsonProperty("file")]
        public string? File { get; set; }

        [JsonProperty("size")]
        public long Size { get; set; }

        [JsonProperty("container")]
        public string? Container { get; set; }
    }

    public class PlexServersResponse
    {
        [JsonProperty("MediaContainer")]
        public ServersMediaContainer? MediaContainer { get; set; }
    }

    public class ServersMediaContainer
    {
        [JsonProperty("size")]
        public int Size { get; set; }

        [JsonProperty("Server")]
        public List<ServerMetadata>? Server { get; set; }
    }

    public class ServerMetadata
    {
        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("host")]
        public string? Host { get; set; }

        [JsonProperty("address")]
        public string? Address { get; set; }

        [JsonProperty("port")]
        public int Port { get; set; }

        [JsonProperty("machineIdentifier")]
        public string? MachineIdentifier { get; set; }

        [JsonProperty("version")]
        public string? Version { get; set; }

        public Server ToServer()
        {
            return new Server
            {
                Name = Name ?? string.Empty,
                Host = Host ?? string.Empty,
                Address = Address ?? string.Empty,
                Port = Port,
                MachineIdentifier = MachineIdentifier ?? string.Empty,
                Version = Version ?? string.Empty
            };
        }
    }
}
