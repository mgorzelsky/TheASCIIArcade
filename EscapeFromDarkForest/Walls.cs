using System;
using System.Drawing;

namespace EscapeFromDarkForest
{
    class Walls
    {
        private static Point noZone1;
        private static Point noZone2;
        private Point position;
        public Point Position { get { return position; } }

        public Walls()
        {
            noZone1 = new Point(0, 7);
            noZone2 = new Point(7, 0);
            GeneratePosition();
        }

        private void GeneratePosition()
        {
            while (true)
            {
                position.X = Game.rnd.Next(1, 7);
                position.Y = Game.rnd.Next(1, 7);
                if (!position.Equals(noZone1) && !position.Equals(noZone2) && 
                    Game.gameBoard[position.X, position.Y] != GameObjects.wall)
                    return;
            }
        }
    }
}
