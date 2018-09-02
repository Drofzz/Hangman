using System;

namespace Hangman
{
    class Program
    {
        static void Main(string[] args)
        {

            var words = System.IO.File.ReadAllLines("words.txt");

            var game = Game.Create(words);

            Console.WriteLine("Playing Hangman!");

            Console.WriteLine($"Guess following: {game.Display}");

            while (!game.GameOver){
                Console.Write("Guess 1 Character: ");
                var c = Console.ReadKey().KeyChar;
                Console.WriteLine();
                try{
                game.Guess(c);
                Console.WriteLine($"Current Display (T:{game.Tries} F:{game.Fails}): {game.Display}");
                }
                catch(Exception e){
                    Console.WriteLine(e.Message);
                }

            }

            Console.WriteLine("Game Over. [Press Any Key To Exit]");
            Console.ReadKey();
        }
    }
}
