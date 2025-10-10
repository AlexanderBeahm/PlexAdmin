namespace PlexAdmin.Models
{
    /// <summary>
    /// Domain model representing a Plex server
    /// </summary>
    public class Server
    {
        public string Name { get; set; } = string.Empty;
        public string Host { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public int Port { get; set; }
        public string MachineIdentifier { get; set; } = string.Empty;
        public string Version { get; set; } = string.Empty;
    }
}
