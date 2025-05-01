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

        /// <summary>
        /// List of monsters currently present in this room
        /// Uses List<Monster> to enable LINQ operations and easy iteration
        /// </summary>
        public List<Monster> Monsters { get; } = new List<Monster>();

        /// <summary>
        /// List of collectible items available in this room
        /// Uses List<Item> to support polymorphism (Weapons/Potions)
        /// </summary>
        public List<Item> Items { get; } = new List<Item>();

        public Room(string description) => Description = description; //Constructs a new room with the specified description
        
    }
}
