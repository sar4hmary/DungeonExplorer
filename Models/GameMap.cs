using System.Collections.Generic;

namespace DungeonExplorer.Models
{
    public class GameMap
    {
        public List<Room> Rooms { get; } = new List<Room>();
        public int CurrentRoomIndex { get; private set; }

        public Room CurrentRoom => Rooms[CurrentRoomIndex];

        public void MoveToRoom(int index)
        {
            if (index < 0 || index >= Rooms.Count)
                throw new ArgumentOutOfRangeException("Invalid room index!");
            CurrentRoomIndex = index;
        }
    }
}
