﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using System.Drawing;

namespace Snake
{
    public enum StateOfLocation { Empty, Food, Snake }
    class Game
    {
        public static StateOfLocation[,] gameBoard;

        private readonly Timer timer = new Timer();
        private readonly Render screen = new Render();
        private bool gameRunning = true;
        private Snake snake = new Snake();
        private Food food = new Food(2, 2);
        private int score = 0;
        private int tickSpeed; //Necessary to ensure that the game doesn't get too fast
        private bool hasEaten = false;

        //Constructor to pass in screen size, allows game to be re-sized with the change of a single variable
        public Game()
        {
            gameBoard = new StateOfLocation[SnakeProgram.width, SnakeProgram.height];
        }

        //Main game loop
        public int PlayGame()
        {
            tickSpeed = 200;
            timer.Interval = 200;

            Console.Clear();
            Console.CursorVisible = false;

            //This event controls the speed of the snake.
            timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            timer.Start();

            //Waiting for player input while the rest of the game runs on a timer
            while (gameRunning)
            {
                ConsoleKey snakeDirection = Console.ReadKey(true).Key;

                if (snakeDirection == ConsoleKey.UpArrow && 
                    ConvertToString(snakeDirection) != ReverseDirection(snake.GetDirection()))
                {
                    snake.ChangeDirection("Up");
                }
                if (snakeDirection == ConsoleKey.LeftArrow &&
                    ConvertToString(snakeDirection) != ReverseDirection(snake.GetDirection()))
                {
                    snake.ChangeDirection("Left");
                }
                if (snakeDirection == ConsoleKey.DownArrow && 
                    ConvertToString(snakeDirection) != ReverseDirection(snake.GetDirection()))
                {
                    snake.ChangeDirection("Down");
                }
                if (snakeDirection == ConsoleKey.RightArrow && ConvertToString(snakeDirection) != ReverseDirection(snake.GetDirection()))
                {
                    snake.ChangeDirection("Right");
                }
            }

            return score;
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            Step();
            //important that the speed only increases when the snake has eaten otherwise the snake will speed up constantly
            if (tickSpeed > 100 && score % 10 == 0 && hasEaten)
            {
                timer.Interval -= 10;
                tickSpeed -= 10;
                hasEaten = false;
            }
        }

        //Controls the pace of the game. Every time this is called the snake advances one space and 
        //collision checks are made
        private void Step()
        {
            StateOfLocation[,] oldGameBoard = gameBoard;
            gameBoard = new StateOfLocation[SnakeProgram.width, SnakeProgram.height];

            snake.MoveSnake();
            List<Point> newSnakePosition = snake.GetSnakePosition();
            Point newFoodPosition = food.FoodPosition;
            gameBoard[newFoodPosition.X, newFoodPosition.Y] = StateOfLocation.Food;

            if (!CheckCollision(oldGameBoard, newSnakePosition))
            {
                foreach (Point segment in newSnakePosition)
                {
                    gameBoard[segment.X, segment.Y] = StateOfLocation.Snake;
                }
            }
            //Game over stops the timer, cleans up everything, and writes the game over messages.
            else
            {
                timer.Stop();
                gameRunning = false;
                Console.Clear();
                string gameOverMessage = "Game Over!";
                string scoreMessage = $"Score: {score}";
                string continueMessage = "Press any key to quit";
                Console.SetCursorPosition((SnakeProgram.width - gameOverMessage.Length) / 2, SnakeProgram.height / 2 - 1);
                Console.Write(gameOverMessage);
                Console.SetCursorPosition((SnakeProgram.width - scoreMessage.Length) / 2, SnakeProgram.height / 2);
                Console.Write(scoreMessage);
                Console.SetCursorPosition((SnakeProgram.width - continueMessage.Length) / 2, SnakeProgram.height / 2 + 1);
                Console.Write(continueMessage);
                
                return;
            }

            screen.DrawScreen();
        }

        private bool CheckCollision(StateOfLocation[,] oldGameBoard, List<Point> newSnakePosition)
        {
            Point snakeHeadPosition = newSnakePosition[0];

            //Does the snake run out of the game area?
            if (snakeHeadPosition.Y < 1 || 
                snakeHeadPosition.Y > SnakeProgram.height - 2 || 
                snakeHeadPosition.X < 1 || 
                snakeHeadPosition.X > SnakeProgram.width - 2)
                return true;

            //Does the snake run into itself?
            if (oldGameBoard[snakeHeadPosition.X, snakeHeadPosition.Y] == StateOfLocation.Snake)
                return true;

            //Does the snake run into food?
            if (oldGameBoard[snakeHeadPosition.X, snakeHeadPosition.Y] == StateOfLocation.Food)
            {
                snake.Eat();
                score++;
                hasEaten = true;
                food.ChangeFoodPosition();
                return false;
            }

            //Does the snake go into empty space?
            return false;
        }

        //Converts ConsoleKey to string for comparison to the snake direction
        private string ConvertToString(ConsoleKey variableAsString)
        {
            switch (variableAsString)
            {
                case ConsoleKey.UpArrow:
                    return "Up";
                case ConsoleKey.LeftArrow:
                    return "Left";
                case ConsoleKey.DownArrow:
                    return "Down";
                case ConsoleKey.RightArrow:
                    return "Right";
            }
            return "not a vaild choice";
        }

        //Takes in the snake direction and returns the reverse value for comparison.
        private string ReverseDirection(string snakeCurrentDirection)
        {
            switch (snakeCurrentDirection)
            {
                case "Up":
                    return "Down";
                case "Down":
                    return "Up";
                case "Left":
                    return "Right";
                case "Right":
                    return "Left";
            }
            return "not a valid choice";
        }
    }
}
