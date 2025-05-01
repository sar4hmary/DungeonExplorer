using System;
using DungeonExplorer.Interfaces;

namespace DungeonExplorer.Models
{
    ///<summary>
    ///Player class with inventory management
    ///</summary>
    public class Player : Creature
    {
        public Inventory Inventory { get; } = new Inventory();

        public Player() : base("Hero", 100, 10) { }

        ///<summary>
        ///Uses item by index (Static Polymorphism - Method Overloading)
        ///</summary>
        public void UseItem(int index)
        {
            if (index < 0 || index >= Inventory.Items.Count)
                throw new IndexOutOfRangeException("Invalid item index!");

            Inventory.Items[index].Use(this);
            Inventory.RemoveItem(index);
        }

        ///<summary>
        ///Uses item by name 
        ///</summary>
        public void UseItem(string itemName)
        {
            var item = Inventory.Items.FirstOrDefault(i => i.Name.Equals(itemName));
            if (item != null) item.Use(this);
        }
    }
}
