using System.Collections.Generic;

namespace WebApp.Models
{
    public partial class Playlist
    {
        public Playlist()
        {
            this.Tracks = new List<Track>();
        }

        public int PlaylistId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Track> Tracks { get; set; }
    }
}
