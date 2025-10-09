namespace PlexAdmin.Models
{
    /*
     * <Playlist
     *   ratingKey="61910"
     *   key="/playlists/61910/items"
     *   guid="com.plexapp.agents.none://8120f1d1-cf6f-4fa0-8828-2d710d911e23"
     *   type="playlist"
     *   title="&#10084;&#65039; Tracks"
     *   titleSort="&#65039; Tracks"
     *   summary="All your highly rated tracks, in one convenient place."
     *   smart="1"
     *   playlistType="audio"
     *   composite="/playlists/61910/composite/1744660169"
     *   icon="playlist://image.smart"
     *   viewCount="1"
     *   lastViewedAt="1684353372"
     *   duration="5431000"
     *   leafCount="11"
     *   addedAt="1684353372"
     *   updatedAt="1744660169">
     * </Playlist>
     */
    public class Playlist
    {
        public string RatingKey { get; set; } = string.Empty;
        public string Key { get; set; } = string.Empty;
        public string Guid { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string TitleSort { get; set; } = string.Empty;
        public string Summary { get; set; } = string.Empty;
        public bool Smart { get; set; }
        public string PlaylistType { get; set; } = string.Empty;
        public string Composite { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
        public int ViewCount { get; set; }
        public long LastViewedAt { get; set; }
        public long Duration { get; set; }
        public int LeafCount { get; set; }
        public long AddedAt { get; set; }
        public long UpdatedAt { get; set; }
    }
}
