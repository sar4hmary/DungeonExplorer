using System;

namespace DungeonExplorer.Interfaces
{
    ///<summary>
    ///Allows objects to receive damage (Player and Monsters)
    ///</summary>
    public interface IDamageable
    {
        void TakeDamage(int damage);
    }
}
