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
            // Debug assertion for development-time validation
            // Only active in debug builds, helps catch programming errors early
            Debug.Assert(index >= 0 && index < Rooms.Count, 
                $"Room index {index} is out of range. Valid range: 0-{Rooms.Count - 1}");
            
            // Runtime validation that will always be active
            // First check: Verify index is within valid bounds
            if (index < 0 || index >= Rooms.Count)
            {
                throw new ArgumentOutOfRangeException(
                    paramName: nameof(index),
                    message: $"Room index {index} is out of range. Valid range: 0-{Rooms.Count - 1}");
            }
            
            // Second check: Verify the room isn't locked
            if (Rooms[index].IsLocked)
            {
                throw new InvalidOperationException(
            $"Room {index} is locked and cannot be entered without the proper key");
            }
            
            // All checks passed - update the current room index
            CurrentRoomIndex = index;
            
            // Post-condition assertion to verify state was updated correctly
            Debug.Assert(CurrentRoomIndex == index, 
                "Failed to properly update current room index");
        }
    }
}
