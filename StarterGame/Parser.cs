using System.Collections;
using System.Collections.Generic;
using System;

namespace StarterGame
{
    // How many words that you are able to type into the command.
    public class Parser
    {
        private CommandWords commands;

        public Parser() : this(new CommandWords())
        {

        }

        public Parser(CommandWords newCommands)
        {
            commands = newCommands;
        }
        //A method to parse a command, only able to enter two words.
        public Command ParseCommand(string commandString)
        {
            Command command = null;
            string[] words = commandString.Split(' ');
            if (words.Length > 0)
            {
                command = commands.Get(words[0]);
                if (command != null)
                {
                    if (words.Length > 1)
                    {
                        command.SecondWord = words[1];
                    }
                    else
                    {
                        command.SecondWord = null;
                    }
                }
                else
                {
                    Console.WriteLine(">>>Did not find the command " + words[0]);
                }
            }
            else
            {
                Console.WriteLine("No words parsed!");
            }
            return command;
        }

        public string Description()
        {
            return commands.Description();
        }
    }
}
