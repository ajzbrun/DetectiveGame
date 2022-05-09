using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace RK_TextAdventure
{
    class Program
    {
        static string playerName;
        static List<Room> Map;
        static Room currentRoom;
        static Dialog Dialogs;
        static List<Item> Inventory;

        static void Main(string[] args)
        {
            InitializeGame();


            Console.ReadKey();
        }

        /// <summary>
        /// Sets the base game information such as rooms (of the entire map), available dialogs, items and it's position in the maps
        /// </summary>
        static void InitializeGame()
        {
            Console.Clear();

            //Lineal structure data
            Inventory = new List<Item>(); //we create the empty invetory
            Item gardenKey = new Item(1, "Back Garden Key", "This key enables you to go into the back garden");
            Item flashLight = new Item(2, "Flashlight", "It will serve you to find more objects hidden in the rooms");

            //graph structure
            Room roomA = new Room("Hall", 3);
            currentRoom = roomA;
            Room roomB = new Room("Kitchen", 3, gardenKey);
            Room roomC = new Room("Living", 4);
            Room roomD = new Room("Bathroom", 2, flashLight);
            Room roomE = new Room("Back Garden");

            Door doorAB = new Door(roomA, roomB); //door from hall to kitchen
            Door doorAC = new Door(roomA, roomC); //door from hall to living room
            Door doorCD = new Door(roomC, roomD); //door from living room to bath
            Door doorCE = new Door(roomC, roomE, gardenKey); //door from living room to back garden

            roomA.AddDoor(doorAB);
            roomB.AddDoor(doorAB);
            roomA.AddDoor(doorAC);
            roomC.AddDoor(doorAC);
            roomC.AddDoor(doorCD);
            roomD.AddDoor(doorCD);
            roomC.AddDoor(doorCE);
            roomE.AddDoor(doorCE);


            Map = new List<Room>();
            Map.Add(roomA);
            Map.Add(roomB);
            Map.Add(roomC);
            Map.Add(roomD);
            Map.Add(roomE);

            //tree structure
            Dialogs = new Dialog("Welcome");
            Dialog bD = Dialogs.AddAOption("Go to kitchen");
            Dialog cD = Dialogs.AddBOption("Go to living room");
            bD.AddAOption("You cant do anything here");
            Dialog dD = cD.AddAOption("Go to the bathroom");
            Dialog dE = cD.AddAOption("Go to the back garden");

            Console.WriteLine("Welcome to MisteriousHouse. You're not the first detective man to arrive...");
            Console.WriteLine("Tell me your name, newbie");
            playerName = Console.ReadLine();
            Console.Clear();

            Console.WriteLine("Welcome to this adventure, "+playerName+".");
            Thread.Sleep(1000);
            Console.Write("Let's begin.");
            //PrintWaiting();
            Console.Clear();

            //start the game
            MoveTo(doorAB, true);
        }

        static void MoveTo(Door targetDoor, bool stayInRoom = false)
        {
            Console.Clear();
            PrintWaiting();

            if (!stayInRoom)
            {
                if (targetDoor.HasKey() != null && !Inventory.Contains(targetDoor.HasKey()))
                {
                    //the door is locked with a key and we dont have the key on our inventory
                    Console.WriteLine("\nThis door is locked by a key. You must find it first.\n");
                }
                else
                {
                    if (currentRoom == targetDoor.GetBRoom())
                        currentRoom = targetDoor.GetARoom();
                    else
                        currentRoom = targetDoor.GetBRoom();

                    if (currentRoom.HasItem() != null) //if there's an object in the room
                    {
                        if (!Inventory.Contains(currentRoom.HasItem())) //check if the item has already been found
                        {
                            if (currentRoom.HasItem().GetName().ToLower().Contains("key")) //if it's a key, we'll only find it if we already found a flashlight
                            {
                                if (Inventory.Find(i => i.GetName().ToLower().Contains("flashlight")) != null)
                                    AddItemToInventory(currentRoom.HasItem());
                            }
                            else
                                AddItemToInventory(currentRoom.HasItem());
                        }
                    }
                }
            }

            Console.WriteLine("\nYou (O) are in the " + currentRoom.GetName());
            PrintMap();
            
            //list the available options
            List<Door> availableDoors = currentRoom.GetDoors();
            Dictionary<int, List<Door>> options = new Dictionary<int, List<Door>>();
            int i = 1;

            Console.WriteLine("Press 0 to see your inventory");
            foreach (Door door in availableDoors)
            {
                Room room = (currentRoom == door.GetBRoom()) ? door.GetARoom() : door.GetBRoom();
                List<Door> doors = room.GetDoors();
                options.Add(i, doors);
                Console.WriteLine("Press " + i.ToString() + " to go to the " + room.GetName());

                i++;
            }

            bool validOption = false;
            int selectedOption = 0;
            List<Door> doorsAvailable = new List<Door>();

            while (!validOption)
            {
                try { selectedOption = Convert.ToInt32(Console.ReadLine()); } catch { }

                if(selectedOption != 0)
                    doorsAvailable = options.GetValueOrDefault(selectedOption);

                if (doorsAvailable != null || selectedOption == 0)
                    validOption = true;
                else
                {
                    PrintWaiting();
                    Console.WriteLine("The option is not valid. Try again...");
                }
            }

            if (selectedOption != 0)
                MoveTo(doorsAvailable.Find(d => d.GetARoom() == currentRoom || d.GetBRoom() == currentRoom));
            else
            {
                if(Inventory.Count > 0)
                {
                    Console.WriteLine("Your inventory:");
                    char abc = 'A';
                    foreach(Item item in Inventory)
                    {
                        Console.WriteLine(abc+"- "+item.GetName());
                        abc++;
                    }
                    PrintWaiting();
                }
                else
                    Console.WriteLine("Your inventory is empty");

                PrintWaiting();

                MoveTo(currentRoom.GetDoors().Find(d => d.GetARoom() == currentRoom || d.GetBRoom() == currentRoom), true); //we mantain the room
            }
        }

        static void PrintWaiting()
        {
            Console.WriteLine("");
            Thread.Sleep(500);
            for (int i=0;i<3;i++)
            {
                Console.Write(".");
                Thread.Sleep(500);
            }
        }

        static void PrintMap()
        {
            string strmap = "";
            int count = 3;
            List<Room> rooms = Map.Take(3).ToList();
            while (rooms.Count > 0)
            {
                foreach (Room room in rooms)
                {
                    for (int i = 0; i <= room.GetSize(); i++)
                        strmap += "_";
                    strmap += " ";
                }
                strmap += "\n";

                for(int j=2; j<=4; j++)
                {
                    foreach (Room room in rooms)
                    {
                        for (int i = 0; i <= room.GetSize(); i++)
                        {
                            if (room.GetSize() < j)
                            {
                                strmap += " ";
                            }
                            else if (i == 0)
                                strmap += "|";
                            else if (i == room.GetSize())
                                strmap += "|";
                            else
                            {
                                if(currentRoom.GetName() == room.GetName() && room.GetSize() == j && i==1)
                                    strmap += "O";
                                else if (room.GetSize() == j)
                                    strmap += "_";
                                else
                                    strmap += " ";
                            }
                        }
                        strmap += " ";
                    }
                    strmap += "\n";
                }

                count += 3;
                rooms = Map.Skip(count - 3).Take(3).ToList();
            }

            Console.WriteLine(strmap);
        }

        static void AddItemToInventory(Item newItem)
        {
            Console.WriteLine("\nGood news! You've found a " + newItem.GetName()+". It's now available in your inventory, keep searching...\n");
            Inventory.Add(newItem);

            //TODO: here I should sort the invetory (item list) with one of the sort methods


        }
    }
}
