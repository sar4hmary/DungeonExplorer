namespace DungeonExplorer.Interfaces
{
    /// <summary>
    /// Allows items to be collected by Player
    /// </summary>
    public interface ICollectible
    {
        void Collect(Player player);
    }
}
