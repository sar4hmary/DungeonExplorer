using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    ///<summary>
    ///The main entry point for the Dungeon Explorer game
    ///This class is responsible for starting the game and running the game loop
    // </summary>
    internal class Program
    {
        ///<summary>
        ///The entry point of the program. Creates a new game instance, starts the game, and waits for user input before exiting
        ///</summary>
        static void Main(string[] args)
        {
            Console.WriteLine("=== Dungeon Explorer ===");
            Console.WriteLine("1. Play Game\n2. Run Tests");
            int choice = Input.ReadInt("Choose option: ", 1, 2);

            if (choice == 1)
            {
                Game game = new Game();
                game.Start();
            }
            else
            {
                Testing.RunAllTests();
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
