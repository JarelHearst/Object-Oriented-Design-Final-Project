using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterGame
{
    //When inspecting an item, it will tell you all the details about it and how much it weighs.
    class InspectCommand : Command
    {
        public InspectCommand() : base()
        {
            this.Name = "inspect";
        }

        override
        public bool Execute(Player player)
        {
            if (this.HasSecondWord())
            {
                player.Inspect(this.SecondWord);
            }
            else
            {
                player.ErrorMessage("\nInspect What?");
            }
            return false;
        }
    }
}
