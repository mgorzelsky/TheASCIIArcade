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

        Bird bird;
        //Walls wall = new Walls();
        TMR.Timer wallGenerator = new TMR.Timer(5000);
        List<Walls> walls = new List<Walls>();

        public Game()
        {
            bird = new Bird();


            wallGenerator.Elapsed += new TMR.ElapsedEventHandler(OnTimedEvent);
            wallGenerator.Enabled = true;
        }

        public void PlayGame()
        {
            Thread inputThread = new Thread(WaitForInput);
            inputThread.Start();

            Console.CursorVisible = false;

            gameOver = false;
            while (!gameOver)
            {
                state = new CellState[FlappyBirdProgram.width, FlappyBirdProgram.height];
                state[bird.Position.X, bird.Position.Y] = CellState.Bird;
                if (walls.Count != 0 && walls.Capacity != 0)
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

        public void OnTimedEvent(Object source, TMR.ElapsedEventArgs e)
        {
            walls.Add(new Walls());
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
