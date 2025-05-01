using DungeonExplorer.Interfaces;

namespace DungeonExplorer.Models
{
    ///<summary>
    ///Abstract base class for all in-game items
    ///Implements ICollectible interface to enable item collection behavior
    ///</summary>
    public abstract class Item : ICollectible
    {
        public string Name { get; protected set; }

        public abstract void Use(Player player); //Abstract method defining how the item affects the player when used

        public void Collect(Player player) //Implementation of ICollectible interface, handles what happens when a player collects this item.
        {
            player.Inventory.AddItem(this);
            Console.WriteLine($"{player.Name} collected {Name}!");
        }
    }

    public class Weapon : Item
    {
        public int DamageBoost { get; } //The amount of damage boost this weapon provides

        public Weapon(string name, int boost) //Constructs a new weapon with specified properties
        {
            Name = name;
            DamageBoost = boost;
        }

        public override void Use(Player player) //Applies the weapon's damage boost when equipped by a player
        {
            player.Damage += DamageBoost;
            Console.WriteLine($"{player.Name} equipped {Name} (+{DamageBoost} damage)!");
        }
    }

    public class Potion : Item
    {
        public int HealAmount { get; } //The amount of health this potion restores

        public Potion(string name, int heal) // Constructs a new health potion with specified properties
        {
            Name = name;
            HealAmount = heal;
        }

        public override void Use(Player player) //Restores player's health when consumed, capping at maximum 100 HP
        {
            player.Health = Math.Min(100, player.Health + HealAmount);
            Console.WriteLine($"{player.Name} healed {HealAmount} HP!");
        }
    }
}
