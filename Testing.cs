using System.Diagnostics;
using DungeonExplorer.Models;
using DungeonExplorer.Interfaces;

namespace DungeonExplorer
{
    public static class Testing
    {
        public static void RunAllTests()
        {
            TestPlayerInitialization();
            TestInventorySystem();
            TestCombatSystem();
            TestRoomNavigation();
            TestItemUsage();
            Console.WriteLine("All tests passed successfully!");
        }

        private static void TestPlayerInitialization()
        {
            Player player = new Player();
            Debug.Assert(player.Name == "Hero", "Player name should be 'Hero'");
            Debug.Assert(player.Health == 100, "Player health should start at 100");
            Debug.Assert(player.Damage == 10, "Player damage should start at 10");
            Debug.Assert(player.IsAlive(), "New player should be alive");
        }

        private static void TestInventorySystem()
        {
            Player player = new Player();
            Inventory inventory = player.Inventory;

            // Test adding items
            var sword = new Weapon("Sword", 5);
            inventory.AddItem(sword);
            Debug.Assert(inventory.Items.Count == 1, "Inventory should have 1 item");
            Debug.Assert(inventory.GetWeapons().Count == 1, "Should find 1 weapon");

            // Test removing items
            inventory.RemoveItem(0);
            Debug.Assert(inventory.Items.Count == 0, "Inventory should be empty after removal");
        }

        private static void TestCombatSystem()
        {
            Player player = new Player();
            Goblin goblin = new Goblin();

            // Test player attack
            int initialGoblinHealth = goblin.Health;
            player.Attack(goblin);
            Debug.Assert(goblin.Health < initialGoblinHealth, "Goblin should take damage");

            // Test monster attack
            int initialPlayerHealth = player.Health;
            goblin.Attack(player);
            Debug.Assert(player.Health < initialPlayerHealth, "Player should take damage");

            // Test death state
            player.TakeDamage(200);
            Debug.Assert(!player.IsAlive(), "Player should die after massive damage");
        }

        private static void TestRoomNavigation()
        {
            GameMap map = new GameMap();
            Room room1 = new Room("Test Room 1");
            Room room2 = new Room("Test Room 2");
            room2.IsLocked = true;
            room2.KeyItem = "Test Key";

            map.Rooms.Add(room1);
            map.Rooms.Add(room2);

            // Test initial room
            Debug.Assert(map.CurrentRoom == room1, "Should start in first room");

            // Test locked room
            try
            {
                map.MoveToRoom(1);
                Debug.Assert(false, "Should throw when moving to locked room");
            }
            catch (Exception) { /* Expected */ }

            // Test valid movement
            room2.IsLocked = false;
            map.MoveToRoom(1);
            Debug.Assert(map.CurrentRoom == room2, "Should move to second room");
        }

        private static void TestItemUsage()
        {
            Player player = new Player();
            Potion potion = new Potion("Health Potion", 20);
            Key key = new Key("Test Key", "Test Door");

            // Test potion healing
            player.TakeDamage(30);
            int damagedHealth = player.Health;
            potion.Use(player);
            Debug.Assert(player.Health == damagedHealth + 20, "Potion should heal 20 HP");

            // Test key usage
            key.Use(player);
            Debug.Assert(player.Inventory.Items.Count == 0, "Key shouldn't be consumed on use");

            // Test weapon equip
            Weapon weapon = new Weapon("Test Sword", 5);
            int baseDamage = player.Damage;
            weapon.Use(player);
            Debug.Assert(player.Damage == baseDamage + 5, "Weapon should increase damage");
        }
    }
}
