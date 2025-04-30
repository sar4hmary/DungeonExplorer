using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace DungeonExplorer
{
    public class Player
    {
        public string Name { get; set; }  //Name of the player
        public int Health { get; set; }  //Health of the player
        public Inventory Inventory { get; set; }  //Player's inventory

        ///<summary>
        ///Constructor for the Player class, initialising the player's name and health
        ///</summary>
        public Player(string name, int health)
        {
            Name = name;
            Health = health;
            Inventory = new Inventory();  //Create an empty inventory for the player
        }

        ///<summary>
        ///Displays the player's current status, including health and items in their inventory
        ///</summary>
        public void ShowStatus()
        {
            Console.WriteLine($"{Name} has {Health} health.");
            Inventory.DisplayInventory();  //Display the inventory
        }

        ///<summary>
        ///Uses an item from the inventory on a target creature
        ///</summary>
        public void UseItem(string itemName, Creature target)
        {
            var item = Inventory.GetItem(itemName);  //Get the item from the inventory
            if (item != null)
            {
                item.Use(target);  //Use the item on the target creature
                Inventory.RemoveItem(itemName);  //Remove the item from the inventory after use
            }
            else
            {
                Console.WriteLine($"Error: {itemName} not found in inventory.");
            }
        }

        ///<summary>
        ///Attacks a target creature with a weapon from the inventory
        ///</summary>
        public void Attack(Creature target)
        {
            if (Inventory.GetItem("Weapon") is Weapon weapon)
            {
                //If the player has a weapon, attack the target creature
                int damage = weapon.Damage;
                target.Health -= damage;
                Console.WriteLine($"{Name} attacks {target.Name} with {weapon.Name}, dealing {damage} damage!");
            }
            else
            {
                Console.WriteLine($"{Name} has no weapons to attack with.");
            }
        }
    }
}
