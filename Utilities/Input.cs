using System;

namespace DungeonExplorer.Utilities
{
    ///<summary>
    ///Handles all user input validation
    ///</summary>
    public static class Input
    {
        public static int ReadInt(string prompt, int min, int max)
        {
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out int result) 
                {
                    if (result >= min && result <= max)
                        return result;
                }
                Console.WriteLine($"Invalid input! Enter a number between {min}-{max}.");
            }
        }

        public static bool ReadYesNo()
        {
            while (true)
            {
                string input = Console.ReadLine()?.Trim().ToUpper();
                if (input == "Y") return true;
                if (input == "N") return false;
                Console.WriteLine("Invalid input! Enter Y/N.");
            }
        }
    }
}
