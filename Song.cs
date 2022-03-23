using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordPlaylistManagerV2
{
    public class Song
    {
        public string Name { get; }
        public string Artist { get; }
        public string? YouTubeLink { get; }

        public Song(string name, string artist, string youTubeLink)
        {
            Name = name;
            Artist = artist;
            YouTubeLink = youTubeLink;
        }
        public Song(string name, string artist)
        {
            Name = name;
            Artist = artist;
        }
    }
}
