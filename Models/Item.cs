using DungeonExplorer.Interfaces;

namespace DungeonExplorer.Models
{
    /// <summary>
    /// Abstract base class for all in game items that can be collected and used
    /// Implements ICollectible interface to enable standardized collection behaviour
    /// </summary>
    public abstract class Item : ICollectible
    {
        /// <summary>
        /// Gets the display name of the item
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// Abstract method defining the item's effect when used
        /// </summary>
        public abstract void Use(Player player);
        
        /// <summary>
        /// Implementation of ICollectible.Collect, handles adding the item to player's inventory
        /// </summary>
        public void Collect(Player player)
        {
            player.Inventory.AddItem(this);
            Console.WriteLine($"{player.Name} collected {Name}!");
        }
    }

    /// <summary>
    /// Represents a weapon item that increases player's damage when equipped
    /// </summary>
    public class Weapon : Item
    {
        /// <summary>
        /// Gets the damage boost value this weapon provides when equipped
        /// </summary>
        public int DamageBoost { get; }

        /// <summary>
        /// Initialises a new weapon with specified properties
        /// </summary>
        public Weapon(string name, int boost)
        {
            Name = name;
            DamageBoost = boost;
        }

        /// <summary>
        /// Equips the weapon to the player, increasing their damage stat
        /// </summary>
        public override void Use(Player player)
        {
            player.Damage += DamageBoost;
            Console.WriteLine($"{player.Name} equipped {Name} (+{DamageBoost} damage)!");
        }
    }

    /// <summary>
    /// Represents a consumable health-restoring item
    /// </summary>
    public class Potion : Item
    {
        /// <summary>
        /// Gets the amount of health this potion restores
        /// </summary>
        public int HealAmount { get; }

        /// <summary>
        /// Initialises a new health potion with specified properties
        /// </summary>
        public Potion(string name, int heal)
        {
            Name = name;
            HealAmount = heal;
        }

        /// <summary>
        /// Consumes the potion, restoring player's health without exceeding maximum (100 HP)
        /// </summary>
        public override void Use(Player player)
        {
            player.Health = Math.Min(100, player.Health + HealAmount);
            Console.WriteLine($"{player.Name} healed {HealAmount} HP!");
        }
    }

    /// <summary>
    /// Represents a key item used to unlock special doors or containers
    /// </summary>
    public class Key : Item
    {
        /// <summary>
        /// Gets the description of what this key unlocks
        /// </summary>
        public string DoorDescription { get; }

        /// <summary>
        /// Initialises a new key item with specified properties
        /// </summary>
        public Key(string name, string doorDescription)
        {
            Name = name;
            DoorDescription = doorDescription;
        }

        /// <summary>
        /// Displays key information when used
        /// </summary>
        public override void Use(Player player)
        {
            Console.WriteLine($"This key unlocks {DoorDescription}");
        }
    }
}
