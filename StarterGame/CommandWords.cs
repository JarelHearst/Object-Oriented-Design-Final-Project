using System.Collections;
using System.Collections.Generic;
using System;

namespace StarterGame
{
    //All of the command words that are displayed with the "help" command.
    public class CommandWords
    {
        Dictionary<string, Command> commands;
        private static Command[] commandArray = { new GoCommand(), new BackCommand(), new QuitCommand(), new OpenCommand(), new SayCommand(), new UnlockCommand(), new InspectCommand(), new GrabCommand(), new DropCommand(), new InventoryCommand() }; //, new CastCommand()

        public CommandWords() : this(commandArray)
        {
        }

        public CommandWords(Command[] commandList)
        {
            commands = new Dictionary<string, Command>();
            foreach (Command command in commandList)
            {
                commands[command.Name] = command;
            }
            //Commands in the command list.
            Command help = new HelpCommand(this);

            commands[help.Name] = help;
        }

        public Command Get(string word)
        {
            Command command = null;
            commands.TryGetValue(word, out command);
            return command;
        }

        public string Description()
        {
            string commandNames = "";
            Dictionary<string, Command>.KeyCollection keys = commands.Keys;
            foreach (string commandName in keys)
            {
                commandNames += " " + commandName;
            }
            return commandNames;


        }
    }
}
