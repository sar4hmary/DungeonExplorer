using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace DungeonExplorer.Models
{
    /// <summary>
    /// Manages a player's collection of items with LINQ-powered query capabilities.
    /// Provides type-safe access to different item categories and maintains inventory integrity.
    /// </summary>
    public class Inventory
    {
        /// <summary>
        /// Gets the complete list of items in the inventory.
        /// The list is initialized empty and can only be modified through class methods.
        /// </summary>
        public List<Item> Items { get; } = new List<Item>();

        /// <summary>
        /// Retrieves all weapons from the inventory using LINQ's OfType filter.
        /// </summary>
        public List<Weapon> GetWeapons() => Items.OfType<Weapon>().ToList();

        /// <summary>
        /// Finds the weapon with the highest damage boost using LINQ sorting.
        /// </summary>
        public Weapon GetStrongestWeapon() => 
            Items.OfType<Weapon>()
                 .OrderByDescending(w => w.DamageBoost)
                 .FirstOrDefault();

        /// <summary>
        /// Adds an item to the inventory with validation.
        /// </summary>
        public void AddItem(Item item)
        {
            Debug.Assert(item != null, "Cannot add null item to inventory");
            Items.Add(item);
        }

        /// <summary>
        /// Removes an item from the inventory by index with bounds checking.
        /// </summary>
        public void RemoveItem(int index)
        {
            // Development-time validation
            Debug.Assert(index >= 0 && index < Items.Count, 
                $"Index {index} out of range (0-{Items.Count - 1})");
            
            // Runtime validation
            if (index < 0 || index >= Items.Count)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(index),
                    $"Index must be between 0 and {Items.Count - 1}");
            }

            Items.RemoveAt(index);
        }
    }
}
