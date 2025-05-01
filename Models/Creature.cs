using System;
using System.Diagnostics;
using DungeonExplorer.Interfaces;

namespace DungeonExplorer.Models
{
    /// <summary>
    /// Abstract base class representing all living entities in the game world
    /// Implements the IDamageable interface to enable damage-taking functionality
    /// Serves as the foundation for both player characters and monsters
    /// </summary>
    public abstract class Creature : IDamageable
    {
        /// <summary>
        /// Gets the name of the creature. Protected set allows derived classes to set the name
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// Gets the current health points of the creature. Protected set allows derived classes to modify health
        /// </summary>
        public int Health { get; protected set; }

        /// <summary>
        /// Gets the base damage the creature deals in combat. Protected set allows derived classes to modify damage
        /// </summary>
        public int Damage { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the Creature class with specified attributes
        /// </summary>
        protected Creature(string name, int health, int damage)
        {
            // Validate constructor parameters
            Debug.Assert(!string.IsNullOrEmpty(name), "Creature name cannot be null or empty");
            Debug.Assert(health > 0, "Initial health must be positive");
            Debug.Assert(damage >= 0, "Damage cannot be negative");

            Name = name;
            Health = health;
            Damage = damage;
        }

        /// <summary>
        /// Virtual method representing a basic attack that can be overridden by derived classes
        /// Performs standard attack sequence: displays attack message and applies damage to target
        /// </summary>
        public virtual void Attack(Creature target)
        {
            // Pre-condition validation using Debug.Assert
            Debug.Assert(target != null, "Attack target cannot be null");
            Debug.Assert(this.IsAlive(), "Dead creatures cannot attack");
            Debug.Assert(target.IsAlive(), "Cannot attack dead creatures");

            // Standard attack sequence
            Console.WriteLine($"{Name} attacks {target.Name} for {Damage} damage!");
            target.TakeDamage(Damage);
        }

        /// <summary>
        /// Explicit implementation of IDamageable.TakeDamage
        /// Reduces the creature's health by the specified damage amount, clamping at 0
        /// </summary>
        void IDamageable.TakeDamage(int damage)
        {
            // Validate damage amount
            Debug.Assert(damage >= 0, "Damage amount cannot be negative");

            // Calculate new health, ensuring it doesn't go below 0
            Health = Math.Max(0, Health - damage);
            Console.WriteLine($"{Name} takes {damage} damage!");
        }

        /// <summary>
        /// Determines if the creature is currently alive (health > 0)
        /// </summary>
        /// <returns>True if health is greater than 0, false otherwise</returns>
        public bool IsAlive() => Health > 0;
    }
}
