using System.Collections;
using System.Collections.Generic;

namespace StarterGame
{
    //Only command that needs to be by itself.
    //When you type it, it will remind you of the commands you have at use.
    public class HelpCommand : Command
    {
        CommandWords words;

        public HelpCommand() : this(new CommandWords())
        {
        }

        public HelpCommand(CommandWords commands) : base()
        {
            words = commands;
            this.Name = "help";
        }

        override
        public bool Execute(Player player)
        {
            if (this.HasSecondWord())
            {
                player.OutputMessage("\nI cannot help you with " + this.SecondWord);
            }
            else
            {
                player.OutputMessage("\nYou are lost. You are alone. You wander around the world, \n\nYour available commands are " + words.Description());
                player.InfoMessage("\nIf you need help finding the win room, it is north of Norg(the echo room)");
            }
            return false;
        }
    }
}
