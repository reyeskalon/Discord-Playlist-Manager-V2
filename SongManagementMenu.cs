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
            MenuOptions = new List<string> {"View Songs", "Add Songs", "Remove Songs", "Update YouTube Link For A Given Song"};

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
                    PlaylistManager.PrintSongs();
                    menuKey = ResetMenuKey();
                }
                if(menuKey == 2)
                {
                    PlaylistManager.AddSongs();
                    menuKey = ResetMenuKey();
                }
                if(menuKey == 3)
                {
                    PlaylistManager.RemoveSongs();
                    menuKey = ResetMenuKey();
                }
                if (menuKey == 4)
                {
                    PlaylistManager.UpdateYouTubeLink();
                    menuKey= ResetMenuKey();
                }
            } while (menuKey != 0);
        }
    }
}
