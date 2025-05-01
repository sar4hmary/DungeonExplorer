using System;
using System.Linq;
using DungeonExplorer.Models;
using DungeonExplorer.Interfaces;
using DungeonExplorer.Utilities;

namespace DungeonExplorer
{
    public class Game
    {
        private Player _player = new Player();  //The player character
        private GameMap _map = new GameMap();   //Manages dungeon rooms and navigation

        ///<summary>
        ///Main game loop that controls the entire game flow
        ///</summary>
        public void Start()
        {
            InitializeGameWorld();  //Set up rooms, monsters and items
            Console.WriteLine("=== Dungeon Explorer ===");
            Console.WriteLine("Navigate rooms, battle monsters, and collect items!");

            //Main game loop will run while player is alive and has rooms to explore
            while (_player.IsAlive() && _map.CurrentRoomIndex < _map.Rooms.Count)
            {
                Room currentRoom = _map.CurrentRoom;  //Get current room
                Console.WriteLine($"\n{currentRoom.Description}");

                //fight all monsters in room
                BattleMonsters(currentRoom);

                //Only proceed if player survived battles
                if (_player.IsAlive())
                {
                    //collect items in room
                    CollectItems(currentRoom);

                    //move to next room if not last room
                    if (_player.IsAlive() && currentRoom != _map.Rooms.Last())
                    {
                        Console.WriteLine("\nPress any key to move to the next room...");
                        Console.ReadKey();  //Pause for player readiness
                        _map.MoveToRoom(_map.CurrentRoomIndex + 1);  //Advance room
                    }
                }
            }

            //Game end message
            Console.WriteLine(_player.IsAlive() 
                ? "\nYou escaped the dungeon! Victory!" 
                : "\nGame Over - You were defeated!");
        }

        ///<summary>
        ///Handles combat between player and monsters in current room
        ///</summary>
        ///<param name="room">The current room with monsters</param>
        private void BattleMonsters(Room room)
        {
            //LINQ query to get only alive monsters 
            var aliveMonsters = room.Monsters.Where(m => m.IsAlive()).ToList();

            //Battle each monster sequentially
            foreach (var monster in aliveMonsters)
            {
                Console.WriteLine($"\nA {monster.Name} (HP: {monster.Health}) appears!");

                while (monster.IsAlive() && _player.IsAlive())
                {
                    //Display combat options
                    Console.WriteLine($"\n[Player HP: {_player.Health}]");
                    Console.WriteLine("1. Attack\n2. Use Item\n3. Flee");

                    //Get validated player input (1-3)
                    int choice = Input.ReadInt("Choose action: ", 1, 3);

                    //Handle player choice
                    switch (choice)
                    {
                        case 1: //Attack
                            _player.Attack(monster);  //Player attacks monster
                            break;
                            
                        case 2: //Use item
                            UseItemMenu();  //Open inventory
                            break;
                            
                        case 3: //Flee (50% success chance)
                            if (new Random().Next(2) == 0)  //Random 0 or 1
                            {
                                Console.WriteLine("You successfully escaped!");
                                return;  //Exit combat
                            }
                            Console.WriteLine("Escape failed! Monster blocks your path!");
                            break;
                    }

                    //Monster counterattacks if still alive
                    if (monster.IsAlive()) 
                    {
                        //Dynamic Polymorphism: Dragon has different attack than Goblin
                        monster.Attack(_player);  
                    }
                }

                //Check if player died during battle
                if (!_player.IsAlive()) break;
            }
        }

        ///<summary>
        ///Displays inventory and handles item usage
        ///</summary>
        private void UseItemMenu()
        {
            //LINQ queries to filter items by type
            var weapons = _player.Inventory.GetWeapons();  //Gets all Weapon items
            var potions = _player.Inventory.Items.OfType<Potion>().ToList();  //Gets all Potions

            Console.WriteLine("\n=== INVENTORY ===");
            
            //Display weapons
            Console.WriteLine("Weapons:");
            for (int i = 0; i < weapons.Count; i++)
                Console.WriteLine($"{i + 1}. {weapons[i].Name} (+{weapons[i].DamageBoost} DMG)");

            //Display potions
            Console.WriteLine("\nPotions:");
            for (int i = 0; i < potions.Count; i++)
                Console.WriteLine($"{weapons.Count + i + 1}. {potions[i].Name} (+{potions[i].HealAmount} HP)");

            //Check if inventory is empty
            if (weapons.Count + potions.Count == 0)
            {
                Console.WriteLine("No items available!");
                return;
            }

            //Get player's item choice (0 to cancel)
            int choice = Input.ReadInt("Select item (0 to cancel): ", 0, weapons.Count + potions.Count);
            if (choice == 0) return;  //Player cancelled

            //Static Polymorphism: Use either Weapon or Potion via method overloading
            if (choice <= weapons.Count)
                _player.UseItem(weapons[choice - 1].Name);  //Use weapon
            else
                _player.UseItem(potions[choice - weapons.Count - 1].Name);  //Use potion
        }

        ///<summary>
        ///Allows player to collect items in the current room
        ///</summary>
        ///<param name="room">The current room with items</param>
        private void CollectItems(Room room)
        {
            //Skip if no items in room
            if (!room.Items.Any()) return;

            Console.WriteLine("\nItems in the room:");
            //ToList() creates copy to avoid modification issues during iteration
            foreach (var item in room.Items.ToList())  
            {
                Console.WriteLine($"- {item.Name}");
                Console.WriteLine("Take it? (Y/N)");
                
                if (Input.ReadYesNo())
                {
                    //Interface implementation: ICollectible.Collect()
                    ((ICollectible)item).Collect(_player);  
                    room.Items.Remove(item);  //Remove from room
                }
            }
        }

        ///<summary>
        ///Initialises the game world with rooms, monsters and items
        ///</summary>
        private void InitializeGameWorld()
        {
            //ROOM 1 - Easy difficulty
            var room1 = new Room("A dark cave with flickering torches...");
            room1.Monsters.Add(new Goblin());  //Weak enemy
            room1.Items.Add(new Weapon("Rusty Sword", 5));  //Small damage boost

            //ROOM 2 - Hard difficulty
            var room2 = new Room("A fiery dungeon with lava flows...");
            room2.Monsters.Add(new Dragon());  //Strong enemy with special attack
            room2.Items.Add(new Potion("Health Potion", 20));  //Healing item

            //Add rooms to game map
            _map.Rooms.Add(room1);
            _map.Rooms.Add(room2);

            Console.WriteLine($"Dungeon initialized with {_map.Rooms.Count} rooms.");
        }
    }
}
