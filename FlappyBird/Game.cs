using System;
using System.Collections.Generic;
using System.Text;
using TMR = System.Timers;
using System.Drawing;
using System.Threading;

namespace FlappyBird
{
    public enum CellState { Empty, Bird, Pillar };
    public class Game
    {
        public static CellState[,] state;
        int score;
        public int HighScore { get; private set; }
        bool gameOver = false;
        Render render = new Render();
        private int stepCounter = 1;

        Bird bird;
        public static TMR.Timer gameStep = new TMR.Timer(50);
        List<Walls> walls = new List<Walls>();

        public Game()
        {
            bird = new Bird();
            gameStep.Elapsed += new TMR.ElapsedEventHandler(onGameStep);
        }

        public void PlayGame()
        {
            Thread inputThread = new Thread(WaitForInput);
            inputThread.Start();

            gameStep.Start();

            Console.CursorVisible = false;

            gameOver = false;
            while (!gameOver)
            {
                state = new CellState[FlappyBirdProgram.width, FlappyBirdProgram.height];
                state[bird.Position.X, bird.Position.Y] = CellState.Bird;
                if (walls.Capacity != 0 && walls.Count != 0)
                {
                    foreach (var wall in walls)
                    {
                        int[] wallArray = wall.getWall();
                        for (int y = 0; y < wallArray.Length; y++)
                        {
                            if (wallArray[y] == 0 && y < FlappyBirdProgram.height)
                                state[wall.getCurrentX(), y] = CellState.Pillar;
                        }
                    }
                }
                CheckCollision();
                render.DrawScreen();
            }

            gameStep.Stop();
            Console.Clear();
            Console.SetCursorPosition(39, 19);
            Console.WriteLine("Game Over!");
        }

        void CheckCollision()
        {
            if (bird.Position.Y < 1 || bird.Position.Y > FlappyBirdProgram.height - 2)
            {
                gameOver = true;
            }
            foreach (var wall in walls)
            {
                if (wall.getCurrentX() == bird.Position.X)
                {
                    if (wall.getWall()[bird.Position.Y] == 0)
                    {
                        gameOver = true;
                    }
                }
            }
        }

        private void onGameStep(object sender, TMR.ElapsedEventArgs e)
        {
            stepCounter++;
            if (stepCounter == 75)
            {
                walls.Add(new Walls());
                stepCounter = 1;
            }
        }

        private void WaitForInput()
        {
            while (!gameOver)
            {
                if (Console.ReadKey(true).Key == ConsoleKey.UpArrow)
                {
                    bird.Flap();
                }
            }
        }


    }
}
