using System;

namespace StarterGame
{
    class Program
    {
        //How the program starts, plays, and ends.
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            Game game = new Game();
            game.Start();
            game.Play();
            game.End();
        }
    }
}
