using System;
using System.Diagnostics;

namespace DungeonExplorer
{
    public class Room
    {
        public string Description { get; set; }  //Description of the room
        public Creature CreatureInRoom { get; set; }  //The creature that resides in this room

        ///<summary>
        ///Constructor for the Room class, initialising the room with a description and a creature
        ///</summary>
        public Room(string description, Creature creature)
        {
            Description = description;
            CreatureInRoom = creature;
        }

        ///<summary>
        ///Returns the description of the room
        ///</summary>
        public string GetDescription()
        {
            return Description;
        }
    }
}
