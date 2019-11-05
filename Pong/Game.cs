using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Drawing;

namespace Pong
{
    //All the possible states of any poisition within the internalState[,]
    public enum GameItems { Nothing, Wall, Paddle, Ball, Goal }
    
    class Game
    {
        public static int Width { get; set; }
        public static int Height { get; set; }
        public static bool roundRunning = true;

        Ball ball = new Ball();
        Paddle leftPaddle;
        Paddle rightPaddle;
        GameItems[,] internalState;
        private int leftScore = 0;
        private int rightScore = 0;
        private bool noWinner = true;

        public Game(int width, int height)
        {
            Width = width;
            Height = height;
            this.leftPaddle = new Paddle("left");
            this.rightPaddle = new Paddle("right");
        }

        public void Start()
        {
            Thread inputThread = new Thread(WaitForInput);
            inputThread.Start();

            internalState = new GameItems[Width, Height];
            Renderer screenRenderer = new Renderer();

            while (noWinner)
            {
                Console.Clear();
                ball.Serve();
                //Main gameplay loop. Updates every 1/60th of a second for a 60 hz refresh rate.
                while (roundRunning)
                {
                    internalState = new GameItems[Width, Height];
                    for (int x = 0; x < Width; x++)
                        internalState[x, 0] = GameItems.Wall;
                    for (int x = 0; x < Width; x++)
                        internalState[x, Height - 1] = GameItems.Wall;

                    //set the goal fields
                    SetGoalLines();
                    //Place the left paddle into the internalState array
                    foreach (Point paddlePosition in leftPaddle.PaddlePosition)
                    {
                        internalState[paddlePosition.X, paddlePosition.Y] = GameItems.Paddle;
                    }
                    //Place the right paddle into the internalState array
                    foreach (Point paddlePosition in rightPaddle.PaddlePosition)
                    {
                        internalState[paddlePosition.X, paddlePosition.Y] = GameItems.Paddle;
                    }
                    //Place the ball into the internalState array
                    internalState[ball.BallPosition.X, ball.BallPosition.Y] = GameItems.Ball;

                    screenRenderer.DrawGame(internalState);
                    Console.SetCursorPosition(0, Height);
                    Console.Write("Score: " + leftScore);
                    Console.SetCursorPosition(Width - 8, Height);
                    Console.Write("Score: " + rightScore);

                    ball.CollisionObjects = internalState;

                    Thread.Sleep(1000 / 60000);
                }

                //Increment the score for the winner of round
                if (ball.BallPosition.X == 0)
                    rightScore++;
                if (ball.BallPosition.X == Width - 1)
                    leftScore++;
                Console.SetCursorPosition(Width / 3, Height / 2);
                Console.Write("Round Complete!");
                Console.SetCursorPosition(Width / 3, (Height / 2) + 1);
                Console.Write("Left Score: " + leftScore + "  Right Score: " + rightScore);
                Thread.Sleep(2000);
                roundRunning = true;

                if (rightScore == 3 || leftScore == 3)
                    noWinner = false;
            }

            screenRenderer.DrawGame(internalState);
            Console.SetCursorPosition(Width / 3, Height / 2);
            Console.Write("Winner!");
            if (leftScore == 3)
            {
                Console.SetCursorPosition(Width / 3, (Height / 2) + 1);
                Console.Write("Left player!");
            }
            if (rightScore == 3)
            {
                Console.SetCursorPosition(Width / 3, (Height / 2) + 1);
                Console.Write("Right player!");
            }

            Console.SetCursorPosition(Width / 3, Height - 2);
            Console.Write("Press any key to continue");
            //Thread.Sleep(3000);
            inputThread.Join();
        }

        private void SetGoalLines()
        {
            //left goal
            for (int y = 1; y < Height - 1; y++)
            {
                if (internalState[0, y] != GameItems.Paddle)
                {
                    internalState[0, y] = GameItems.Goal;
                }
            }
            //right goal
            for (int y = 1; y < Height - 1; y++)
            {
                if (internalState[Width - 1, y] != GameItems.Paddle)
                {
                    internalState[Width - 1, y] = GameItems.Goal;
                }
            }
        }
        
        //Loop waiting for player input. On a seperate thread so it can be killed when the game over state is
        //reached and it doesn't hang up the game state progression at the ReadKey().
        private void WaitForInput()
        {
            while (noWinner)
            {
                ConsoleKey keyPressed = Console.ReadKey(true).Key;
                leftPaddle.PaddleMove(keyPressed);
                rightPaddle.PaddleMove(keyPressed);
            }
        }
    }
}
