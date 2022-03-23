using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordPlaylistManagerV2
{
    public class MainMenu : Menu
    {
        public PlaylistManagementMenu PlaylistManagementMenu { get; set; }
        public SongManagementMenu SongManagementMenu { get; set; }
        public MainMenu(PlaylistManager playlistManager, PlaylistManagementMenu playlistManagementMenu, SongManagementMenu songManagementMenu)
        {
            PlaylistManager = playlistManager;
            PlaylistManagementMenu = playlistManagementMenu;
            SongManagementMenu = songManagementMenu;
            MenuOptions = new List<string> { "Shuffle Selected Playlist", "Select Different Playlist", "Manage Songs In Playlist", "Manage Playlists" };
        }
        public void RunMenu()
        {
            int menuKey = -1;
            do
            {
                DisplayMenu();
                while (!ValidateMenuSelection(menuKey))
                {
                    menuKey = PromptForMenuKey();
                }
                if (menuKey == 1)
                {
                    PlaylistManager.CurrentlySelectedPlaylist.ShufflePlaylist();
                    menuKey = -1;
                }
                if (menuKey == 2)
                {
                    PlaylistManager.CurrentlySelectedPlaylist = PlaylistManager.PlaylistSelctor();
                    menuKey = -1;
                }
                if (menuKey == 3)
                {
                    SongManagementMenu.Run();
                    menuKey = -1;
                }
                if (menuKey == 4)
                {
                    PlaylistManagementMenu.Run();
                    menuKey = -1;
                }
                if (menuKey == 0)
                {
                    PlaylistManager.SavePlaylists();
                }
            } while (menuKey != 0);
        }
    }
}
