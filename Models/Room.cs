using System.Collections.Generic;
using DungeonExplorer.Models;

namespace DungeonExplorer.Models
{
    public class Room
    {
        public string Description { get; }
        public List<Monster> Monsters { get; } = new List<Monster>();
        public List<Item> Items { get; } = new List<Item>();

        public Room(string description) => Description = description;
    }
}
