using System;
using System.Collections.Generic;
using System.Linq;
using Checkers.Model;

namespace Checkers
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = "";
            while (input.ToUpper() != "Q")
            {
                Console.WriteLine("---------------");
                Console.WriteLine("I) Input string");
                Console.WriteLine("Q) Quit");
                Console.Write("Please enter a command: ");
                input = Console.ReadLine();
                if (input == "I" || input == "i")
                {
                    Console.WriteLine("Please input string like \"wa1;wc3;Wg2;bg7;Bf6\":");
                    var pieces = Console.ReadLine();
                    Console.WriteLine("Please enter indicator what the side is to move: w - white, b - black.");
                    var turn = Console.ReadLine();
                    try
                    {
                        var game = new Game();
                        game.Initialize(pieces, turn);

                        IEnumerable<Position> moves = Game.GetAllMoves(game);
                        foreach (var move in moves)
                        {
                            Console.WriteLine(move);
                        }
                        if (!moves.Any())
                        {
                            Console.WriteLine("No moves.");
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
        }
    }
}
