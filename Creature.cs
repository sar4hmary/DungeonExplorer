namespace DungeonExplorer.Creatures
{
    ///<summary>
    ///Abstract class that represents a creature in the game
    ///</summary>
    public abstract class Creature
    {
        public string Name { get; set; }  //Name of the creature
        public int Health { get; set; }  //Health of the creature

        ///<summary>
        ///Constructor for the Creature class, initialising the creature with a name and health
        ///</summary>
        protected Creature(string name, int health)
        {
            Name = name;
            Health = health;
        }

        ///<summary>
        ///Abstract method to attack the player, the attack logic will be defined in the subclasses
        ///</summary>
        public abstract void Attack(Player player);
    }
}
