using System;
using System.Collections.Generic;
using System.Threading;
using System.Drawing;

namespace EscapeFromDarkForest
{
    public enum GameObjects { empty, player, enemy, food,  wall, exit };
    public class Game
    {
        //These are static so they can be referenced across the program instead of having to be constantly
        //passed around methods.
        public static GameObjects[,] gameBoard;
        public static int level;
        public static bool gameOver;
        public static Random rnd = new Random();

        Player player;
        Renderer renderer;
        private bool levelComplete = false;
        private List<Enemy> listOfEnemies;
        private List<Walls> listOfWalls;
        private List<Food> listOfFood;

        public Game()
        {
            gameBoard = new GameObjects[8, 8];
            level = 1;
            gameOver = false;
        }

        public void Start()
        {
            player = new Player(EscapeFromDarkForestProgram.game);
            renderer = new Renderer(player);

            gameBoard = new GameObjects[8, 8];

            Thread renderThread = new Thread(renderer.DrawScreen);
            renderThread.Start();

            while (!gameOver) //overall game loop
            {
                //this section builds the new level and resets all objects to where they start
                levelComplete = false;
                gameBoard = new GameObjects[8, 8];

                player.ResetPosition();
                gameBoard[player.Position.X, player.Position.Y] = GameObjects.player;
                gameBoard[7, 0] = GameObjects.exit;

                listOfWalls = new List<Walls>();
                WallBuilder();

                listOfEnemies = new List<Enemy>();
                EnemyBuilder();

                listOfFood = new List<Food>();
                FoodBuilder();

                while (!levelComplete && !gameOver) //level loop
                {
                    //SetupGameBoard() is called after every object updates so that the gameBoard
                    //array is constantly up to date for the renderer to draw. This keeps the game
                    //feeling responsive and smooth.
                    SetupGameboard();

                    bool validMove = WaitForInput();

                    SetupGameboard();

                    if (validMove)
                    {
                        Thread.Sleep(50);
                        foreach (Enemy enemy in listOfEnemies)
                        {
                            enemy.Move();
                            SetupGameboard();
                            Thread.Sleep(20);   //This delay just allows for the move to finish before the attack animation starts.
                            enemy.Act();
                        }
                    }

                    SetupGameboard();

                    CheckCollisions();

                    if (player.Food < 1)
                        gameOver = true;
                }
                level++;
                Thread.Sleep(500);
            }
            renderThread.Join(); //Wait for rendering to finish before displaying game over so that it doesn't hijack the screen.
            PlayAgain();
        }

        //Simply applies all the current object locations to a fresh gameBoard array
        private void SetupGameboard()
        {
            gameBoard = new GameObjects[8, 8];

            if (listOfFood != null)
                foreach (Food food in listOfFood)
                    if (food != null)
                        gameBoard[food.Position.X, food.Position.Y] = GameObjects.food;
            gameBoard[7, 0] = GameObjects.exit;
            foreach (Walls wall in listOfWalls)
                gameBoard[wall.Position.X, wall.Position.Y] = GameObjects.wall;
            foreach (Enemy enemy in listOfEnemies)
                gameBoard[enemy.Position.X, enemy.Position.Y] = GameObjects.enemy;
            gameBoard[player.Position.X, player.Position.Y] = GameObjects.player;
        }

        private void CheckCollisions()
        {
            Point exit = new Point(7, 0);

            //for loop over foreach ensures that only the eaten food object is removed and not all the food objects
            for (int i = 0; i < listOfFood.Count; i++)
            {
                if (listOfFood[i] != null)
                {
                    if (player.Position == listOfFood[i].Position)
                    {
                        player.Eat();
                        listOfFood[i] = null;
                    }
                }
            }
            if (player.Position.Equals(exit))
                levelComplete = true;
        }

        private bool WaitForInput()
        {
            //This while loops clears out any stored/buffered key inputs since the last time we were at the
            //ReadKey so that a whole bunch of inputs aren't executed all at once and only the input the
            //user explicitly chooses is used.
            while (Console.KeyAvailable) Console.ReadKey(true);
            if (Console.KeyAvailable != true)
            {
                ConsoleKey keyPressed = Console.ReadKey(true).Key;
                if (keyPressed == ConsoleKey.UpArrow || keyPressed == ConsoleKey.LeftArrow ||
                    keyPressed == ConsoleKey.DownArrow || keyPressed == ConsoleKey.RightArrow)
                    return player.Move(keyPressed);
            }

            return false;
        }

        private void EnemyBuilder()
        {
            int numberOfEnemies = 0;
            if (level < 5)
                numberOfEnemies = 0;
            else if (level < 10)
                numberOfEnemies = 1;
            else if (level < 15)
                numberOfEnemies = 2;
            else if (level >= 15)
                numberOfEnemies = 3;

            for (int i = 0; i < numberOfEnemies; i++)
            {
                listOfEnemies.Add(new Enemy(player));
                gameBoard[listOfEnemies[i].Position.X, listOfEnemies[i].Position.Y] = GameObjects.enemy;
            }
        }

        //Build a list of the number of walls that will be present on the current level.
        private void WallBuilder()
        {
            int numberOfWalls = rnd.Next(5, 9);
            for (int i = 0; i < numberOfWalls; i++)
            {
                listOfWalls.Add(new Walls());
                gameBoard[listOfWalls[i].Position.X, listOfWalls[i].Position.Y] = GameObjects.wall;
            }
        }

        //Build a list of the number of foods present on the current level.
        private void FoodBuilder()
        {
            int numberOfFoods;
            int foodSpawnChanceModifier = 80;
            if (level < 10)
                numberOfFoods = 2;
            else
                numberOfFoods = 1;

            if (level % 2 == 0 && foodSpawnChanceModifier > 10)
                foodSpawnChanceModifier -= 2;
            for (int i = 0; i < numberOfFoods; i++)
            {
                if (rnd.Next(0, 100) < foodSpawnChanceModifier)
                    listOfFood.Add(new Food());
            }
        }
        //When the player attacks a wall remove the wall they attack from the list.
        public void RemoveWallAt(Point p)
        {
            for (int i = 0; i < listOfWalls.Count; i++)
            {
                if (listOfWalls[i].Position.X == p.X && listOfWalls[i].Position.Y == p.Y)
                    listOfWalls.RemoveAt(i);
            }
        }
        //Display game over and ask if the player wishes to play again, if they do give the option to view the instructions again.
        private void PlayAgain()
        {
            EscapeFromDarkForestProgram.DrawGenericScreen($"Game Over. You made it {level} levels.", (EscapeFromDarkForestProgram.width - 33) / 2, EscapeFromDarkForestProgram.height / 2 - 2);
            EscapeFromDarkForestProgram.DrawGenericScreen("Would you like to play again? Y/N", (EscapeFromDarkForestProgram.width - 33) / 2, EscapeFromDarkForestProgram.height / 2);
            bool validChoice = false;
            while (!validChoice)
            {
                switch (Console.ReadKey(true).Key)
                {
                    case (ConsoleKey.Y):
                        EscapeFromDarkForestProgram.playAgain = true;
                        validChoice = true;
                        break;
                    case (ConsoleKey.N):
                        EscapeFromDarkForestProgram.playAgain = false;
                        EscapeFromDarkForestProgram.viewInstructions = false;
                        validChoice = true;
                        break;
                }
            }
            if (EscapeFromDarkForestProgram.playAgain)
            {
                EscapeFromDarkForestProgram.DrawGenericScreen("Do you wish to view the instructions? Y/N", (EscapeFromDarkForestProgram.width - 41) / 2, EscapeFromDarkForestProgram.height / 2 + 2);
                validChoice = false;
                while (!validChoice)
                {
                    switch (Console.ReadKey(true).Key)
                    {
                        case (ConsoleKey.Y):
                            EscapeFromDarkForestProgram.viewInstructions = true;
                            EscapeFromDarkForestProgram.playAgain = false;
                            validChoice = true;
                            break;
                        case (ConsoleKey.N):
                            EscapeFromDarkForestProgram.viewInstructions = false;
                            validChoice = true;
                            break;
                    }
                }
            }
        }
    }
}
