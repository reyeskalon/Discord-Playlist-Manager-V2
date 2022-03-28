using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordPlaylistManagerV2
{
    class Display
    {
        public static void ErrorMessage(string text)
        {
            RedText();
            Console.WriteLine(text);
            Console.WriteLine();
            WhiteText();
        }
        public static void SuccessMessage(string text)
        {
            GreenText();
            Console.WriteLine(text);
            Console.WriteLine();
            WhiteText();
        }
        public static string PromptForYesNo()
        {
            GreenText();
            string yesOrNo = Console.ReadLine().ToLower();
            Console.WriteLine();
            WhiteText();
            while (yesOrNo != "y" && yesOrNo != "n")
            {
                RedText();
                Console.Write("Please enter either \"y\" or \"n\": ");
                GreenText();
                yesOrNo = Console.ReadLine().ToLower();
            }
            WhiteText();
            Console.WriteLine();
            return yesOrNo;
        }
        public static void YellowText()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
        }

        public static void WhiteText()
        {
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void GreenText()
        {
            Console.ForegroundColor = ConsoleColor.Green;
        }

        public static void RedText()
        {
            Console.ForegroundColor = ConsoleColor.Red;
        }
    }
}
