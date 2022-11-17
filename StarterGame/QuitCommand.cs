﻿using System.Collections;
using System.Collections.Generic;

namespace StarterGame
{
    //when you use quit, you quit the whole game.
    public class QuitCommand : Command
    {

        public QuitCommand() : base()
        {
            this.Name = "quit";
        }

        override
        public bool Execute(Player player)
        {
            bool answer = true;
            if (this.HasSecondWord())
            {
                player.OutputMessage("\nI cannot quit " + this.SecondWord);
                answer = false;
            }
            return answer;
        }
    }
}
