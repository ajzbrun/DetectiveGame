using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RK_TextAdventure
{
    class Door
    {
        Room roomA;
        Room roomB;
        Item key;

        public Door(Room rA, Room rB, Item k_obj=null)
        {
            roomA = rA;
            roomB = rB;
            if (k_obj != null) //not all the doors are locked
                key = k_obj;
        }

        public Item HasKey()
        {
            return key;
        }

        public Room GetARoom()
        {
            return roomA;
        }
        public Room GetBRoom()
        {
            return roomB;
        }
    }
}
