using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordPlaylistManagerV2
{
    public class Playlist
    {
        public List<Song> Songs = new List<Song>();
        public string Name { get; set; }
        public Playlist(string name)
        {
            Name = name;
        }
        public Playlist() { }
        public void AddSong(Song song)
        {
            Songs.Add(song);
        }
        public Song GetSong(string songName, string songArtist)
        {
            foreach (Song song in Songs)
            {
                if(song.Name == songName && song.Artist == songArtist)
                {
                    return song;
                }
            }
            return null;
        }
        public void RemoveSong(Song song)
        {
            Songs.Remove(song);
        }
        public void ShufflePlaylist()
        {
            Random random = new Random();
            for (int i = 0; i < Songs.Count; i++)
            {
                int randomIndex = random.Next(i, Songs.Count);
                Song song = Songs[i];
                Songs[i] = Songs[randomIndex];
                Songs[randomIndex] = song;
            }
            Console.WriteLine("Playlist has been shuffled.");
            Console.WriteLine();
            Console.ReadLine();
        }
    }
}
