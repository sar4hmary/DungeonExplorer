using System;
using System.Collections.Generic;

namespace DungeonExplorer
{
    public class Inventory
    {
        private List<Item> _items;  //List of items in the inventory

        ///<summary>
        ///Constructor for the Inventory class, initialising an empty list of item
        ///</summary>
        public Inventory()
        {
            _items = new List<Item>();
        }

        ///<summary>
        ///Adds an item to the inventory
        ///</summary>
        public void AddItem(Item item)
        {
            _items.Add(item);
            Console.WriteLine($"Item added: {item.Name}");
        }

        ///<summary>
        ///Removes an item from the inventory by its name
        ///</summary>
        public bool RemoveItem(string itemName)
        {
            var item = _items.Find(i => i.Name == itemName);
            if (item != null)
            {
                _items.Remove(item);
                Console.WriteLine($"Item removed: {itemName}");
                return true;
            }
            Console.WriteLine($"Item {itemName} not found in inventory.");
            return false;
        }

        ///<summary>
        ///Retrieves an item from the inventory by its name
        ///</summary>
        public Item GetItem(string itemName)
        {
            return _items.Find(i => i.Name == itemName);
        }

        ///<summary>
        ///Displays the inventory contents
        ///</summary>
        public void DisplayInventory()
        {
            if (_items.Count == 0)
            {
                Console.WriteLine("Your inventory is empty.");
            }
            else
            {
                Console.WriteLine("Inventory: ");
                foreach (var item in _items)
                {
                    Console.WriteLine($"- {item.Name}: {item.Description}");
                }
            }
        }
    }
}
