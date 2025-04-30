using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace DungeonExplorer
{
    ///<summary>
    ///Game class handles the main gameplay logic
    ///Manages the player, room, creature, and game loop
    ///</summary>
    public class Game
    {
        private Player _player;  //The player in the game
        private List<Room> _rooms;  //List of rooms in the dungeon

        ///<summary>
        ///Constructor for the Game class, it creates a new player and a list of rooms in the game
        ///</summary>
        public Game()
        {
            _player = new Player("Hero", 100);  //Initialises the player with a name and health
            _rooms = new List<Room>
            {
                new Room("A dark cave.", new Goblin("Goblin", 20)),  //Room with a Goblin
                new Room("A forest clearing.", new Giant("Giant", 50)),  //Room with a Giant
                new Room("A witch's hut.", new Witch("Witch", 30))  //Room with a Witch
            };
        }

        ///<summary>
        ///Starts the game, looping through each room and displaying information about it
        ///</summary>
        public void Start()
        {
            Console.WriteLine("Welcome to Dungeon Explorer!");
            
            //Go through all rooms and interact with the creature inside each room
            foreach (var room in _rooms)
            {
                Console.WriteLine(room.GetDescription());  //Display room description
                room.CreatureInRoom.Attack(_player);  //Attack the player by the creature in the room
                _player.ShowStatus();  //Show the player's status after each attack
            }
        }
    }
}
