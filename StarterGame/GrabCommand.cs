using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterGame
{
    //How you can grab an item from a world.
    //When you grab an item, you will get a message saying you picked something up, not a notification.
    class GrabCommand : Command
    {
        public GrabCommand() : base()
        {
            this.Name = "grab";
        }

        override
        public bool Execute(Player player)
        {
            if (this.HasSecondWord())
            {
                player.Grab(this.SecondWord);
            }
            else
            {
                player.ErrorMessage("\nGrab what?");
            }
            return false;
        }
    }
}
