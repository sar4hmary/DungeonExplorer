namespace DungeonExplorer.Creatures
{
    public class Giant : Creature
    {
        ///<summary>
        ///Constructor for the Giant class, inheriting from Creature
        ///</summary>
        public Giant(string name, int health) : base(name, health)
        {
        }

        ///<summary>
        ///The Giant attacks the player, reducing their health by 10
        ///</summary>
        public override void Attack(Player player)
        {
            player.Health -= 10;
            Console.WriteLine($"{Name} smashes {player.Name}, dealing 10 damage!");
        }
    }
}
