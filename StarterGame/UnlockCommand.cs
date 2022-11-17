using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterGame
{
    //A command where you can unlock a door if a lock is present. 
    class UnlockCommand : Command
    {
        public UnlockCommand() : base()
        {
            this.Name = "open";
        }

        override
        public bool Execute(Player player)
        {
            if (this.HasSecondWord())
            {
                player.Unlock(this.SecondWord);
            }
            else
            {
                player.ErrorMessage("\nUnlock What?");
            }
            return false;
        }
    }
}
