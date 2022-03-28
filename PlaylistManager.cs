using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DiscordPlaylistManagerV2
{
    public class PlaylistManager
    {
        public Playlist? CurrentlySelectedPlaylist { get; set; }
        public List<Playlist> Playlists = new List<Playlist>();
        private string StartOfPlaylistStr = "[Start of Playlist]";
        private string EndOfPlaylistStr = "[End of Playlist]";
        private string CurrentPlaylistStr = "[Current Playlist]";
        public string ReadWriteFilePath = Directory.GetCurrentDirectory() + "\\DiscordPlaylistManagerList.txt";
        private string AHKFilePath = Directory.GetCurrentDirectory() + "\\playlist.ahk";
        private int SongsPerFile = 50;
        public PlaylistManager()
        {

        }
        public void AddPlaylist(Playlist playlist)
        {
            Playlists.Add(playlist);
        }
        public void RemovePlaylist()
        {
            if(CurrentlySelectedPlaylist != null)
            {
                Console.Write($"Are you sure you want to delete the currently selected playlist '{CurrentlySelectedPlaylist.Name}' this action cannot be undone (y/n) ");
                string delete = Display.PromptForYesNo();
                if (delete == "y")
                {
                    Playlists.Remove(CurrentlySelectedPlaylist);
                    if (Playlists.Count == 0)
                    {
                        Display.ErrorMessage("There are no longer any playlists");
                        CurrentlySelectedPlaylist = null;
                    }
                    else
                    {

                        CurrentlySelectedPlaylist = Playlists[0];
                        Display.SuccessMessage($"{CurrentlySelectedPlaylist.Name} is now selected");
                    }
                }
            }
            else
            {
                Display.ErrorMessage("There is no playlist to remove");
            }
        }
        public void RenamePlaylist()
        {
            if(CurrentlySelectedPlaylist != null)
            {
                Console.Write($"What would you like to rename '{CurrentlySelectedPlaylist.Name}' to: ");
                Display.GreenText();
                string newName = Console.ReadLine();
                Console.WriteLine();
                Display.WhiteText();
                if (newName != "")
                {
                    bool nameExists = false;
                    foreach (Playlist playlist in Playlists)
                    {
                        if (playlist.Name == newName)
                        {
                            nameExists = true;
                            break;
                        }
                    }
                    if (!nameExists)
                    {
                        Console.Write($"Are you sure you want to rename '{CurrentlySelectedPlaylist.Name}' to ");
                        Display.GreenText();
                        Console.Write($"{ newName}");
                        Display.WhiteText();
                        Console.Write("? (y / n) ");
                        string rename = Display.PromptForYesNo();
                        if (rename == "y")
                        {
                            CurrentlySelectedPlaylist.Name = newName;
                            Display.SuccessMessage("Playlist has been renamed");
                        }
                        else
                        {
                            Display.SuccessMessage("Renaming Cancelled");
                        }
                    }
                    else
                    {
                        Display.ErrorMessage("Playlist with the same name already exists");
                    }
                }
                else
                {
                    Display.ErrorMessage("Playlist name cannot be empty");
                }
            }
            else
            {
                Display.ErrorMessage("There is no playlist to rename");
            }
        }
        public void ShufflePlaylist()
        {
            if(CurrentlySelectedPlaylist != null)
            {
                List<Song>? songs = CurrentlySelectedPlaylist.Songs;
                Random random = new Random();
                for (int i = 0; i < CurrentlySelectedPlaylist.Songs.Count; i++)
                {
                    int randomIndex = random.Next(i, songs.Count);
                    Song song = songs[i];
                    songs[i] = songs[randomIndex];
                    songs[randomIndex] = song;
                }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Playlist has been shuffled.");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
            }
            else
            {
                Display.ErrorMessage("There is no playlist to shuffle");
            }
        }
        public void RetrievePlaylists()
        {
            try
            {
                using(StreamReader sr = new StreamReader(ReadWriteFilePath))
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
                            Display.SuccessMessage($"{CurrentlySelectedPlaylist.Name} has been created");
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
                if(CurrentlySelectedPlaylist != null)
                {
                    if (playlist.Name == CurrentlySelectedPlaylist.Name)
                    {
                        linesToWrite.Add(CurrentPlaylistStr);
                    }
                    else
                    {
                        linesToWrite.Add(StartOfPlaylistStr);
                    }
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
                File.WriteAllLines(ReadWriteFilePath, linesToWrite);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public Playlist? PlaylistSelctor()
        {
            if(Playlists.Count > 0)
            {
                Console.WriteLine("Which playlist would you like to use?");
                Console.WriteLine("--------------------------------------------------");
                Dictionary<int, Playlist> indexedPlaylists = new Dictionary<int, Playlist>();
                int counter = 1;
                foreach (Playlist playlist in Playlists)
                {
                    Console.WriteLine($"({counter}) {playlist.Name}");
                    indexedPlaylists.Add(counter, playlist);
                    counter++;
                }
                Console.WriteLine("--------------------------------------------------");
                Console.WriteLine();
                int playlistKey = 0;
                while (playlistKey < 1 || playlistKey > indexedPlaylists.Count)
                {
                    try
                    {
                        Display.YellowText();
                        playlistKey = int.Parse(Console.ReadLine());
                        Display.WhiteText();
                        Console.WriteLine();
                    }
                    catch (Exception)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Please enter a valid number!");
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    if (playlistKey < 1 || playlistKey > indexedPlaylists.Count)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Please enter the number that corresponds to the playlist you would like to access");
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
                Display.SuccessMessage($"{indexedPlaylists[playlistKey].Name} is now selected");
                return indexedPlaylists[playlistKey];
            }
            Display.ErrorMessage("There are no playlists to select from");
            return null;
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
                if (hasPlaylistWithSameName)
                {
                    Display.ErrorMessage("There is already a playlist with that name please choose another");
                }
                if(playlistName == "")
                {
                    Display.ErrorMessage("Playlist name cannot be empty");
                }

            }
            Playlist newPlaylist = new Playlist(playlistName);
            Playlists.Add(newPlaylist);
            Display.GreenText();
            Console.WriteLine();
            return newPlaylist;
        }
        public void AddSongs()
        {
            if(CurrentlySelectedPlaylist != null)
            {
                int numOfSongsAdded = 0;
                string moreSongs = "";
                do
                {
                    Console.Clear();
                    Song newSong = new Song();

                    Console.Write("Please enter the name of the song you'd like to add: ");
                    Display.GreenText();
                    string songName = Console.ReadLine();
                    Console.WriteLine();
                    while (songName == "")
                    {
                        Display.ErrorMessage("Song name cannot be empty");
                        Console.Write("Please enter the name of the song you'd like to add: ");
                        Display.GreenText();
                        songName = Console.ReadLine();
                    }
                    Display.WhiteText();

                    newSong.Name = songName;

                    Console.Write("Please enter the artists name: ");
                    Display.GreenText();
                    string artistName = Console.ReadLine();
                    Console.WriteLine();
                    while (songName == "")
                    {
                        Display.ErrorMessage("Artist name cannot be empty");
                        Console.Write("Please enter the artists name: ");
                        Display.GreenText();
                        songName = Console.ReadLine();
                    }
                    Display.WhiteText();

                    newSong.Artist = artistName;
                    bool hasSong = false;
                    foreach (Song song in CurrentlySelectedPlaylist.Songs)
                    {
                        if (song.Name == newSong.Name && song.Artist == newSong.Artist)
                        {
                            hasSong = true;
                            Display.ErrorMessage($"{song.Name} by {song.Artist} already exists in this playlist");
                            break;
                        }
                    }
                    if (!hasSong)
                    {
                        Console.Write("Please enter the desired YouTube link for this song leave empty if you don't have one: ");
                        Display.GreenText();
                        string youTubeLink = Console.ReadLine();
                        Console.WriteLine();
                        Display.WhiteText();
                        newSong.YouTubeLink = youTubeLink;

                        CurrentlySelectedPlaylist.AddSong(newSong);
                        numOfSongsAdded++;
                    }
                    Console.Write("Would you like to add another song (y/n): ");
                    moreSongs = Display.PromptForYesNo();

                } while (moreSongs == "y");
                Display.SuccessMessage($"{numOfSongsAdded} song(s) added");
            }
            else
            {
                Display.ErrorMessage("There is no playlist to add songs to");
            }
            
        }
        public void RemoveSongs()
        {
            if(CurrentlySelectedPlaylist != null)
            {
                int numOfSongsRemoved = 0;
                string moreSongs = "";
                if (CurrentlySelectedPlaylist.Songs.Count > 0)
                {
                    do
                    {
                        Song? selectedSong = FindSongInCurrentPlaylist();
                        if (selectedSong != null)
                        {
                            CurrentlySelectedPlaylist.Songs.Remove(selectedSong);
                            Display.SuccessMessage($"{selectedSong.Name} by {selectedSong.Artist} has been removed from '{CurrentlySelectedPlaylist.Name}'");
                            numOfSongsRemoved++;
                        }
                        else
                        {
                            break;
                        }
                        if(CurrentlySelectedPlaylist.Songs.Count > 0)
                        {
                            Console.Write("Would you like to remove another song (y/n): ");
                            moreSongs = Display.PromptForYesNo();
                        }
                        else
                        {
                            Console.WriteLine("There are no more songs to remove");
                        }
                    } while (moreSongs == "y" && CurrentlySelectedPlaylist.Songs.Count > 0);
                    Display.SuccessMessage($"{numOfSongsRemoved} song(s) removed");
                }
                else
                {
                    Display.ErrorMessage("There are no songs to remove");
                }
            }
            else
            {
                Display.ErrorMessage("No playlist to remove songs from");
            }
            
        }
        public void UpdateYouTubeLink()
        {
            if(CurrentlySelectedPlaylist != null)
            {
                if(CurrentlySelectedPlaylist.Songs.Count > 0)
                {
                    string moreSongs = "";
                    do
                    {
                        Song? selectedSong = FindSongInCurrentPlaylist();
                        if (selectedSong != null)
                        {
                            if (selectedSong.YouTubeLink == "")
                            {
                                Console.Write($"Please enter the new YouTube link for {selectedSong.Name} by {selectedSong.Artist} it's currently empty: ");
                            }
                            else
                            {
                                Console.Write($"Please enter the new YouTube link for {selectedSong.Name} by {selectedSong.Artist} current link is '{selectedSong.YouTubeLink}': ");
                            }
                            Display.GreenText();
                            string ytLink = Console.ReadLine();
                            Console.WriteLine();
                            Display.WhiteText();
                            selectedSong.YouTubeLink = ytLink;
                        }
                        Console.Write("Would you like to edit another song (y/n): ");
                        moreSongs = Display.PromptForYesNo();
                    } while (moreSongs == "y");
                }
                else
                {
                    Display.ErrorMessage("There are no songs to edit the link for");
                }
            }
            else
            {
                Display.ErrorMessage("There is no playlist to edit songs in");
            }     
        }
        public void PrintSongs()
        {
            if(CurrentlySelectedPlaylist != null)
            {
                Console.Clear();
                if (CurrentlySelectedPlaylist.Songs.Count < 1)
                {
                    Console.WriteLine($"There are no songs in {CurrentlySelectedPlaylist.Name}");
                }
                else
                {
                    int whiteSpacesToAddForCount = (CurrentlySelectedPlaylist.Songs.Count.ToString().Length + 2);
                    int whiteSpacesToAdd = (48 - whiteSpacesToAddForCount);
                    string spaces = string.Concat(Enumerable.Repeat(" ", whiteSpacesToAdd));
                    string numSpaces = string.Concat(Enumerable.Repeat(" ", whiteSpacesToAddForCount));
                    Display.YellowText();
                    Console.WriteLine($"{numSpaces}Song{spaces}Artist");
                    Console.WriteLine();
                    Display.WhiteText();
                    List<Song> songs = CurrentlySelectedPlaylist.Songs;
                    for(int i = 0; i < songs.Count; i++)
                    {
                        whiteSpacesToAddForCount = (songs.Count.ToString().Length - (i + 1).ToString().Length);
                        whiteSpacesToAdd = (50 - songs[i].Name.Length - (i + 1).ToString().Length - whiteSpacesToAddForCount);
                        spaces = string.Concat(Enumerable.Repeat(" ", whiteSpacesToAdd));
                        numSpaces = string.Concat(Enumerable.Repeat(" ", whiteSpacesToAddForCount));
                        Console.WriteLine($"{i + 1}. {numSpaces}{songs[i].Name}{spaces}{songs[i].Artist}");
                        Console.WriteLine();
                    }
                    Console.WriteLine();
                }
            }
            else
            {
                Display.ErrorMessage("No playlist to retrieve songs from");
            }
            
        }
        public void ConvertPlaylistToAHK()
        {
            if(CurrentlySelectedPlaylist != null)
            {
                int songCount = CurrentlySelectedPlaylist.Songs.Count;
                int numOfFiles = (songCount - 1)/ 50;
                int songsLoopedThrough = 0;
                for (int i = 0; i <= numOfFiles; i++)
                {
                    string fileName = AHKFilePath.Insert(AHKFilePath.Length - 4, $"{i}");
                    List<string> linesToAdd = new List<string>();
                    linesToAdd.Add("1::");
                    linesToAdd.Add("");
                    linesToAdd.Add("{");
                    for (int j = songsLoopedThrough; j < songsLoopedThrough + SongsPerFile && j < songCount; j++)
                    {
                        Song currentSong = CurrentlySelectedPlaylist.Songs[j];
                        if (currentSong.YouTubeLink != null && currentSong.YouTubeLink != "")
                        {
                            linesToAdd.Add($"\tSendRaw, !play {currentSong.YouTubeLink}");
                        }
                        else
                        {
                            linesToAdd.Add($"\tSendRaw, !play {currentSong.Name} by {currentSong.Artist}");
                        }
                        linesToAdd.Add("\tSend, {enter}");
                    }
                    linesToAdd.Add("}");
                    File.WriteAllLines(fileName, linesToAdd);
                    songsLoopedThrough += SongsPerFile;
                }
                Display.SuccessMessage("Playlist converted to AHK script(s)");
            }  
        }
        public Song? FindSongInCurrentPlaylist()
        {
            string songName = "";
            string songArtist = "";
            bool songFound = false;
            Console.Clear();
            PrintSongs();
            Console.Write("Please enter the name of the song: ");
            while (songName == "")
            {
                Display.GreenText();
                songName = Console.ReadLine();
                Console.WriteLine();
                Display.WhiteText();
                if (songName == "")
                {
                    Display.ErrorMessage("You must enter a song name!");
                }
            }
            Console.Write("Please enter the songs artist: ");
            while (songArtist == "")
            {
                Display.GreenText();
                songArtist = Console.ReadLine();
                Console.WriteLine();
                Display.WhiteText();
                if (songArtist == "")
                {
                    Display.ErrorMessage("You must enter an artists name!");
                }
            }
            if(CurrentlySelectedPlaylist != null)
            {
                foreach (Song song in CurrentlySelectedPlaylist.Songs)
                {
                    if (song.Name == songName && song.Artist == songArtist)
                    {
                        return song;
                    }
                }
            } 
            if (!songFound)
            {
                Display.ErrorMessage("Song not found please check spelling or that song exists in current playlist");
            }
            return null;
        }
    }
}
