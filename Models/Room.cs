using System.Collections.Generic;
using DungeonExplorer.Models;

namespace DungeonExplorer.Models
{
    /// <summary>
    /// Represents a location in the dungeon containing monsters and items
    /// Each room maintains its own state including active monsters and available loot
    /// </summary>
    public class Room
    {
        public string Description { get; } //The descriptive text displayed when players enter the room
        public List<Monster> Monsters { get; } = new List<Monster>();
        public List<Item> Items { get; } = new List<Item>();
        public bool IsLocked { get; set; }
        public string KeyItem { get; set; }
        public bool HasPuzzle { get; set; }
        
        public Room(string description) => Description = description; //Constructs a new room with the specified description
        
    }
}
