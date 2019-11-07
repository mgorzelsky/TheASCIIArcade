using System;
using System.Drawing;

namespace EscapeFromDarkForest
{
    //Food needs to be able to generate a location for itself and remember that location. All other logic
    //for it is held in the other classes as it is all based on their interaction with a food object.
    class Food
    {
        private Point position;
        public Point Position { get { return position; } }
        public Food()
        {
            FoodPositionGenerator();
        }

        private void FoodPositionGenerator()
        {
            while (true)
            {
                position.X = Game.rnd.Next(0, 8);
                position.Y = Game.rnd.Next(0, 8);
                if (Game.gameBoard[position.X, position.Y] == GameObjects.empty)
                    return;
            }
        }
    }
}
