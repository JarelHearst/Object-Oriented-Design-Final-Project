using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterGame
{
    //Interface for my items. All items have a varient of a name and a weight for that item. 
    public interface IItem
    {
        String Name { get; set; }

        float Weight { get; set; }
        String LongName { get; }

        string Description { get; }
        void AddDecorator(IItem decorator);
        void AddItem(IItem item);
        IItem RemoveItem(string name);
        string LongDescription { get; }

        IItem Clone();
    }
}
