using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordPlaylistManagerV2
{
    public class PlaylistManagementMenu : Menu
    {
        public PlaylistManagementMenu(PlaylistManager playlistManager)
        {
            MenuOptions = new List<string> { "Add New Playlist", "Remove Playlist", "Rename Playlist" };
            PlaylistManager = playlistManager;
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
                     PlaylistManager.CurrentlySelectedPlaylist = PlaylistManager.CreateNewPlaylist();
                    menuKey = -1;
                }
                if (menuKey == 2)
                {
                    Console.WriteLine($"Are you sure you want to delete the currently selected playlist: {PlaylistManager.CurrentlySelectedPlaylist.Name} this action cannot be undone (y/n)");
                    string delete = PromptForYesNo();
                    if(delete == "y")
                    {
                        for(int i = 0; i < PlaylistManager.Playlists.Count; i++)
                        {
                            if(PlaylistManager.Playlists[i].Name == PlaylistManager.CurrentlySelectedPlaylist.Name)
                            {
                                PlaylistManager.Playlists.RemoveAt(i);
                            }
                        }
                        Console.WriteLine("Playlist has been deleted");
                    }
                    else
                    {
                        menuKey = -1;
                    }
                }
                if (menuKey == 3)
                {
                    Console.WriteLine($"What would you like to rename {PlaylistManager.CurrentlySelectedPlaylist.Name} to?");
                    string newName = Console.ReadLine();
                    if (newName != null)
                    {
                        Console.WriteLine($"Are you sure you want to rename {PlaylistManager.CurrentlySelectedPlaylist.Name} to {newName}?");
                        string rename = PromptForYesNo();
                        if (rename == "y")
                        {
                            PlaylistManager.CurrentlySelectedPlaylist.Name = newName;
                            menuKey = -1;
                        }
                        else
                        {
                            menuKey = -1;
                        }
                    }
                    else
                    {
                        menuKey = -1;
                    }
                }
            } while (menuKey != 0);
        }
    }
}
