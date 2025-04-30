namespace DungeonExplorer
{
    public class Weapon : Item
    {
        public int Damage { get; set; }  //Damage dealt by the weapon

        ///<summary>
        ///Constructor for the Weapon class, inheriting from Item
        ///</summary>
        public Weapon(string name, string description, int damage)
            : base(name, description)
        {
            Damage = damage;
        }

        ///<summary>
        ///Use the weapon to attack a creature
        ///</summary>
        public override void Use(Creature target)
        {
            Console.WriteLine($"{Name} is used to attack {target.Name}!");
        }
    }
}
