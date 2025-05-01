using System;
using System.Linq;
using DungeonExplorer.Models;
using DungeonExplorer.Interfaces;
using DungeonExplorer.Utilities;

namespace DungeonExplorer
{
    public class Game
    {
        private Player _player = new Player();
        private GameMap _map = new GameMap();

        public void Start()
        {
            InitializeGameWorld();
            Console.WriteLine("=== Dungeon Explorer ===");
            Console.WriteLine("Navigate rooms, solve puzzles, find keys, and defeat monsters!");

            while (_player.IsAlive() && _map.CurrentRoomIndex < _map.Rooms.Count)
            {
                Room currentRoom = _map.CurrentRoom;
                Console.WriteLine($"\n{currentRoom.Description}");

                HandlePuzzleRoom(currentRoom);
                BattleMonsters(currentRoom);

                if (_player.IsAlive())
                {
                    CollectItems(currentRoom);

                    if (currentRoom != _map.Rooms.Last())
                    {
                        Console.WriteLine("\nPress any key to attempt to move to the next room...");
                        Console.ReadKey();
                        MoveToNextRoom();
                    }
                }
            }

            Console.WriteLine(_player.IsAlive() 
                ? "\nYou conquered the dungeon! Victory!" 
                : "\nGame Over - You were defeated!");
        }

        private void MoveToNextRoom()
        {
            Room nextRoom = _map.Rooms[_map.CurrentRoomIndex + 1];
            
            if (nextRoom.IsLocked)
            {
                Console.WriteLine($"\nThe door is locked! You need a {nextRoom.KeyItem}.");
                
                if (_player.Inventory.Items.Any(item => item.Name == nextRoom.KeyItem))
                {
                    Console.WriteLine($"You used the {nextRoom.KeyItem} to unlock the door!");
                    nextRoom.IsLocked = false;
                    _map.MoveToRoom(_map.CurrentRoomIndex + 1);
                }
                else
                {
                    Console.WriteLine("You don't have the required key!");
                }
            }
            else
            {
                _map.MoveToRoom(_map.CurrentRoomIndex + 1);
            }
        }

        private void HandlePuzzleRoom(Room room)
        {
            if (!room.HasPuzzle) return;

            Console.WriteLine("\nThe runes on the walls begin to glow...");
            Console.WriteLine("Solve the riddle to proceed:");
            Console.WriteLine("I speak without a mouth and hear without ears. I have no body but come alive with wind. What am I?");
            
            string answer = Console.ReadLine()?.ToLower();
            if (answer == "echo")
            {
                Console.WriteLine("\nThe runes fade! A hidden compartment opens...");
                room.Items.Add(new Key("Rusty Key", "the iron door"));
            }
            else
            {
                Console.WriteLine("\nNothing happens... The runes continue to glow.");
            }
        }

        private void BattleMonsters(Room room)
        {
            var aliveMonsters = room.Monsters.Where(m => m.IsAlive()).ToList();

            foreach (var monster in aliveMonsters)
            {
                Console.WriteLine($"\nA {monster.Name} (HP: {monster.Health}) appears!");

                while (monster.IsAlive() && _player.IsAlive())
                {
                    Console.WriteLine($"\n[Player HP: {_player.Health}]");
                    Console.WriteLine("1. Attack\n2. Use Item\n3. Flee");

                    int choice = Input.ReadInt("Choose action: ", 1, 3);

                    switch (choice)
                    {
                        case 1:
                            _player.Attack(monster);
                            break;
                        case 2:
                            UseItemMenu();
                            break;
                        case 3:
                            if (new Random().Next(2) == 0)
                            {
                                Console.WriteLine("You successfully escaped!");
                                return;
                            }
                            Console.WriteLine("Escape failed! Monster blocks your path!");
                            break;
                    }

                    if (monster.IsAlive()) 
                    {
                        monster.Attack(_player);  
                    }
                }

                if (!_player.IsAlive()) break;
            }
        }

        private void UseItemMenu()
        {
            var weapons = _player.Inventory.GetWeapons();
            var potions = _player.Inventory.Items.OfType<Potion>().ToList();
            var keys = _player.Inventory.Items.OfType<Key>().ToList();

            Console.WriteLine("\n=== INVENTORY ===");
            
            Console.WriteLine("Weapons:");
            for (int i = 0; i < weapons.Count; i++)
                Console.WriteLine($"{i + 1}. {weapons[i].Name} (+{weapons[i].DamageBoost} DMG)");

            Console.WriteLine("\nPotions:");
            for (int i = 0; i < potions.Count; i++)
                Console.WriteLine($"{weapons.Count + i + 1}. {potions[i].Name} (+{potions[i].HealAmount} HP)");

            Console.WriteLine("\nKeys:");
            for (int i = 0; i < keys.Count; i++)
                Console.WriteLine($"{weapons.Count + potions.Count + i + 1}. {keys[i].Name} (for {keys[i].DoorDescription})");

            int totalItems = weapons.Count + potions.Count + keys.Count;
            if (totalItems == 0)
            {
                Console.WriteLine("No items available!");
                return;
            }

            int choice = Input.ReadInt("Select item (0 to cancel): ", 0, totalItems);
            if (choice == 0) return;

            if (choice <= weapons.Count)
                _player.UseItem(weapons[choice - 1].Name);
            else if (choice <= weapons.Count + potions.Count)
                _player.UseItem(potions[choice - weapons.Count - 1].Name);
            else
                _player.UseItem(keys[choice - weapons.Count - potions.Count - 1].Name);
        }

        private void CollectItems(Room room)
        {
            if (!room.Items.Any()) return;

            Console.WriteLine("\nItems in the room:");
            foreach (var item in room.Items.ToList())
            {
                Console.WriteLine($"- {item.Name}");
                Console.WriteLine("Take it? (Y/N)");
                
                if (Input.ReadYesNo())
                {
                    ((ICollectible)item).Collect(_player);
                    room.Items.Remove(item);
                }
            }
        }

        private void InitializeGameWorld()
        {
            // Room 1 - Entrance
            var room1 = new Room("A dark cave entrance with flickering torches. There's a musty smell in the air.");
            room1.Monsters.Add(new Goblin());
            room1.Items.Add(new Weapon("Rusty Dagger", 3));

            // Room 2 - Locked Corridor
            var room2 = new Room("A damp corridor with an imposing iron door blocking your path.");
            room2.IsLocked = true;
            room2.KeyItem = "Rusty Key";
            room2.Monsters.Add(new Goblin());
            room2.Monsters.Add(new Goblin());

            // Room 3 - Puzzle Room
            var room3 = new Room("A circular chamber with glowing runes carved into the stone walls.");
            room3.HasPuzzle = true;
            room3.Items.Add(new Potion("Health Potion", 20));

            // Room 4 - Dragon's Lair
            var room4 = new Room("A massive cavern with bubbling lava pools. The heat is intense.");
            room4.Monsters.Add(new Dragon());
            room4.Items.Add(new Weapon("Dragon Slayer", 15));
            room4.Items.Add(new Key("Golden Key", "the treasure vault"));

            // Room 5 - Treasure Vault
            var room5 = new Room("The treasure vault! Gold and gems glitter in piles everywhere.");
            room5.IsLocked = true;
            room5.KeyItem = "Golden Key";
            room5.Items.Add(new Potion("Elixir of Life", 50));

            _map.Rooms.Add(room1);
            _map.Rooms.Add(room2);
            _map.Rooms.Add(room3);
            _map.Rooms.Add(room4);
            _map.Rooms.Add(room5);
        }
    }
}
