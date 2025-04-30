namespace DungeonExplorer
{
    public class Potion : Item
    {
        public int HealingAmount { get; set; }  //Amount of health the potion heals

        ///<summary>
        ///Constructor for the Potion class, inheriting from Item
        ///</summary>
        public Potion(string name, string description, int healingAmount) 
            : base(name, description)
        {
            HealingAmount = healingAmount;
        }

        ///<summary>
        ///Use the potion on a player, healing them for the specified amount
        ///</summary>
        public override void Use(Creature target)
        {
            if (target is Player player)
            {
                player.Health += HealingAmount;  // Heal the player
                Console.WriteLine($"{Name} heals {player.Name} for {HealingAmount} health!");
            }
            else
            {
                Console.WriteLine($"{Name} has no effect on {target.Name}.");
            }
        }
    }
}
