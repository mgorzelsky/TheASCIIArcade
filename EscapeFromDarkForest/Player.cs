using System;
using System.Drawing;
using System.Diagnostics;

namespace EscapeFromDarkForest
{
    class Player
    {
        private Point position;
        public Point Position { get { return position; } }
        private int food;
        public int Food { get { return food; } }
        private Game game;
        public Player(Game game)
        {
            ResetPosition();
            this.food = 100;
            this.game = game;
        }

        //Player always starts in the bottom left corner
        public void ResetPosition()
        {
            position.X = 0;
            position.Y = 7;
        }
        //take in the player input. If empty space or food allow movement and use a food. If wall destroy that wall and use a food.
        //If OOB or Enemy don't move and don't use food.
        public bool Move(ConsoleKey direction)
        {
            switch (direction)
            {
                case (ConsoleKey.UpArrow):
                    if (position.Y > 0)
                    {
                        if (Game.gameBoard[position.X, position.Y - 1] == GameObjects.wall) //Check for wall collision, if so, destroy that wall
                        {
                            game.RemoveWallAt(new Point(position.X, position.Y - 1));
                            food--;
                            return true;
                        }
                        if (Game.gameBoard[position.X, position.Y - 1] == GameObjects.empty ||
                            Game.gameBoard[position.X, position.Y - 1] == GameObjects.food ||
                            Game.gameBoard[position.X, position.Y - 1] == GameObjects.exit)
                        {
                            position.Y--;
                            food--;
                            return true;
                        }

                    }
                    break;
                case (ConsoleKey.LeftArrow):
                    if (position.X > 0)                    
                    {
                        if (Game.gameBoard[position.X - 1, position.Y] == GameObjects.wall)
                        {
                            game.RemoveWallAt(new Point(position.X - 1, position.Y));
                            food--;
                            return true;
                        }
                        if (Game.gameBoard[position.X - 1, position.Y] == GameObjects.empty ||
                            Game.gameBoard[position.X - 1, position.Y] == GameObjects.food ||
                            Game.gameBoard[position.X - 1, position.Y] == GameObjects.exit)
                        {
                            position.X--;
                            food--;
                            return true;
                        }
                    }
                    break;
                case (ConsoleKey.DownArrow):
                    if (position.Y < 7)
                    {
                        if (Game.gameBoard[position.X, position.Y + 1] == GameObjects.wall)
                        {
                            game.RemoveWallAt(new Point(position.X, position.Y + 1));
                            food--;
                            return true;
                        }
                        if (Game.gameBoard[position.X, position.Y + 1] == GameObjects.empty ||
                            Game.gameBoard[position.X, position.Y + 1] == GameObjects.food ||
                            Game.gameBoard[position.X, position.Y + 1] == GameObjects.exit)
                        {
                            position.Y++;
                            food--;
                            return true;
                        }
                    }
                    break;
                case (ConsoleKey.RightArrow):
                    if (position.X <7)
                    {
                        if (Game.gameBoard[position.X + 1, position.Y] == GameObjects.wall)
                        {
                            game.RemoveWallAt(new Point(position.X + 1, position.Y));
                            food--;
                            return true;
                        }
                        if (Game.gameBoard[position.X + 1, position.Y] == GameObjects.empty ||
                            Game.gameBoard[position.X + 1, position.Y] == GameObjects.food ||
                            Game.gameBoard[position.X + 1, position.Y] == GameObjects.exit)
                        {
                            position.X++;
                            food--;
                            return true;
                        }
                    }
                    break;
            }
            Debug.WriteLine("That was not a valid move.");
            return false;
        }

        //When eating food increase the current food meter by the specified amount.
        public void Eat()
        {
            food += 20;
            Debug.WriteLine("Om nom nom nom...");
        }
        //When attacked remove food at the specified amount.
        public void TakeDamage()
        {
            food -= 10;
            Debug.WriteLine("owwwwww!");
        }
    }
}
