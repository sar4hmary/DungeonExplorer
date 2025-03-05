using System;
using System.Media;

namespace DungeonExplorer
{
    internal class Game
    {
        private Player player;
        private Room currentRoom;

        public Game()
        {
            // Initialising the game with one room and one player
            player = new Player("Hero", 100);
            currentRoom = new Room("A mysterious, dark, eerie room with stone walls.");
        }
        
        public void Start()
        {
            // Changed the playing logic into true and populated the while loop
            bool playing = true;
            while (playing)
            {
                Console.WriteLine(currentRoom.GetDescription());
                Console.WriteLine($"Health: {player.Health}");
                Console.WriteLine)$"Inventory: {player.InventoryContents()}");

                Console.WriteLine("Choose action: ");
                Console.WriteLine("1. Pick up the sword");
                Console.WriteLine("2. Exit the Dungeon");
                
            }
        }
    }
}
