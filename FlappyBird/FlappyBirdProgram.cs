﻿using System;
using System.Drawing;
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
            //Console.SetWindowSize(1, 1);
            //Console.SetBufferSize(width, height + 1);
            //Console.SetWindowSize(width, height + 1);

            Game game = new Game();
            Console.WriteLine("Welcome to flappy bird in the terminal.");
            Thread.Sleep(2000);

            do
            {
                game.PlayGame();
            } while (game.PlayAgain);
        }
    }
}