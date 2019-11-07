using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Snake
{
    public class SnakeProgram
    {
        //height and width will be referenced many times throughout the game. They can be changed here and the
        //rest will dynamically adjust
        public static int width = 50;
        public static int height = 20;
        public void StartSnake()
        {
            int consoleWidth = 120;
            int consoleHeight = 25;
            Console.Clear();
            Console.CursorVisible = false;

            //Grab the appropriate images and put them in variables, then write them to the screen. Wait 5 seconds, then move on.
            string[] snakeSplash = File.ReadAllLines(@"txt\SnakeSplash.txt");
            string contributers = "Contributers: Michael Barta, Michael Gorzelsky, Radiah Jones";
            string instructions = "Use the arrow keys to turn. Avoid walls and yourself. Eat the food --> *";

            Render renderer = new Render();
            renderer.DrawGenericScreen(snakeSplash, (consoleWidth - snakeSplash[7].Length) / 2, 1);
            renderer.DrawGenericScreen(contributers, (consoleWidth - contributers.Length) / 2, consoleHeight - 1);
            renderer.DrawGenericScreen(instructions, (consoleWidth - instructions.Length) / 2, consoleHeight / 2 + 2);

            Thread.Sleep(5000);

            Console.Clear();
            Game game = new Game();
            int score = game.PlayGame(); //PlayGame return an int score to be used in the future for score tracking.
        }
    }
}