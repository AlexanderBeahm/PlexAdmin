namespace PlexAdmin.Models
{
    /// <summary>
    /// Domain model representing an item within a Plex playlist
    /// </summary>
    public class PlaylistItem
    {
        public string RatingKey { get; set; } = string.Empty;
        public string Key { get; set; } = string.Empty;
        public string Guid { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Summary { get; set; } = string.Empty;
        public int Year { get; set; }
        public long Duration { get; set; }
        public double Rating { get; set; }
        public string Thumb { get; set; } = string.Empty;
        public string Art { get; set; } = string.Empty;
        public long AddedAt { get; set; }
        public long UpdatedAt { get; set; }
        public int Index { get; set; }
        public string FilePath { get; set; } = string.Empty;
    }
}
