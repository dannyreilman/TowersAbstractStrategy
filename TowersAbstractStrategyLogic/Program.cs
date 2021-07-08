using Mono.Options;
using System;
using System.Collections.Generic;

namespace TowersAbstractStrategyLogic
{
    class Program
    {
        const int DEFAULT_WIDTH = 8;

        enum AIType
        {
            None,
            Random
        }

        static void Main(string[] args)
        {
            bool showHelp = false;
            bool widthSupplied = false;
            bool heightSupplied = false;
            int width = DEFAULT_WIDTH;
            int height = DEFAULT_WIDTH;

            AIType player1AI = AIType.None;
            AIType player2AI = AIType.None;

            var optionsSet = new OptionSet()
            {
                { "h|help",  "show this message and exit",
                   v => showHelp = v != null },
                { "w|width=", "set the width of the board",
                  (int v) => {
                      widthSupplied = true;
                      width = v;
                    }
                },
                { "h|height=", "set the height of the board. If width is supplied and height is not the board will be a square.",
                  (int v) => {
                      heightSupplied = true;
                      height = v;
                    }
                },
                {"ai1", "set the type of the first player AI (and disables human control of the first player)\n" +
                        "Possible values are [Random]",
                  v =>
                  {
                    if(!Enum.TryParse(v, true, out player1AI))
                        throw new OptionException("Unknown AIType", "ai1");
                  }
                },
                {"ai2", "set the type of the second player AI (and disables human control of the second player)\n" +
                        "Possible values are [Random]",
                  v =>
                  {
                    if(!Enum.TryParse(v, true, out player2AI))
                        throw new OptionException("Unknown AIType", "ai2");
                  }
                }
            };

            try
            {
                List<string> extras = optionsSet.Parse(args);
                foreach (string s in extras)
                {
                    Console.Error.WriteLine($"Unknown argument {s}");
                }

            }
            catch (OptionException e)
            {
                Console.Write("play: ");
                Console.WriteLine(e.Message);
                Console.WriteLine("Try `play --help' for more information.");
                return;
            }

            if (showHelp)
            {
                ShowHelp(optionsSet);
                return;
            }

            // If no height supplied the board is a square
            if (widthSupplied && !heightSupplied)
            {
                height = width;
            }

            Board board = new Board((width, height));

            //TODO: Game logic
        }

        static void ShowHelp(OptionSet p)
        {
            Console.WriteLine("Usage: play [OPTIONS]");
            Console.WriteLine("Play a game of Towers Strategy Game. You can play with other humans or vs an AI.");
            Console.WriteLine();
            Console.WriteLine("Options:");
            p.WriteOptionDescriptions(Console.Out);
        }
    }
}
