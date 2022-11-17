using System.Collections;
using System.Collections.Generic;
using System;

namespace StarterGame
{
    public class Game
    {
        //"Player" executes the commnad
        Player player;
        Parser parser;
        bool playing;
        //GameClock gc;

        public Game()
        {
            //gc = new GameClock(5000);
            playing = false;
            parser = new Parser(new CommandWords());
            //Singleton design pattern - all entrances will be the same
            player = new Player(GameWorld.Instance.Entrance);
        }

       
        /**
     *  Main play routine.  Loops until end of play.
     */
        public void Play()
        {

            // Enter the main command loop.  Here we repeatedly read commands and
            // execute them until the game is over.

            bool finished = false;
            while (!finished)
            {
                Console.Write("\n>");
                Command command = parser.ParseCommand(Console.ReadLine());
                if (command == null)
                {
                    Console.WriteLine("I don't understand...");
                }
                else
                {
                    finished = command.Execute(player);
                }
            }
        }


        public void Start()
        {
            playing = true;
            player.OutputMessage("Welcome to the world of Everrage!" + "\nIn this world you need to find notes that are scattered in rooms to decipher a code.");
            player.OutputMessage("Once you think that you have all the clues you need to solve the word at the end...head to the win room!");
            player.InfoMessage("Hint: The password only has 10(ten) characters in it with no spaces.  ");
            player.WarningMessage("Be warned, there are some fake notes that can lead you astray, be careful of them.");
            player.WarningMessage("One more warning, you also have three tries to win at the win room, if you are incorrect three times...");
            player.ErrorMessage("you lose.\n");
            player.GoodJobMessage("But no pressure!");
            player.OutputMessage("Have fun and we will see you at the win room!");
            player.GoodJobMessage("Good Luck!!\n");
            Console.WriteLine(player.CurrentRoom.Description());
        }

        public void End()
        {
            playing = false;
            player.OutputMessage(Goodbye());
        }

        public string Welcome()
        {

            return "Welcome to the World of Everrage!\n\nThe World of Everage is a new, incredibly great adventure game!\n\nType 'help' if you need help." + player.CurrentRoom.Description();          
        }

        public string Goodbye()
        {
            return "\nThank you for playing. Hope to see you again! \n";
        }

        public string Win()
        {
            return "\n Good job!!! You Win!!! \n";
        }

    }
}
