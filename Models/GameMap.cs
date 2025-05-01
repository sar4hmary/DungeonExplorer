using System;
using System.Collections.Generic;

namespace DungeonExplorer.Models
{
    ///<summary>
    ///Manages the dungeon layout and player navigation between rooms
    ///Tracks the current room and validates room transitions
    ///</summary>
    public class GameMap
    {
        public List<Room> Rooms { get; private set; } = new List<Room>(); //Stores all rooms in the dungeon, uses a List for ordered access

        public int CurrentRoomIndex { get; private set; } //Tracks the index of the player's current room in the Rooms list

        public Room CurrentRoom => Rooms[CurrentRoomIndex]; //Property that returns the current Room object, uses lambda syntax for concise property getter

        /// <summary>
        /// Moves player to a new room by index.
        /// </summary>
        public void MoveToRoom(int index)
        {
            if (index < 0 || index >= Rooms.Count) //Validates room index
            {
                throw new ArgumentOutOfRangeException(
                    paramName: nameof(index),
                    message: $"Room index {index} is out of range. Valid range: 0-{Rooms.Count - 1}");
            }
            
            CurrentRoomIndex = index;
            
        }
        public void MoveToRoom(int index)
        {
            Debug.Assert(index >= 0 && index < Rooms.Count, "Room index out of range");
            if (Rooms[index].IsLocked)
            {
                throw new InvalidOperationException("Room is locked");
            }
            CurrentRoomIndex = index;
        }
    }
}
