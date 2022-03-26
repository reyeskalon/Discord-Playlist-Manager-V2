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
            YellowText();
            Console.Write("Hello and Welcome to Keenan's Playlist Manager");
            WhiteText();
            Console.Write("\t\t\t\t Selected Playlist: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{PlaylistManager.CurrentlySelectedPlaylist.Name}");
            WhiteText();
            Console.WriteLine();
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
                Console.WriteLine();
                RedText();
                Console.WriteLine("Please enter a valid number");
                WhiteText();
                Console.WriteLine();
            }
            return menuKey;
        }
        public string PromptForYesNo()
        {
            string yesOrNo = Console.ReadLine().ToLower();
            Console.WriteLine();
            while (yesOrNo != "y" && yesOrNo != "n")
            {
                {
                    RedText();
                    Console.WriteLine("Please enter either \"y\" or \"n\"");
                    WhiteText();
                    Console.WriteLine();
                }
                yesOrNo = Console.ReadLine().ToLower();
            }
            return yesOrNo;
        }
        /*        public string PromptForString(string promptMsg, string emptyMsg)
                {
                    Console.WriteLine();
                    Console.WriteLine(promptMsg);
                    string returnString = Console.ReadLine();
                    while (returnString == "" || returnString == null)
                    {
                        {
                            Console.WriteLine();
                            RedText();
                            Console.WriteLine(emptyMsg);
                            WhiteText();
                            Console.WriteLine();
                        }
                        returnString = Console.ReadLine().ToLower();
                    }
                    return returnString;
                }*/
        public int ResetMenuKey()
        {
            Console.ReadLine();
            return -1;
        }
        public void YellowText()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
        }

        public void WhiteText()
        {
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void GreenText()
        {
            Console.ForegroundColor = ConsoleColor.Green;
        }

        public void RedText()
        {
            Console.ForegroundColor = ConsoleColor.Red;
        }
    }
}

