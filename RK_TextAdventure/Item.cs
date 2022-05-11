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
        Item dependantItem;

        public Item(int id, string name, string desc, Item dep = null)
        {
            Id = id;
            Name = name;
            Description = desc;
            dependantItem = dep;
        }

        public string GetName()
        {
            return Name;
        }

        public Item GetDependantItem()
        {
            return dependantItem;
        }
    }
}
