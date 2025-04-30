namespace DungeonExplorer
{
    ///<summary>
    ///Abstract base class for all items in the game
    ///</summary>
    public abstract class Item
    {
        public string Name { get; set; }  //Name of the item
        public string Description { get; set; }  //Description of the item

        ///<summary>
        ///Constructor for the Item class, initialising the name and description of the item
        ///</summary>
        protected Item(string name, string description)
        {
            Name = name;
            Description = description;
        }

        ///<summary>
        ///Use the item on a creature
        ///</summary>
        public virtual void Use(Creature target)
        {
            Console.WriteLine($"{Name} is used. Nothing special happens.");
        }
    }
}
