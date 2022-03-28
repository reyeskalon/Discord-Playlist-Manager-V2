using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordPlaylistManagerV2
{
    public class Menu
    {
        public List<string> MenuOptions { get; set; } = new List<string>();
        public PlaylistManager PlaylistManager { get; set; } = new PlaylistManager();
        public void DisplayMenu()
        {
            Console.Clear();
            Display.YellowText();
            Console.Write("Hello and Welcome to Keenan's Playlist Manager");
            Display.WhiteText();
            Console.Write("\t\t\t\t Selected Playlist: ");
            if(PlaylistManager.CurrentlySelectedPlaylist != null)
            {
                Display.SuccessMessage($"{PlaylistManager.CurrentlySelectedPlaylist.Name}");
            }
            else
            {
                Display.ErrorMessage("No Playlists");
            }
            for (int i = 0; i < MenuOptions.Count; i++)
            {
                Console.WriteLine($"{i + 1}) {MenuOptions[i]}");
            }
            Console.WriteLine("0) Exit");
            Console.WriteLine();
        }
        public bool ValidateMenuSelection(int menuKey)
        {
            if(menuKey >= 0 && menuKey <= MenuOptions.Count)
            {
                return true;
            }
            return false;
        }
        public int PromptForMenuKey()
        {
            int menuKey = -1;
            try
            {
                menuKey = int.Parse(Console.ReadLine());
                Console.WriteLine();
            }
            catch
            {
                Display.ErrorMessage("Please enter a valid number");
            }
            return menuKey;
        }
        public int ResetMenuKey()
        {
            Console.ReadLine();
            return -1;
        }
    }
}

