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
            //Creates a new instance of the Game class to start the game
            Game game = new Game();
            
            //Starts the game, which will run the game loop
            game.Start();

            //Prompts the user to press any key to exit once the game is finished
            Console.WriteLine("Press any key to exit...");

            //Waits for the user to press a key before the program ends
            Console.ReadKey();
        }
    }
}
