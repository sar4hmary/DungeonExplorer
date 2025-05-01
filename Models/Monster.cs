namespace DungeonExplorer.Models
{
    ///<summary>
    ///Base monster class
    ///</summary>
    public class Monster : Creature
    {
        public Monster(string name, int health, int damage) : base(name, health, damage) { }
    }

    ///<summary>
    ///Weak enemy with standard attack
    ///</summary>
    public class Goblin : Monster
    {
        public Goblin() : base("Goblin", 30, 5) { }
    }

    ///<summary>
    ///Strong enemy with overridden attack (Dynamic Polymorphism)
    ///</summary>
    public class Dragon : Monster
    {
        public Dragon() : base("Dragon", 100, 20) { }

        public override void Attack(Creature target)
        {
            Console.WriteLine("The dragon breathes fire!");
            target.TakeDamage(Damage + 10); //Extra damage
        }
    }
}
