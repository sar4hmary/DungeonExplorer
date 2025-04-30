namespace DungeonExplorer.Creatures
{
    public class Goblin : Creature
    {
        ///<summary>
        ///Constructor for the Goblin class, inheriting from Creature
        ///</summary>
        public Goblin(string name, int health) : base(name, health)
        {
        }

        ///<summary>
        ///The Goblin attacks the player, reducing their health by 5
        ///</summary>
        public override void Attack(Player player)
        {
            player.Health -= 5;
            Console.WriteLine($"{Name} attacks {player.Name}, dealing 5 damage!");
        }
    }
}
