using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordPlaylistManagerV2
{
    public class SongManagementMenu : Menu
    {
        public SongManagementMenu(PlaylistManager playlistManager)
        {
            PlaylistManager = playlistManager;
            MenuOptions = new List<string> {"View Songs", "Add Song", "Remove Song", "Update YouTube Link For A Given Song"};

        }
        public void Run()
        {
            int menuKey = -1;
            do
            {
                DisplayMenu();
                while (!ValidateMenuSelection(menuKey))
                {
                    menuKey = PromptForMenuKey();
                }
                if(menuKey == 1)
                {
                    Playlist playlist = PlaylistManager.CurrentlySelectedPlaylist;
                    if(playlist.Songs.Count < 1)
                    {
                        Console.WriteLine($"There are no songs in {playlist.Name}");
                    }
                    else
                    {
                        Console.WriteLine("Song \t\t Artist");
                        foreach (Song song in playlist.Songs)
                        {
                            Console.WriteLine($"{song.Name} {song.Artist}");
                        }
                        Console.WriteLine();
                        Console.ReadLine();
                    }
                    menuKey = ResetMenuKey();
                }
                if(menuKey == 2)
                {
                    string answer = "y";
                    while(answer == "y")
                    {
                        PlaylistManager.AddSong();
                        Console.WriteLine("Would you like to add another song?(y/n)");
                        Console.WriteLine();
                        answer = PromptForYesNo();
                    }
                    menuKey = -1;
                }
                if(menuKey == 3)
                {
                    PlaylistManager.RemoveSongs();
                    menuKey = -1;
                }
                if (menuKey == 4)
                {
                    PlaylistManager.UpdateYouTubeLink();
                    menuKey= -1;
                }
            } while (menuKey != 0);
        }
    }
}
