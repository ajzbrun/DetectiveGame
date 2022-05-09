using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RK_TextAdventure
{
    class Item
    {
        int Id;
        string Name;
        string Description;

        public Item(int id, string name, string desc)
        {
            Id = id;
            Name = name;
            Description = desc;
        }

        public string GetName()
        {
            return Name;
        }
    }
}
