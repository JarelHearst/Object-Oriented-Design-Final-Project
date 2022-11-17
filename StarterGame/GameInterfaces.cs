using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterGame
{
    //All of the interfaces that I use for the game.
    public interface GameInterfaces
    {
        public interface ILocking
        {
            bool IsLocked { get; }
            bool IsUnlocked { get; }
            void Lock();
            void Unlock();
            bool MayOpen { get; }
            bool MayClose { get; }
        }
        public interface RoomDelegate
        {
            Door GetExit(string exitName);
            Room ContainingRoom { set; get; }
            Dictionary<string, Door> Exits { get; set; }
            string Description();
        }


        public interface IItem
        {
            String Name { get; set; }
            String LongName { get; }
            float Weight { get; set; }
            string Description { get; }
            string LongDescription { get; }
            void addDecorator(IItem decorator);
            void AddItem(IItem item);
            IItem RemoveItem(string name);
        }

    }
}
