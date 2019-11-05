using System;
using System.Drawing;
using System.Threading;

namespace FlappyBird
{
    //hi
    public class FlappyBirdProgram
    {
        public void StartFlappyBird()
        {
            int width = 160;
            int height = 40;
            Console.Clear();
            Console.CursorVisible = false;
            Console.SetWindowSize(1, 1);
            Console.SetBufferSize(width, height + 1);
            Console.SetWindowSize(width, height + 1);

            Game game = new Game();
            Console.WriteLine("Welcome to flappy bird in the terminal.");
            Thread.Sleep(2000);

            do
            {
                game.PlayGame(height, width);
            } while (game.PlayAgain);
        }
    }
}