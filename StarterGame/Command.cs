using System.Collections;
using System.Collections.Generic;
using System;

namespace StarterGame
{
    //How you enter a command for the game. Requires the command and what you want to do with that command.
    //Command design pattern.
    public abstract class Command
    {
        private string _name;
        public string Name { get { return _name; } set { _name = value; } }
        private string _secondWord;
        public string SecondWord { get { return _secondWord; } set { _secondWord = value; } }

        //Default constructor
        public Command()
        {
            this.Name = "";
            this.SecondWord = null;
        }

        public bool HasSecondWord()
        {
            return this.SecondWord != null;
        }


        public abstract bool Execute(Player player);
    }
}
