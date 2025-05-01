using System.Collections.Generic;
using System.Linq;

namespace DungeonExplorer.Models
{
    ///<summary>
    ///Manages player inventory with LINQ queries
    ///</summary>
    public class Inventory
    {
        public List<Item> Items { get; } = new List<Item>();

        //LINQ: Filter weapons
        public List<Weapon> GetWeapons() => Items.OfType<Weapon>().ToList();

        //Lambda: Find strongest weapon
        public Weapon GetStrongestWeapon() => 
            Items.OfType<Weapon>()
                 .OrderByDescending(w => w.DamageBoost)
                 .FirstOrDefault();

        public void AddItem(Item item) => Items.Add(item);
        public void RemoveItem(int index) => Items.RemoveAt(index);
    }
}
