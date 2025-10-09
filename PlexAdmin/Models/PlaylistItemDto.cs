namespace PlexAdmin.Models
{
    /// <summary>
    /// Data Transfer Object for playlist items displayed in the UI
    /// </summary>
    public class PlaylistItemDto
    {
        public string Id { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public int Year { get; set; }
        public string Duration { get; set; } = string.Empty;
        public long DurationMs { get; set; }
        public string Summary { get; set; } = string.Empty;
        public double Rating { get; set; }
        public string FilePath { get; set; } = string.Empty;

        /// <summary>
        /// Formats duration from milliseconds to human-readable format (e.g., "1h 23m")
        /// </summary>
        public static string FormatDuration(long durationMs)
        {
            if (durationMs <= 0) return "0m";

            var totalSeconds = durationMs / 1000;
            var hours = totalSeconds / 3600;
            var minutes = (totalSeconds % 3600) / 60;

            if (hours > 0)
            {
                return minutes > 0 ? $"{hours}h {minutes}m" : $"{hours}h";
            }

            return $"{minutes}m";
        }
    }
}
