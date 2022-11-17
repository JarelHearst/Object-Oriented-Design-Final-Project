using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterGame
{
    //A command where you want to say a word, only one word can be said.
    class SayCommand : Command
    {
        public SayCommand() : base()
        {
            this.Name = "say";
        }

        override
        public bool Execute(Player player)
        {
            if (this.HasSecondWord())
            {
                player.Say(this.SecondWord);
            }
            else
            {
                player.ErrorMessage("\nSay What?");
            }
            return false;
        }
    }
}
