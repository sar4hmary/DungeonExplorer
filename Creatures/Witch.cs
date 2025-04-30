namespace DungeonExplorer.Creatures
{
    public class Witch : Creature
    {
        ///<summary>
        /// Constructor for the Witch class, inheriting from Creature
        ///</summary>
        public Witch(string name, int health) : base(name, health)
        {
        }

        ///<summary>
        ///The Witch casts a spell on the player, reducing their health by 7
        ///</summary>
        public override void Attack(Player player)
        {
            player.Health -= 7;
            Console.WriteLine($"{Name} casts a spell on {player.Name}, dealing 7 damage!");
        }
    }
}
