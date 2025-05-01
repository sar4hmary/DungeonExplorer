using DungeonExplorer.Interfaces;

namespace DungeonExplorer.Models
{
    public abstract class Item : ICollectible
    {
        public string Name { get; protected set; }
        public abstract void Use(Player player);
        
        public void Collect(Player player)
        {
            player.Inventory.AddItem(this);
            Console.WriteLine($"{player.Name} collected {Name}!");
        }
    }

    public class Weapon : Item
    {
        public int DamageBoost { get; }

        public Weapon(string name, int boost)
        {
            Name = name;
            DamageBoost = boost;
        }

        public override void Use(Player player)
        {
            player.Damage += DamageBoost;
            Console.WriteLine($"{player.Name} equipped {Name} (+{DamageBoost} damage)!");
        }
    }

    public class Potion : Item
    {
        public int HealAmount { get; }

        public Potion(string name, int heal)
        {
            Name = name;
            HealAmount = heal;
        }

        public override void Use(Player player)
        {
            player.Health = Math.Min(100, player.Health + HealAmount);
            Console.WriteLine($"{player.Name} healed {HealAmount} HP!");
        }
    }

    public class Key : Item
    {
        public string DoorDescription { get; }

        public Key(string name, string doorDescription)
        {
            Name = name;
            DoorDescription = doorDescription;
        }

        public override void Use(Player player)
        {
            Console.WriteLine($"This key unlocks {DoorDescription}");
        }
    }
}
