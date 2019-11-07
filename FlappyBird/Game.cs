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
        int score; //will be used in the future for score tracking
        public int HighScore { get; private set; }
        bool gameOver = false;
        Render render = new Render();
        private int stepCounter = 1;

        Bird bird;
        public static TMR.Timer gameStep;
        List<Wall> listOfWalls = new List<Wall>();

        //Create one timer to handle the different progressing events. Timer set to the lowest required value.
        public Game()
        {
            gameStep = new TMR.Timer(50);
            bird = new Bird();
            gameStep.Elapsed += new TMR.ElapsedEventHandler(onGameStep);
        }

        public void PlayGame()
        {
            //Input is handled on its own thread seperate from the looping game logic.
            Thread inputThread = new Thread(WaitForInput);
            inputThread.Start();

            gameStep.Start();

            Console.CursorVisible = false;

            //Loop the updates all the positions of the objects. Simply asks each for its position and writes that to a new array.
            //By having this run constantly the internal state of the game is always as up to date as possible and catches any
            //changes immediately.
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

            gameStep.Dispose(); //dispose of the timer when done with it
            Console.Clear();
            Console.SetCursorPosition((FlappyBirdProgram.width - 10) / 2, FlappyBirdProgram.height / 2);
            Console.WriteLine("Game Over!");
            Console.SetCursorPosition((FlappyBirdProgram.width - 21) / 2, FlappyBirdProgram.height / 2 + 1);
            Console.WriteLine("Press any key to quit");
            inputThread.Join(); //wait for the input thread to finish before leaving to prevent conflicts later
        }

        void CheckCollision()
        {
            if (bird.Position.Y < 1 || bird.Position.Y > FlappyBirdProgram.height - 2) //does bird hit floor or ceiling?
            {
                gameOver = true;
            }
            for (int i = 0; i < listOfWalls.Count; i++) //does bird hit wall
            {
                if (listOfWalls[i].getCurrentX() == bird.Position.X)
                {
                    if (listOfWalls[i].getWall()[bird.Position.Y] == 0)
                    {
                        gameOver = true;
                    }
                }
                if (listOfWalls[i].getCurrentX() == 0) //if the current wall being checked has reached the end of its path remove it from the list. Without this is is essentially a memory leak.
                    listOfWalls.RemoveAt(i);
            }
        }

        //Step counter counts how many times 50ms has gone by and after 3750ms creates another wall and starts the counter over
        private void onGameStep(object sender, TMR.ElapsedEventArgs e)
        {
            stepCounter++;
            if (stepCounter == 75)
            {
                listOfWalls.Add(new Wall());
                stepCounter = 1;
            }
        }

        //Input loop. Watches key presses and on the appropriate key press calls the bird to flap.
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
