using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RK_TextAdventure
{
    class Room
    {
        string Name;
        List<Door> Doors;
        Item roomItem;
        int Size;

        public Room(string name, int size = 2, Item item = null)
        {
            Doors = new List<Door>();
            Name = name;            
            roomItem = item;
            size = size < 2 ? 2 : size; //min room size = 2
            Size = size > 4 ? 4 : size; //max size = 4
        }

        public void AddDoor(Door d)
        {
            Doors.Add(d);
        }

        public Item HasItem()
        {
            return roomItem;
        }
        public string GetName()
        {
            return Name;
        }

        public List<Door> GetDoors()
        {
            return Doors;
        }

        public int GetSize()
        {
            return Size;
        }
    }
}
