using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordPlaylistManagerV2
{
    public class PlaylistManager
    {
        public Playlist CurrentlySelectedPlaylist { get; set; } = new Playlist();
        public List<Playlist> Playlists = new List<Playlist>();
        public string StartOfPlaylistStr = "[Start of Playlist]";
        public string EndOfPlaylistStr = "[End of Playlist]";
        public string CurrentPlaylistStr = "[Current Playlist]";
        public string FilePath { get; set; } = "";
        public PlaylistManager(string filePath)
        {
            FilePath = filePath;
        }
        public PlaylistManager()
        {

        }
        public void AddPlaylist(Playlist playlist)
        {
            Playlists.Add(playlist);
        }
        public void SetFileToReadFrom(string filePath)
        {
            FilePath = filePath;
        }
        public void RetrievePlaylists()
        {
            try
            {
                using(StreamReader sr = new StreamReader(FilePath))
                {
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        if (line == StartOfPlaylistStr)
                        {
                            string playlistName = sr.ReadLine();
                            Playlist currentPlaylist = new Playlist(playlistName);
                            line = sr.ReadLine();
                            while (line != EndOfPlaylistStr)
                            {
                                string[] songArray = line.Split('|');
                                if (songArray.Length == 2)
                                {
                                    Song currentSong = new Song(songArray[0], songArray[1]);
                                    currentPlaylist.AddSong(currentSong);
                                }
                                if (songArray.Length == 3)
                                {
                                    Song currentSong = new Song(songArray[0], songArray[1], songArray[2]);
                                    currentPlaylist.AddSong(currentSong);

                                }
                                line = sr.ReadLine();
                            }
                            AddPlaylist(currentPlaylist);
                        }
                        if (line == CurrentPlaylistStr)
                        {
                            string playlistName = sr.ReadLine();
                            Playlist currentPlaylist = new Playlist(playlistName);
                            line = sr.ReadLine();
                            while (line != EndOfPlaylistStr)
                            {
                                string[] songArray = line.Split('|');
                                if (songArray.Length == 2)
                                {
                                    Song currentSong = new Song(songArray[0], songArray[1]);
                                    currentPlaylist.AddSong(currentSong);
                                }
                                if (songArray.Length == 3)
                                {
                                    Song currentSong = new Song(songArray[0], songArray[1], songArray[2]);
                                    currentPlaylist.AddSong(currentSong);
                                }
                                line = sr.ReadLine();
                            }
                            AddPlaylist(currentPlaylist);
                            CurrentlySelectedPlaylist = currentPlaylist;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void SavePlaylists()
        {
            List<string> linesToWrite = new List<string>();
            linesToWrite.Add("List of Playlists and Songs Within");
            foreach (Playlist playlist in Playlists)
            {
                if(playlist.Name == CurrentlySelectedPlaylist.Name)
                {
                    linesToWrite.Add(CurrentPlaylistStr);
                }
                else
                {
                    linesToWrite.Add(StartOfPlaylistStr);
                }
                linesToWrite.Add(playlist.Name);
                List<Song> songs = playlist.Songs;
                foreach (Song song in songs)
                {
                    if (song.YouTubeLink == null)
                    {
                        linesToWrite.Add($"{song.Name}|{song.Artist}");
                    }
                    else
                    {
                        linesToWrite.Add($"{song.Name}|{song.Artist}|{song.YouTubeLink}");
                    }
                }
                linesToWrite.Add(EndOfPlaylistStr);
            }
            try
            {
                File.WriteAllLines(FilePath, linesToWrite);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public Playlist PlaylistSelctor()
        {
            Console.WriteLine("Which playlist would you like to use?");
            Console.WriteLine("--------------------------------------------------");
            Dictionary<int, Playlist> indexedPlaylists = new Dictionary<int, Playlist>();
            int counter = 1;
            foreach(Playlist playlist in Playlists)
            {
                Console.WriteLine($"({counter}) {playlist.Name}");
                indexedPlaylists.Add(counter, playlist);
                counter++;
            }
            Console.WriteLine("--------------------------------------------------");
            int playlistKey = 0;
            while(playlistKey < 1 || playlistKey > indexedPlaylists.Count)
            {
                try
                {
                    playlistKey = int.Parse(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine("Please enter a valid number!");
                }
                if(playlistKey < 1 || playlistKey > indexedPlaylists.Count)
                {
                    Console.WriteLine("Please enter the number that corresponds to the playlist you would like to access");
                }
            }
            return indexedPlaylists[playlistKey];
        }
        public Playlist CreateNewPlaylist()
        {
            bool hasPlaylistWithSameName = false;
            string playlistName = "";
            Console.WriteLine("Please enter a name for your new playlist");
            while (hasPlaylistWithSameName || playlistName == "")
            {
                hasPlaylistWithSameName = false;
                playlistName = Console.ReadLine();
                foreach (Playlist playlist in Playlists)
                {
                    if (playlist.Name == playlistName)
                    {
                        hasPlaylistWithSameName = true;
                    }
                }
                if(hasPlaylistWithSameName)
                {
                    Console.WriteLine("There is already a playlist with that name please choose another");
                }
                if(playlistName == "")
                {
                    Console.WriteLine("Playlist name cannot be empty");
                }

            }
            Playlist newPlaylist = new Playlist(playlistName);
            Playlists.Add(newPlaylist);
            return newPlaylist;
        }
        public void AddSong()
        {
            Console.WriteLine("adding songs");
            Console.ReadLine();
        }
        public void RemoveSongs()
        {
            Console.WriteLine("removing songs");
            Console.ReadLine();
        }
        public void UpdateYouTubeLink()
        {
            Console.WriteLine("updating YouTube link");
            Console.ReadLine();
        }
    }
}
