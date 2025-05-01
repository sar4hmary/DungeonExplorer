using System;
using System.Collections.Generic;

namespace DungeonExplorer
{
    public class GameMap
    {
        private Dictionary<string, Room> rooms;  //Dictionary to store rooms by direction
        private Room currentRoom;  //The room the player is currently in

        ///<summary>
        ///Initialises the rooms and places creatures in each room
        ///</summary>
        public void InitialiseRooms()
        {
            //Create a dictionary of rooms
            rooms = new Dictionary<string, Room>
            {
                { "Start", new Room("a dimly lit dungeon room.") },
                { "North", new Room("a cavern with dripping water.") },
                { "East", new Room("a chamber with ancient carvings.") },
                { "South", new Room("a crypt filled with bones.") },
                { "West", new Room("a treasure room with gold.") }
            };

            //Assign creatures to each room
            rooms["North"].CreatureInRoom = new Goblin("Goblin", 20);
            rooms["East"].CreatureInRoom = new Witch("Witch", 30);
            rooms["South"].CreatureInRoom = new Giant("Giant", 50);
            rooms["West"].CreatureInRoom = new Weapon("Axe", "A powerful axe", 12);

            //Set the starting room
            currentRoom = rooms["Start"];
        }

        ///<summary>
        ///Moves the player to a new room based on the direction provided
        ///</summary>
        public Room MovePlayer(string direction)
        {
            //Check if the room exists in the dictionary and move the player
            if (rooms.TryGetValue(direction, out Room newRoom))
            {
                currentRoom = newRoom;
                return newRoom;
            }
            return null;
        }
    }
}
