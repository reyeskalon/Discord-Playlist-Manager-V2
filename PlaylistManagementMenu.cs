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
                    Display.YellowText();
                    menuKey = PromptForMenuKey();
                    Display.WhiteText();
                }
                if(menuKey == 1)
                {
                    PlaylistManager.CurrentlySelectedPlaylist = PlaylistManager.CreateNewPlaylist();
                    menuKey = ResetMenuKey();
                }
                if (menuKey == 2)
                {
                    PlaylistManager.RemovePlaylist();
                    menuKey = ResetMenuKey();
                }
                if (menuKey == 3)
                {
                    PlaylistManager.RenamePlaylist();
                    menuKey = ResetMenuKey();
                }
            } while (menuKey != 0);
        }
    }
}
