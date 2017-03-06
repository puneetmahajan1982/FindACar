using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarFinder.Logic;

namespace CarFinder.Console
{
    class Program
    {
        private static Helpers _helpers;

        static void Main(string[] args)
        {
            short initialVelocity;
            short initialPosition;

            _helpers = new Helpers();
            ConfigReader configReader = new ConfigReader(_helpers);
            configReader.ReadParameters(out initialVelocity, out initialPosition);

            System.Console.WriteLine("initialVelocity=" + initialVelocity);
            System.Console.WriteLine("initialPosition=" + initialPosition);
            
            RunGame(initialVelocity, initialPosition);
            System.Console.WriteLine("Game Complete");
        }

        private static void RunGame(short initialVelocity, short initialPosition)
        {
            GameRunner gameRunner = new GameRunner(_helpers);
            gameRunner.RunGame(initialVelocity, initialPosition);
        }
    }
}
