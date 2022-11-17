using System.Collections;
using System.Collections.Generic;

namespace StarterGame
{
    //The go command where you type "go".
    //Only words that go with go is "south", "north", "east", "west"
    public class GoCommand : Command
    {
        public GoCommand() : base()
        {
            this.Name = "go";
        }

        override
        public bool Execute(Player player)
        {
            if (this.HasSecondWord())
            {
                player.WaltTo(this.SecondWord);
            }
            else
            {
                player.ErrorMessage("\nGo Where?");
            }
            return false;
        }
    }
}
