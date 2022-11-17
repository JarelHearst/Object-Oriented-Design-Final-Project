using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterGame
{
    //Back Command for the game when you type in back, you will go back to the last room.
    class BackCommand : Command
    {
        public BackCommand() : base()
        {
            this.Name = "back";
        }

        override
        public bool Execute(Player player)
        {
            if (player.BackTo())
            {
            }
            else
            {
                player.ErrorMessage("I can not go anymore back.");
            }
            return false;
        }
    }
}
