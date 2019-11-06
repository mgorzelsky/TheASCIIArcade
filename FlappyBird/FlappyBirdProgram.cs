using System;
using System.Drawing;
using System.IO;
using System.Threading;

namespace FlappyBird
{
    public class FlappyBirdProgram
    {
        public static int width = 120;
        public static int height = 30;

        public void StartFlappyBird()
        {
            Console.Clear();
            Console.CursorVisible = false;

            string[] flappyBirdSplash = File.ReadAllLines(@"..\..\..\txt\FlappyBirdSplash.txt");
            string contributers = "Contributers: Ruying Chen, Michael Gorzelsky, Matt Juel, Chris Masters, Robert Schroeder";
            string instructions = "Press the Up Arrow or Spacebar to flap higher";

            Render renderer = new Render();
            renderer.DrawGenericScreen(flappyBirdSplash, (width - flappyBirdSplash[3].Length)/2, 4);
            renderer.DrawGenericScreen(contributers, (width - contributers.Length)/2, height - 1);
            renderer.DrawGenericScreen(instructions, (width - instructions.Length)/2, height / 2);

            Thread.Sleep(5000);

            do //Eventually offer the option to play again without leaving the game
            {
                Game game = new Game();
                game.PlayGame();
            } while (false);
        }
    }
}