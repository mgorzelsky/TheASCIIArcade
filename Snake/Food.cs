using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;       // for Point object

namespace Snake
{
    class Food
    {
        //  variables and properties
        private Point foodPosition;

        public Point FoodPosition 
        {
            get { return foodPosition; }
        }

        // methods
        private void SetFoodPosition(int x, int y)
        {
            foodPosition.X = x;
            foodPosition.Y = y;
        }

        // changes the position of the food on the screen randomly based on the size of the screen size
        public void ChangeFoodPosition()
        {
            bool tryAgain = false;
            do
            {
                Random rand = new Random();
                int newX = rand.Next(2, SnakeProgram.width - 1);
                int newY = rand.Next(2, SnakeProgram.height - 1);
                SetFoodPosition(newX, newY);

                if (Game.gameBoard[newX, newY] == StateOfLocation.Snake)
                    tryAgain = true;
            }
            while (tryAgain);
        }

        // constructor
        public Food(int x, int y)
        {
            Point p1 = new Point(x, y);
            foodPosition = p1;
        }
    }
}
