using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using System.Drawing;

namespace FlappyBird
{
    public enum CellState { Empty, Bird, Pillar };
    public class Game
    {
        CellState[,] state;
        int score;
        public int HighScore { get; private set; }
        public bool PlayAgain { get; private set; }
        bool gameOver;
        int height;
        int width;
        Render render = new Render();

        Bird bird;
        //Walls wall = new Walls();
        Timer wallGenerator = new Timer(5000);
        Timer gameTimer = new Timer(100);
        List<Walls> walls = new List<Walls>();

        public Game()
        {
            bird = new Bird();


            wallGenerator.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            wallGenerator.Enabled = true;
            gameTimer.Elapsed += new ElapsedEventHandler(GameLoop);
            gameTimer.Enabled = true;     
        }

        public void PlayGame(int height, int width)
        {
            this.height = height;
            this.width = width;

            Console.CursorVisible = false;
            ConsoleKey action = ConsoleKey.H;

            gameOver = false;
            do
            {
                //while (action != ConsoleKey.Q)
                //{
                if (gameOver != true)
                {

                    action = Console.ReadKey().Key;
                    if (action == ConsoleKey.UpArrow)
                    {
                        bird.Flap();
                        //Step();
                        //render.DrawScreen(state, height, width);
                    }
                }
                //}
            }
            while (gameOver != true);

            

            //do
            //{
            //    Step();

            Console.Clear();
            Console.SetCursorPosition(39, 19);
            Console.WriteLine("Game Over!");
            Console.ReadKey();
        }

        void Step()
        {
            state = new CellState[height, width];
            int birdX = bird.getX();
            int birdY = bird.getY();
            state[birdY, birdX] = CellState.Bird;
            foreach(var wall in walls)
            {
                int[] wallArray = wall.getWall();
                for (int y = 0; y < wallArray.Length; y++)
                {
                    if (wallArray[y] == 0)
                        state[y, wall.getCurrentX()] = CellState.Pillar;
                }
            }
            CheckCollision();
        }

        void CheckCollision()
        {
            if (bird.getY() < 1 || bird.getY() > height - 2)
            {
                gameOver = true;
                gameTimer.Stop();
            }
            foreach (var wall in walls)
            {
                if (wall.getCurrentX() == bird.getX())
                {
                    if (wall.getWall()[bird.getY()] == 0)
                    {
                        gameOver = true;
                        gameTimer.Stop();
                    }
                }
            }
        }

        public void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            walls.Add(new Walls());
        }

        public void GameLoop(Object source, ElapsedEventArgs e)
        {
            Step();
            render.DrawScreen(state, height, width);
        }


    }
}
