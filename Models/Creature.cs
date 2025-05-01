using System;
using DungeonExplorer.Interfaces;

namespace DungeonExplorer.Models
{
    ///<summary>
    ///Base class for all living entities
    ///Implements IDamageable interface
    ///</summary>
    public abstract class Creature : IDamageable
    {
        public string Name { get; protected set; }
        public int Health { get; protected set; }
        public int Damage { get; protected set; }

        protected Creature(string name, int health, int damage)
        {
            Name = name;
            Health = health;
            Damage = damage;
        }

        ///<summary>
        ///Virtual method, can be overridden by derived classes
        ///</summary>
        public virtual void Attack(Creature target)
        {
            Console.WriteLine($"{Name} attacks {target.Name} for {Damage} damage!");
            target.TakeDamage(Damage);
        }

        ///<summary>
        ///Explicit interface implementation
        ///</summary>
        void IDamageable.TakeDamage(int damage)
        {
            Health = Math.Max(0, Health - damage);
            Console.WriteLine($"{Name} takes {damage} damage!");
        }

        public bool IsAlive() => Health > 0;

        public virtual void Attack(Creature target)
        {
            Debug.Assert(target != null, "Attack target cannot be null");
            Debug.Assert(this.IsAlive(), "Dead creatures cannot attack");
            Debug.Assert(target.IsAlive(), "Cannot attack dead creatures");
            
            Console.WriteLine($"{Name} attacks {target.Name} for {Damage} damage!");
            target.TakeDamage(Damage);
        }
    }
}
