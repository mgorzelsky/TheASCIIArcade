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
        public static TMR.Timer gameStep;
        List<Wall> listOfWalls = new List<Wall>();

        public Game()
        {
            gameStep = new TMR.Timer(50);
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
                foreach (var wall in listOfWalls)
                {
                    int[] wallArray = wall.getWall();
                    for (int y = 0; y < wallArray.Length; y++)
                    {
                        if (wallArray[y] == 0 && y < FlappyBirdProgram.height)
                            state[wall.getCurrentX(), y] = CellState.Pillar;
                    }
                }
                CheckCollision();
                render.DrawScreen();
            }

            gameStep.Dispose();
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
            for (int i = 0; i < listOfWalls.Count; i++)
            {
                if (listOfWalls[i].getCurrentX() == bird.Position.X)
                {
                    if (listOfWalls[i].getWall()[bird.Position.Y] == 0)
                    {
                        gameOver = true;
                    }
                }
                if (listOfWalls[i].getCurrentX() == 0)
                    listOfWalls.RemoveAt(i);
            }
        }

        private void onGameStep(object sender, TMR.ElapsedEventArgs e)
        {
            stepCounter++;
            if (stepCounter == 75)
            {
                listOfWalls.Add(new Wall());
                stepCounter = 1;
            }
        }

        private void WaitForInput()
        {
            while (!gameOver)
            {
                ConsoleKey keyPressed = Console.ReadKey(true).Key;
                if (keyPressed == ConsoleKey.UpArrow || keyPressed == ConsoleKey.Spacebar)
                {
                    bird.Flap();
                }
            }
        }


    }
}
